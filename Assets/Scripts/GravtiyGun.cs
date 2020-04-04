using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravtiyGun : MonoBehaviour, IItem
{
    private bool holdingObject;
    public Transform rayCaster;
    public Transform Holder;

    [Range(0, 10)]
    [Tooltip("Range at which you can pull objects (ray length).")]
    public float pullRange = 3f;
    [Range(0, 10)]
    [Tooltip("Range at which you can shoot objects (ray length).")]
    public float pushRange = 3f;
    [Range(0, 100)]
    [Tooltip("Amount of power your held item will be shot with.")]
    public float firePower = 10f;
    [Range(0, 100)]
    [Tooltip("Power you use when pushing objects.")]
    public float pushPower = 10f;

    private Rigidbody rb;
    private bool turningLeft = false, turningRight = false;
    private float turningDegree = 0;
    Vector3 oldEulerAngles;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        // Sets position in front of raycaster
        Holder.position = rayCaster.position + rayCaster.forward;
        oldEulerAngles = this.transform.rotation.eulerAngles;
    }

    public void Use()
    {
        Debug.Log("Use");
        // If item held, throws it
        ThrowItem();
        // Runs if item is not held
        ShoveObject();
    }
    public void AltUse()
    {
        Debug.Log("Alt Use");
        // Picks up item if it is in front of player
        PickUpItem();
    }


    public void Pickup(Transform hand)
    {
        rb.isKinematic = true;
        this.transform.SetParent(hand);
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
        this.GetComponent<SpinItem>().enabled = false;
    }

    public void Drop()
    {
        rb.isKinematic = false;
        rb.AddRelativeForce(Vector3.forward, ForceMode.Impulse);
        this.transform.SetParent(null);
    }


    public void Update()
    {

        Debug.Log("This is how far left " + (oldEulerAngles.x - transform.rotation.eulerAngles.x));

        Transform heldItem = this.transform.GetChild(0).GetChild(0);
        // Allows for .5 dead zone
        if (oldEulerAngles.x - this.transform.rotation.eulerAngles.x < .5 && oldEulerAngles.x - this.transform.rotation.eulerAngles.x > -.5)
        {
            //NO ROTATION
            if(turningDegree < 0.02 && turningDegree > -0.02) // Resets to normal if position is almost normal
            {
                heldItem.transform.position = this.Holder.transform.position;
            } // Handles placing it back
            else if(turningDegree < 0)
            {
                turningDegree += 0.01f;
            }
            else if (turningDegree > 0)
            {
                turningDegree -= 0.01f;
            }
        }
        // Turning right
        else if (oldEulerAngles.x - this.transform.rotation.eulerAngles.x > 1)
        {
            oldEulerAngles = this.transform.rotation.eulerAngles;
            //DO WHATEVER YOU WANT
            Debug.Log("Right");
            if(turningDegree < 2)
            {
                turningDegree += 0.03f;
            }

        }
        // Turning left
        else if (oldEulerAngles.x - this.transform.rotation.eulerAngles.x < 1)
        {
            oldEulerAngles = this.transform.rotation.eulerAngles;
            //DO WHATEVER YOU WANT
            if (turningDegree > -2)
            {
                turningDegree -= 0.03f;
            }
            Debug.Log("Left");

        }
        else
        {
            oldEulerAngles = this.transform.rotation.eulerAngles;
            //DO WHATEVER YOU WANT
            Debug.Log("Rotation");
        }
        Debug.Log(turningDegree);
        // Places helditem at relative postion
        heldItem.transform.position = this.Holder.transform.position + Holder.right * turningDegree;
    }


    public void OnTriggerEnter(Collider other)
    {
        // Designed to fix FPS gravity when he lands
        ResetFPSController();
    }

    public void ResetFPSController()
    {
        // Used for jump pad, to make player work right when they hit the ground.
        Rigidbody grandfatherBody = this.GetComponentInParent<Camera>().GetComponentInParent<Rigidbody>();
        CharacterController grandfatherController = this.GetComponentInParent<Camera>().GetComponentInParent<CharacterController>();
        grandfatherController.enabled = true;
        grandfatherBody.isKinematic = true;

    }

    public void BouncePad(RaycastHit hit)
    {
        if (hit.transform.name.ToLower().Contains("bouncepad"))
        {
            // Painful code to make the FPS controller fly
            Rigidbody grandfatherBody = this.GetComponentInParent<Camera>().GetComponentInParent<Rigidbody>();
            CharacterController grandfatherController = this.GetComponentInParent<Camera>().GetComponentInParent<CharacterController>();
            grandfatherController.enabled = false;
            grandfatherBody.isKinematic = false;
            //grandfatherBody.mass = .01f;
            grandfatherBody.AddRelativeForce(Vector3.up * 100);
        }
    }


    public void ShoveObject()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        Debug.DrawRay(rayCaster.position, transform.TransformDirection(Vector3.forward * pushRange), Color.green);

        if (!holdingObject)
        {
            if (Physics.Raycast(rayCaster.position, transform.TransformDirection(Vector3.forward * pushRange), out hit, 3f))
            {
                // Checks if it was an object that can be used
                if (hit.transform.gameObject.GetComponent<Rigidbody>() != null)
                {

                    Debug.Log("I shot " + hit.transform.gameObject.name);
                    Rigidbody rb = hit.transform.gameObject.GetComponent<Rigidbody>();


                    rb.AddRelativeForce(transform.forward * pushPower, ForceMode.Impulse);
                }
                else // Runs if object isn't a moveable object. Then checks if it was a jump pad.
                {
                    BouncePad(hit);
                }
            }
        }
    }

    public void ThrowItem()
    {
        if(holdingObject)
        {
            // Gets held item
            Transform heldItem = this.transform.GetChild(0).GetChild(0);
            heldItem.position = Holder.position;
            turningDegree = 0;
            // Removed child object from parents
            heldItem.parent = null;
            // Get componenets
            Rigidbody heldBody = heldItem.GetComponent<Rigidbody>();
            heldBody.GetComponent<Collider>().enabled = true; // Only gets disabled due to possible issues
            heldBody.isKinematic = false; // Allows it to be forced



            //this.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

            heldItem.transform.rotation = rayCaster.localRotation;

            heldBody.AddRelativeForce(Vector3.forward * firePower, ForceMode.Impulse); // Fires it
            holdingObject = false;

            // Resets position since gaining a child screws it up...
            Holder.position = rayCaster.position + rayCaster.forward;
        }
    }

    public void PickUpItem()
    {
        if (!holdingObject) // Only runs if not holding object
        {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            Debug.DrawRay(rayCaster.position, transform.TransformDirection(Vector3.forward * pullRange), Color.green);
            if (Physics.Raycast(rayCaster.position, transform.TransformDirection(Vector3.forward), out hit, pullRange))
            {
                // Makes sure object has rigidbody
                if (hit.transform.gameObject.GetComponent<Rigidbody>() != null)
                {

                    Debug.Log("I pulled " + hit.transform.gameObject.name);
                    Rigidbody rb = hit.transform.gameObject.GetComponent<Rigidbody>();


                    hit.transform.gameObject.GetComponent<Collider>().enabled = false;

                    // Makes it sit still when held
                    rb.isKinematic = true;
                    // Sets bool to true so gun knows it's holding an item
                    holdingObject = true;
                    // Places object in a position in front of gun
                    hit.transform.position = this.transform.GetChild(0).position;
                    hit.transform.SetParent(this.transform.GetChild(0));
                    hit.transform.localRotation = rayCaster.localRotation;
                    
                }
            }
        }
    }

}

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

    // Start is called before the first frame update
    void Start()
    {
        // Sets position in front of raycaster
        Holder.position = rayCaster.position + rayCaster.forward;
    }

    public void Use()
    {
        Debug.Log("Use");
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        Debug.DrawRay(rayCaster.position, transform.TransformDirection(Vector3.forward * pushRange), Color.green);
        // Runs if not holding object
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
                    if(hit.transform.name.ToLower().Contains("bouncepad"))
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
            }
        }
        else // Handles firing object that's held
        {
            // Gets held item
            Transform heldItem = this.transform.GetChild(0).GetChild(0);
            // Removed child object from parents
            heldItem.parent = null;
            // Get componenets
            Rigidbody heldBody = heldItem.GetComponent<Rigidbody>();
            heldBody.GetComponent<Collider>().enabled = true; // Only gets disabled due to possible issues
            heldBody.isKinematic = false; // Allows it to be forced
            heldBody.AddRelativeForce(rayCaster.transform.forward * firePower, ForceMode.Impulse); // Fires it
            holdingObject = false;

            // Resets position since gaining a child screws it up...
            Holder.position = rayCaster.position + rayCaster.forward;
        }
    }
    public void AltUse()
    {
        Debug.Log("Alt Use");

        if(!holdingObject)
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
                    hit.transform.localRotation = Quaternion.identity;
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        // Used for jump pad, to make player work right when they hit the ground.
            Rigidbody grandfatherBody = this.GetComponentInParent<Camera>().GetComponentInParent<Rigidbody>();
            CharacterController grandfatherController = this.GetComponentInParent<Camera>().GetComponentInParent<CharacterController>();
            grandfatherController.enabled = true;
            grandfatherBody.isKinematic = true;
    }
}

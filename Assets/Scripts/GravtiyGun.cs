using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravtiyGun : MonoBehaviour, IItem
{
    public Transform rayCaster;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Use()
    {
        Debug.Log("Use");
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        Debug.DrawRay(rayCaster.position, transform.TransformDirection(Vector3.forward * 3), Color.green);
        if (Physics.Raycast(rayCaster.position, transform.TransformDirection(Vector3.forward * 3), out hit, 3f))
        {
            Debug.Log("I shot " + hit.transform.gameObject.name);
            Rigidbody rb = hit.transform.gameObject.GetComponent<Rigidbody>();


            rb.AddForce(Vector3.forward * 100, ForceMode.Impulse);
        }
    }
    public void AltUse()
    {
        Debug.Log("Alt Use");

    }
}

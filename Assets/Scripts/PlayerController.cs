using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private IItem heldItem;
    private IItem lastTouchedItem;

    private bool canPickup = false;

    // Start is called before the first frame update
    void Start()
    {   
        // delete this after creating pickup functionality.
        heldItem = this.transform.GetChild(0).GetChild(0).GetComponent<IItem>();
        this.GetComponent<Rigidbody>().useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0)) {
			heldItem.Use();
		}
        if(Input.GetKey(KeyCode.Mouse1)) {
			heldItem.AltUse();
		}
        if(Input.GetKey(KeyCode.E))
        {
            if(canPickup)
            {
                heldItem = lastTouchedItem;
                heldItem.Pickup();
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            canPickup = true;
            lastTouchedItem = other.gameObject.GetComponent<IItem>();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            canPickup = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private IItem heldItem;
    public GameObject lastTouchedItem;

    private Transform hand;

    private bool canPickup = false;

	public List<GameObject> inv;
	// if I pick up a new item while I am holding an item,
	// move my held Item to the inventory.
	// if I drop an item but still have an item in inventory,
	// make that item active.
	// if I press Tab, select the next item in the inventory.


    // Start is called before the first frame update
    void Start()
    {   
        // This is the position you want to hold items at.
    	  hand = this.transform.GetChild(0).GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(heldItem != null)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                heldItem.Use();
            }
            if (Input.GetKey(KeyCode.Mouse1))
            {
                heldItem.AltUse();
            }
            if(Input.GetKey(KeyCode.Q))
            {
                heldItem.Drop();
            }
        }
        if(Input.GetKey(KeyCode.E))
        {
            Pickup();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            Debug.Log("Enter lastTouchedItem = " + lastTouchedItem);
            Debug.Log("I can pickup an item");
            canPickup = true;
            lastTouchedItem = other.gameObject;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            Debug.Log("I cannot pickup an item");
            canPickup = false;
            lastTouchedItem = null;
        }
    }


    void Pickup()
    {
        if (canPickup)
        {
            if (heldItem != null)
            {
                heldItem.Drop();
                heldItem = null;
            }
            // what if we are already holding an item?
            Debug.Log("I am trying to pick up item");
            Debug.Log("lastTouchedItem = " + lastTouchedItem);
            heldItem = lastTouchedItem.GetComponent<IItem>();
            //lastTouchedItem = null;
            heldItem.Pickup(hand);
        }
    }
}
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

    // Start is called before the first frame update
    void Start()
    {   
        // This is the position you want to hold items at.
    	hand = this.transform.GetChild(0).GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
		if(heldItem != null) {
			if(Input.GetKey(KeyCode.Mouse0)) {
				heldItem.Use();
			}
			if(Input.GetKey(KeyCode.Mouse1)) {
				heldItem.AltUse();
			}
			if(Input.GetKey(KeyCode.Q)){
				heldItem.Drop();
				heldItem = null;
			}
		}

		if(Input.GetKey(KeyCode.E)) {
			Pickup();
		}
	}

	void Pickup() {
		if(canPickup) {
			Debug.Log("lasttoucheditem = " + lastTouchedItem);
			// what if we are already holding an item? (drop it!)
			if(heldItem != null) {
				heldItem.Drop();
				heldItem = null;
			}
			Debug.Log("I am trying to pick up an item.");
			heldItem = lastTouchedItem.GetComponent<IItem>();
			//lastTouchedItem = null;
			heldItem.Pickup(hand);
		}
	}

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Item")) {
            Debug.Log("I can pickup an item");
            canPickup = true;
            lastTouchedItem = other.gameObject;
			Debug.Log("lasttoucheditem = " + lastTouchedItem);
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Item")) {
            Debug.Log("I can not pickup an item");
            canPickup = false;
            lastTouchedItem = null;
        }
    }
}

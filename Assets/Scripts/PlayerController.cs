using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private IItem heldItem;

	public List<GameObject> inv;
	// if I pick up a new item while I am holding an item,
	// move my held Item to the inventory.
	// if I drop an item but still have an item in inventory,
	// make that item active.
	// if I press Tab, select the next item in the inventory.

    public GameObject lastTouchedItem;

	private Transform hand;

	public bool debug = false;

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
			if(Input.GetKeyDown(KeyCode.Q)){
				heldItem.Drop();
				inv.Remove(inv[inv.Count - 1]);
				heldItem = hand.GetChild(inv.Count - 1).GetComponent<IItem>();
				hand.GetChild(inv.Count - 1).gameObject.SetActive(true);
			}
			if(Input.GetKey(KeyCode.Tab)) {
				SwitchItem();
			}
		}

		if(Input.GetKey(KeyCode.E)) {
			Pickup();
		}
	}

	void Pickup() {
		if(canPickup) {
			Debug.Log("lasttoucheditem = " + lastTouchedItem);
			// what if we are already holding an item? (hide it!)
			if(heldItem != null) {
				// heldItem.Drop();			// change this it.
				heldItem = null;
				hand.GetChild(inv.Count - 1).gameObject.SetActive(false);
				
			}
			Debug.Log("I am trying to pick up an item.");
			heldItem = lastTouchedItem.GetComponent<IItem>();

			inv.Add(lastTouchedItem);			// there will be a lot of getComponent happening.

			// if(debug) {
			// 	Debug.Log("Your inventory size = " + inv.Count);
			// }
			//lastTouchedItem = null;
			heldItem.Pickup(hand);

			// resetting lastTouchedItem and canPickup.
			canPickup = false;
			lastTouchedItem = null;
		}
	}

	void SwitchItem() {
		
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

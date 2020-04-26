using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : MonoBehaviour, IItem
{
	Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody>();
		this.gameObject.tag = "Item";
		this.transform.Rotate(0, Random.Range(0f,360f), 0);
	}

    public void Use(){}
	public void AltUse(){}

	public void Pickup(Transform hand){
		Debug.Log("I am picked up!");
		this.transform.SetParent(hand);
		this.transform.localPosition = Vector3.zero;
		this.transform.localRotation = Quaternion.identity;
		rb.isKinematic = true;
		Destroy(this.GetComponent<SpinItem>());	
	}

	public void Drop(){
		Debug.Log("Dropping heldItem");
		this.transform.SetParent(null);
		// throw item
		rb.isKinematic = false;
		rb.AddRelativeForce(Vector3.forward * 5, ForceMode.Impulse);
	}
}

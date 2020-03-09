using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private IItem heldItem;

    // Start is called before the first frame update
    void Start()
    {   
        // delete this after creating pickup functionality.
        heldItem = this.transform.GetChild(0).GetChild(0).GetComponent<IItem>();
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
    }
}

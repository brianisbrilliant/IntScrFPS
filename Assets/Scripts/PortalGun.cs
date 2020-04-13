using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun : MonoBehaviour, IItem
{
    public GameObject purpPortal;
    public GameObject bluePortal;

    public GameObject firePoint;
    //public GameObject mainCam;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
      rb= this.GetComponent<Rigidbody>();  
      purpPortal = GameObject.FindWithTag("PurpPortal");
      bluePortal = GameObject.FindWithTag("BluePortal");
      //mainCam = GameObject.FindWithTag("MainCamera");
    }
    public void Use(){
        //Debug.Log("Portal 1 aka Use()");
        ThrowPortal(purpPortal);

    }
    public void AltUse(){
       // Debug.Log("Portal 2 aka AltUse()");
        ThrowPortal(bluePortal);
        
    }
    public void Pickup(Transform hand){
        Debug.Log("I am Picked up!");
        
        this.transform.SetParent (hand);
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
        rb.isKinematic = true;
        Destroy(this.GetComponent<SpinItem>());

    }
    public void Drop(){
        Debug.Log("dropping held item");
        this.transform.SetParent(null);
        rb.isKinematic = false;
        rb.AddRelativeForce(Vector3.forward, ForceMode.Impulse);
        
    }

    void ThrowPortal(GameObject portal){
        RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit)){
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward *20), Color.green);
                if(hit.transform.gameObject.CompareTag("PortalWall")){
                   Debug.Log("Hit Portal Wall");
                    Quaternion hitObjectRotation = Quaternion.LookRotation(hit.normal);
                    portal.transform.position = hit.point; 
                    portal.transform.rotation =hitObjectRotation;
                }
                else{
                    Debug.Log("Miss");
                }
            }

    }
}

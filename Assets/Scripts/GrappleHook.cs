using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    public float maxTravelDistance = 50;
    public float currentTravelDistance;
    public float hookTravelSpeed;

    public bool hooked;
    public bool shot;
    
    public GameObject holster;
    public GameObject hook;
    public GameObject hookStartPos;
    public GameObject player;
    
    public Rigidbody hookRB;

    


    void Start()
    {
        shot = false;
        
    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0) && shot == false){
            shot = true;
        }

        if(shot){
            hook.transform.Translate(Vector3.forward * Time.deltaTime * hookTravelSpeed);
            //hookRB.AddRelatvieForce(Vector3.forward * Time.deltaTime * hookTravelSpeed);
            currentTravelDistance = Vector3.Distance(this.transform.position, hook.transform.position);
            if(currentTravelDistance >= maxTravelDistance){
                ReturnHook();
            }
            Debug.Log("You've shot me");
        }

        if(hooked){
            player.transform.position = Vector3.MoveTowards(player.transform.position, hook.transform.position, 10);
        }


        
    }


    void FireHook(){
        
    }

    void ReturnHook(){
        hook.transform.position = hookStartPos.transform.position;
        shot = false;
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("a")){
            hooked = true;
            Debug.Log("I am hooked");
        }
    }




}
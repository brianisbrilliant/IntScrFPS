using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    public float maxTravelDistance = 50;
    public float currentTravelDistance;
    public float hookTravelSpeed;
    public float playerTravelSpeed = 30f;

    public bool hooked;
    public bool shot;
    
    public GameObject holster;
    public GameObject hook;
    public GameObject hookStartPos;
    public GameObject player;
    
    public Rigidbody hookRB;
    public Rigidbody playerRB;

    


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
            currentTravelDistance = Vector3.Distance(this.transform.position, hook.transform.position);
            if(currentTravelDistance >= maxTravelDistance){
                ReturnHook();
            }
        }

        
        if(hooked){
            player.transform.position = Vector3.MoveTowards(player.transform.position, hook.transform.position, playerTravelSpeed);
            //playerRB.velocity = new Vector3(10f, 0, 10f);
            player.GetComponent<Rigidbody>().useGravity = false;
        }
        else{
            player.GetComponent<Rigidbody>().useGravity = true;
        }


        
    }


    void FireHook(){
        
    }

    void ReturnHook(){
        hook.transform.position = hookStartPos.transform.position;
        shot = false;
        hooked = false;
    }



}
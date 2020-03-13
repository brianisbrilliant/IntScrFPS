using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject hookedObj;
    
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
            FireHook();

            /*
            hook.transform.Translate(Vector3.forward * Time.deltaTime * hookTravelSpeed);
            currentTravelDistance = Vector3.Distance(this.transform.position, hook.transform.position);
            */
            if(currentTravelDistance >= maxTravelDistance){
                ReturnHook();
            }
        }

        
        if(hooked){
            hook.transform.parent = hookedObj.transform;
            player.transform.position = Vector3.MoveTowards(player.transform.position, hook.transform.position, playerTravelSpeed);
            playerRB.velocity = new Vector3(10f, -10, 10f);
            player.GetComponent<Rigidbody>().useGravity = false;
        }
        else{
            player.GetComponent<Rigidbody>().useGravity = true;
            hook.transform.parent = holster.transform;
        }


        
    }


    void FireHook(){
        RaycastHit hit;
            if(Physics.Raycast(hookStartPos.transform.position, transform.TransformDirection(Vector3.forward), out hit)){
                hook.transform.position = hit.point;
            }
            shot = false;
    }

    void ReturnHook(){
        hook.transform.position = hookStartPos.transform.position;
        shot = false;
        hooked = false;
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("restart")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }



}
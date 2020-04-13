using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepThruPortal : MonoBehaviour
{
    public Transform otherPortal;
    public Transform player;
    private bool playerIsOverlapping = false;
    // Start is called before the first frame update
    void Start()
    {
        if (this.tag == "PurpPortal"){
            otherPortal = GameObject.FindWithTag("BluePortal").transform;
        }
        else{
            otherPortal = GameObject.FindWithTag("PurpPortal").transform;
        }

        player = GameObject.FindWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsOverlapping){
            Debug.Log("Player is overlapping starting to teleport");
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct =  Vector3.Dot(transform.up, portalToPlayer);

            if (dotProduct < 0f){
                float rotationDiff = -Quaternion.Angle(transform.rotation, otherPortal.rotation);
                rotationDiff += 180;
                player.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                player.position = otherPortal.position * 2 + positionOffset;
                playerIsOverlapping = false;
                Debug.Log("Player is not overlapping + Teleported");
            }

        }
        
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            playerIsOverlapping = true;
            Debug.Log("Player is overlapping");

        }
    }
    void OnTriggerExit(Collider other){
        Debug.Log("Player Exitting");
        if(other.tag == "Player"){
            playerIsOverlapping = false;

        }
    }
}

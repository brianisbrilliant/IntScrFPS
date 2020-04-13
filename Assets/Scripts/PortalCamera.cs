using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform playerCam;
    public Transform portal;
    public Transform otherPortal;
    // Start is called before the first frame update
    void Start()
    {
        playerCam = GameObject.FindWithTag("MainCamera").transform;
        if (portal.tag == "PurpPortal"){
            otherPortal = GameObject.FindWithTag("BluePortal").transform;
        }
        else{
            otherPortal = GameObject.FindWithTag("PurpPortal").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 playerOffsetFromPortal = playerCam.position - otherPortal.position;
       // transform.position = portal.position - playerOffsetFromPortal;

       float angularDif = Quaternion.Angle(portal.rotation, otherPortal.rotation);
       Quaternion portalRotationDif = Quaternion.AngleAxis(angularDif, Vector3.up);

       Vector3 newCameraDirect = portalRotationDif * playerCam.forward;
       transform.rotation = Quaternion.LookRotation(newCameraDirect, Vector3.up);

        
    }
}

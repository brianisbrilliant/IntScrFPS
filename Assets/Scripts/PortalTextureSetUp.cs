using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetUp : MonoBehaviour
{

    public Camera  camB;
    public Material camMatB;
    public Camera  camP;
    public Material camMatP;
    // Start is called before the first frame update
    void Start()
    {
        camB = GameObject.FindWithTag("CamB").GetComponent<Camera>();
        if(camB.targetTexture != null){
            camB.targetTexture.Release();
        }
        camB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        camMatB.mainTexture = camB.targetTexture;

        camP = GameObject.FindWithTag("CamP").GetComponent<Camera>();
        if(camP.targetTexture != null){
            camP.targetTexture.Release();
        }
        camP.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        camMatP.mainTexture = camP.targetTexture;
        
    }

}

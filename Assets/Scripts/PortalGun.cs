using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun : MonoBehaviour, IItem
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Use(){
        Debug.Log("Portal 1 aka Use()");

    }
    public void AltUse(){
        Debug.Log("Portal 2 aka AltUse()");
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShot : MonoBehaviour, IItem
{
    // Start is called before the first frame update
    void Start()
    {
        Use();
        AltUse();
    }

    public void Use(){
        Debug.Log("Use()");
    }

    public void AltUse(){
        Debug.Log("AltUse()");
    }

    public void Pickup(Transform hand)
    {

    }

    public void Drop()
    {

    }


}

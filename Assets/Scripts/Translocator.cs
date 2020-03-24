using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translocator : MonoBehaviour, IItem
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Use() {
        Debug.Log("use");
    }
    public void AltUse() {
        Debug.Log("Alt use");
    }
}

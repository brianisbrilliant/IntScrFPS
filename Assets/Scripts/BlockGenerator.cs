using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour, IItem
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Use(){
        Debug.Log("Pow!! Main Use.");

    }

    public void AltUse(){
        Debug.Log("Pow!! Alt Use.");
        
    }
}

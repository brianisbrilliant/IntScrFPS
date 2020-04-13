using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinItem : MonoBehaviour
{
    [Range(0,360)]
    public float rotationSpeed = 30f;
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}

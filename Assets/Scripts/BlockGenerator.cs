using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour, IItem
{
    public GameObject BlockyBlock;

    private int count = 0;
    private Vector3 myMouse;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Use() {
        Debug.Log("Pow!! Main Use.");

        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit)){
            if(hit.rigidbody != null){
                Debug.Log("I got a wall!!!");
                //instantiate cube
                Instantiate(BlockyBlock, hit.point, Quaternion.identity);
                count++;
                if(count > 3){
                    GameObject[] objects;
                    objects = GameObject.FindGameObjectsWithTag("BlockyBlock");
                    Destroy(objects[0]);
                    count--;
                }
            }
        }
    }

    public void AltUse() {
        Debug.Log("Pow!! Alt Use.");

        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit)){
            if(hit.collider.tag == "BlockyBlock"){
                Debug.Log("Time to die BlockyBlock");
                //instantiate cube
                Destroy(hit.collider.gameObject);
                count--;
            }
            else if(hit.collider.tag == "wall"){
                Debug.Log("Cannot Kill Walls!!");
            }
        }
    }
}

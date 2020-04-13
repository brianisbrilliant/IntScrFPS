// MoveTo.cs
    using UnityEngine;
    using UnityEngine.AI;
    
    public class MoveTo : MonoBehaviour {

       [Tooltip("add the player here")]
       public Transform goal;
       public float followDist = 6f;

       private NavMeshAgent agent;
    
       
       void Start () {
          agent = GetComponent<NavMeshAgent>();
       }
       void Update(){
           float dist = Vector3.Distance(this.transform.position, goal.position);

           if (dist > followDist){
               agent.destination = goal.position;
           }
            else{
                agent.destination = this.transform.position;

            }
       }
    }
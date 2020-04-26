// MoveTo.cs
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour {
	
	[Tooltip("Add the player here!")]
	public Transform goal;

	public float followDistance = 10f;

	private NavMeshAgent agent;
	
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		
	}

	void Update() {
		float dist = Vector3.Distance(this.transform.position, goal.position);
		if(dist > followDistance) {
			agent.destination = goal.position;
		} else {
			agent.destination = this.transform.position;
		}
	}
}
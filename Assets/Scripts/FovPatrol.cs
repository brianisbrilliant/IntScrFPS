// Patrol.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class FovPatrol : MonoBehaviour {

	public float lookInterval;
	public Transform player;
	public Transform eye;			// ensure that it has no collider and is pointing "forward" relative to the AI.
	public float fieldOfView = 60;
	public float viewDistance = 15;

	public Transform[] points;
	private int destPoint = 0;
	private NavMeshAgent agent;

	void Start () {
		agent = GetComponent<NavMeshAgent>();

		// Disabling auto-braking allows for continuous movement
		// between points (ie, the agent doesn't slow down as it
		// approaches a destination point).
		agent.autoBraking = false;

		GotoNextPoint();

		StartCoroutine(LookForPlayer());
	}

	IEnumerator LookForPlayer() {
		while(true) {
			// find the correct direction for the ray from the eye to the player.
			Vector3 rayDirection = player.position - eye.position;
			//Debug.DrawRay(eye.position, rayDirection, Color.cyan, 1f);

			// calculate the direction of forward versus the direction of the player.
			float angle = Vector3.Angle(rayDirection, eye.forward);

			// if angle is less than 60, can we see the player?
			if(angle < fieldOfView) {
				RaycastHit hit;
				if(Physics.Raycast(eye.position, rayDirection, out hit, viewDistance)) {
					if(hit.collider.gameObject.CompareTag("Player")) {
						Debug.DrawRay(eye.position, rayDirection, Color.green, 0.5f);
						agent.destination = player.position;	// the AI automatically returns to patrolling after getting to the player's position.
					} else {
						Debug.DrawRay(eye.position, rayDirection, Color.red, 0.5f);
					}
				}
			}
			
			yield return new WaitForSeconds(lookInterval);
		}
	}


	void GotoNextPoint() {
		// Returns if no points have been set up
		if (points.Length == 0)
			return;

		// Set the agent to go to the currently selected destination.
		agent.destination = points[destPoint].position;

		// Choose the next point in the array as the destination,
		// cycling to the start if necessary.
		destPoint = (destPoint + 1) % points.Length;
	}


	void Update () {
		/*
			if(Playerisclose (5m)) {
				Move To player
			}
			if(playerisfar (15m)) {
				gotonextpoint();
			}
		*/

		// Choose the next destination point when the agent gets
		// close to the current one.
		if (!agent.pathPending && agent.remainingDistance < 1f)
			GotoNextPoint();
	}
}
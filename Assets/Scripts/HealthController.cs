using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthController : MonoBehaviour
{
    public int health = 10;

	void OnCollisionEnter(Collision other) {
		if(other.gameObject.CompareTag("Bullet")) {
			health -= 1;
			if(health <= 0) {
				Destroy(this.gameObject, 2f);
				if(this.GetComponent<NavMeshAgent>() != null) {
					Destroy(this.GetComponent<NavMeshAgent>());
					this.gameObject.AddComponent<Rigidbody>();
				}
			}
			Destroy(other.gameObject);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SlugThrower : MonoBehaviour, IItem
{
	private Transform bulletSpawn;

	[SerializeField]
	private Rigidbody bulletPrefab;

	[SerializeField]
	private float bulletSpeed = 50f;
	
	[Range(0.05f, 0.5f)]
	[SerializeField]
	private float interval = 0.1f;

	[SerializeField]
	private AudioClip shot;

	private AudioSource aud;
	private bool canFire = true;



    // Start is called before the first frame update
    void Start()
    {
        bulletSpawn = this.transform.GetChild(0);
		aud = this.GetComponent<AudioSource>();
    }

	public void Use() {
		if(canFire) {
			Debug.Log("Pow!");
			Rigidbody rb = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
			rb.AddRelativeForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);
			Destroy(rb.gameObject, 3f);

			// randomize the audiosource pitch for variety.
			aud.pitch = Random.Range(0.95f, 1.05f);
			// play sound clip
			aud.PlayOneShot(shot);
			StartCoroutine(WaitToFire());
		}
	}

	public void AltUse() {
		Debug.Log("Alt fire!");
	}

	IEnumerator WaitToFire() {
		canFire = false;
		yield return new WaitForSeconds(interval);
		canFire = true;
	}
}

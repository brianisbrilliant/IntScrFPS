using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlugThrower : MonoBehaviour
{
	private Transform bulletSpawn;

	[SerializeField]
	private Rigidbody bulletPrefab;

	[SerializeField]
	private float bulletSpeed = 50f;


    // Start is called before the first frame update
    void Start()
    {
        bulletSpawn = this.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)) {
			Use();
		}
    }

	public void Use() {
		Debug.Log("Pow!");
		Rigidbody rb = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
		rb.AddRelativeForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);
		Destroy(rb.gameObject, 1f);
	}
}

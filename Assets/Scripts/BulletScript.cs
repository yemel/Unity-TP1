using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	
	// Update is called once per frame
	public float bulletSpeed = 200;
	
	public void launch() {
		GameObject ship = GameObject.FindGameObjectWithTag("Ship");
		GameObject shipAxis = GameObject.FindGameObjectWithTag("ShipAxis");
		transform.position = ship.transform.position;
		transform.rotation = shipAxis.transform.rotation;
		rigidbody.velocity = ship.rigidbody.velocity;
		rigidbody.AddRelativeForce(0,0,bulletSpeed,ForceMode.Impulse);
		
	}
}

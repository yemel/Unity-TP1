using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {
	
	private float xMove, zMove;
	
	// Use this for initialization
	void Start () {
	
	}
	
	void OnEnable() {
		float cameraSize = (float) Camera.main.camera.orthographicSize + 1.0f;
		float xPos, zPos;

		float randX = (float) UnityEngine.Random.Range(5.0f, 10.0f);
		float randZ = (float) UnityEngine.Random.Range(5.0f, 10.0f);
		
		// Choose a random spot to spawn, always on a camera border
		float randomBorder = UnityEngine.Random.Range(0.0f, 1.0f);
		if(randomBorder < 0.5f) {
			xPos = (float) UnityEngine.Random.Range(-cameraSize, cameraSize);
			zPos = randomBorder > 0.25f ? -cameraSize : cameraSize;
			zMove = randomBorder > 0.25f ? randZ : -randZ;
			xMove = UnityEngine.Random.Range(0.0f, 1.0f) > 0.5f ? randX : -randX;
			xMove /= 2;
		} else {
			zPos = (float) UnityEngine.Random.Range(-cameraSize, cameraSize);
			xPos = randomBorder > 0.75f ? -cameraSize : cameraSize;
			xMove = randomBorder > 0.75f ? randX : -randX;
			zMove = UnityEngine.Random.Range(0.0f, 1.0f) > 0.5f ? randZ : -randZ;
			zMove /= 2;
		}
				
		this.transform.position = new Vector3(xPos, 0, zPos);
		
//		Debug.Log("xMove: " + xMove + " - " + "zMove: " + zMove);
		gameObject.rigidbody.velocity = new Vector3(xMove, 0, zMove);
//		gameObject.rigidbody.AddForce(new Vector3(xMove, 0, zMove) * 10, ForceMode.Impulse);		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x, 0, transform.position.z);
		
		// Deactivate the asteroid when it reachs a camera border
		float cameraSize = (float) Camera.main.camera.orthographicSize + 3.0f;
		if(this.transform.position.z < -cameraSize || this.transform.position.z > cameraSize ||
			this.transform.position.x < -cameraSize || this.transform.position.x > cameraSize) {
			this.gameObject.SetActiveRecursively(false);
		}
	}
	
	void OnTriggerEnter(Collider other) {
		Debug.Log("ouch with " + other.gameObject);
	}
}

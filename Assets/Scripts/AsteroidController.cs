using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {
	
	private float xMove, zMove;
	private BulletManagerScript bulletManagerScript;
	private AsteroidManagerScript asteroidManager;
	private GameManagerScript gameManager;
	private ShipController shipController;
	
	void Start () {
	}
	
	void OnEnable() {	
	}
	
	void Update () {
		asteroidManager = getAsteroidManager();
		
		transform.position = new Vector3(transform.position.x, 0, transform.position.z);
		
		// Deactivate the asteroid when it reachs a camera border
		float cameraSize = (float) Camera.main.camera.orthographicSize + 10.0f;
		if(this.transform.position.z < -cameraSize || this.transform.position.z > cameraSize ||
			this.transform.position.x < -cameraSize || this.transform.position.x > cameraSize) {
			asteroidManager.disableAsteroid(this.gameObject);
		}
	}
	
	void OnTriggerEnter(Collider other) {
		asteroidManager = getAsteroidManager();
		bulletManagerScript = getBulletManager();
		gameManager = getGameManager();
		
		if(other.CompareTag("Bullet")) {
			this.gameObject.active = false;
			GameObject bullet = other.gameObject.transform.parent.gameObject;
			bulletManagerScript.DisableBullet(bullet);
			GameObject explosion = (GameObject) Instantiate(Resources.Load("Explosion"), this.transform.position, this.transform.rotation);
			asteroidManager.hitAsteroid(this.gameObject);
		} else if(other.CompareTag("Ship")) {
			shipController = getShipController();
			shipController.crashShip();
		}
	}
	
	private AsteroidManagerScript getAsteroidManager() {
		if(asteroidManager == null) {
			asteroidManager = (AsteroidManagerScript) GameObject.FindGameObjectWithTag("AsteroidManager").GetComponent(typeof(AsteroidManagerScript));
		}
		
		return asteroidManager;
	}
	
	private BulletManagerScript getBulletManager() {
		if(bulletManagerScript == null) {
			bulletManagerScript = (BulletManagerScript) GameObject.FindGameObjectWithTag("BulletManager").GetComponent(typeof(BulletManagerScript));		}
		
		return bulletManagerScript;
	}
	
	public void setRandomBorderPosition() {
		float cameraSize = (float) Camera.main.camera.orthographicSize + 1.0f;
		float xPos, zPos;

		float randX = (float) UnityEngine.Random.Range(10.0f, 12.0f);
		float randZ = (float) UnityEngine.Random.Range(10.0f, 12.0f);
		
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
	
	private GameManagerScript getGameManager() {
		if(gameManager == null) {
			gameManager = (GameManagerScript) GameObject.FindGameObjectWithTag("GameManager").GetComponent(typeof(GameManagerScript));
		}
		
		return gameManager;
	}

	private ShipController getShipController() {
		if(shipController == null) {
			shipController = (ShipController) GameObject.FindGameObjectWithTag("Ship").GetComponent(typeof(ShipController));
		}
		
		return shipController;
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
	
public class AsteroidController : MonoBehaviour {
	
	private float xMove, zMove;
	private BulletManagerScript bulletManagerScript;
	private AsteroidManagerScript asteroidManager;
	private GameManagerScript gameManager;
	private ShipController shipController;
	
	void Start () {
	}
	
	void Update () {
		asteroidManager = getAsteroidManager();
		
		transform.position = new Vector3(transform.position.x, 0, transform.position.z);

		Vector3 cameraBounds = Camera.main.camera.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,0));
		cameraBounds += new Vector3(40F, 0, 40F);

		if(this.transform.position.z < -cameraBounds.z || this.transform.position.z > cameraBounds.z ||
			this.transform.position.x < -cameraBounds.x || this.transform.position.x > cameraBounds.x) {
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
			Vector3 newPos = new Vector3(transform.position.x, 20, transform.position.z);
			asteroidManager.instantiateExplotion(newPos);
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
		Vector3 cameraBounds = Camera.main.camera.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,0));
		cameraBounds += new Vector3(20F, 0, 20F);
		
		float xPos, zPos;

		float randX = (float) Random.Range(12.0f, 15.0f);
		float randZ = (float) Random.Range(12.0f, 15.0f);
		
		float randomBorder = Random.Range(0.0f, 1.0f);
		if(randomBorder < 0.5f) {
			xPos = (float) Random.Range(-cameraBounds.x, cameraBounds.x);
			zPos = randomBorder > 0.25f ? -cameraBounds.z : cameraBounds.z;
			zMove = randomBorder > 0.25f ? randZ : -randZ;
			xMove = Random.Range(0.0f, 1.0f) > 0.5f ? randX : -randX;
			xMove /= 2;
		} else {
			zPos = (float) Random.Range(-cameraBounds.z, cameraBounds.z);
			xPos = randomBorder > 0.75f ? -cameraBounds.x : cameraBounds.x;
			xMove = randomBorder > 0.75f ? randX : -randX;
			zMove = Random.Range(0.0f, 1.0f) > 0.5f ? randZ : -randZ;
			zMove /= 2;
		}
				
		this.transform.position = new Vector3(xPos, 0, zPos);
		
		gameObject.rigidbody.velocity = new Vector3(xMove, 0, zMove);
		gameObject.rigidbody.AddTorque(new Vector3(Random.Range(10F, 20F),Random.Range(10F, 20F),Random.Range(10F, 20F)));
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

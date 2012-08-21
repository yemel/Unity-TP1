using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {
	
	private GameManagerScript gameManager;
	private BulletManagerScript bulletManager;	
	
	public ParticleSystem mainThrust;
	public float movementForce = 12;
	public float breakForce = 1;
	public float deltaTurn = 1;
	
	private static float crashLimit = 5F;
	private float lastCrashed;
	
	void Start() {
		lastCrashed = Time.timeSinceLevelLoad;
	}
	
	void Update () {
		updateMovement();
		if(Input.GetKeyUp(KeyCode.Space)){
			getBulletManager().doShot();
		}
	}
	
	private void updateMovement(){
		float movement 	= Input.GetAxis("Vertical");
		float rotation 	= Input.GetAxis("Horizontal");
		bool isBreaking = Input.GetButton("Break");
		
		if(movement > 0){
			rigidbody.AddRelativeForce(0,-movementForce,0);
			mainThrust.Emit(0);
		} else {
			mainThrust.Emit(-1);
		}
		
		if(rotation != 0){
			float turn = Mathf.Sign(rotation)*deltaTurn;
			transform.Rotate(0,0,turn);
		}
		
		if(isBreaking){
			rigidbody.AddForce(rigidbody.velocity * (-breakForce));
			rigidbody.AddTorque(rigidbody.angularVelocity * (-breakForce));
		}
	}
	
	public void crashShip() {
		if((Time.timeSinceLevelLoad - lastCrashed) < crashLimit) return;

		gameManager = getGameManager();
		
		gameManager.decrementLives();
		if(gameManager.getCurrentLives() > 0) {		
			GameObject explosion = (GameObject) Instantiate(Resources.Load("Explosion"), gameObject.transform.position, gameObject.transform.rotation);
			gameObject.transform.position = Vector3.zero;
			gameObject.rigidbody.velocity = Vector3.zero;
			lastCrashed = Time.timeSinceLevelLoad;
		} else {
			DestroyObject(this.gameObject);
		}
		
	}
	
	private GameManagerScript getGameManager() {
		if(gameManager == null) {
			gameManager = (GameManagerScript) GameObject.FindGameObjectWithTag("GameManager").GetComponent(typeof(GameManagerScript));
		}
		
		return gameManager;
	}

	private BulletManagerScript getBulletManager() {
		if(bulletManager == null) {
			bulletManager = (BulletManagerScript) GameObject.FindGameObjectWithTag("BulletManager").GetComponent(typeof(BulletManagerScript));
		}
		
		return bulletManager;
	}
}

using UnityEngine;
using System.Collections.Generic;

public class AsteroidManagerScript : MonoBehaviour {

	private List<GameObject> enableAsteroids;
	private List<GameObject> disableAsteroids;	
	private int asteroidsCache = 50;
	
	public float spawnTwoAsteroidsProb;
	public float spawnFourAsteroidsProb;
	
	public float changeProabilityTime;
	public float lastChangeProabilityTime;
	public float spawnTime;
	private float lastSpawnTime;
	
	private Vector3 bigScale, mediumScale, smallScale;
	
	private static int smallScore = 277;
	private static int mediumScore = 184;
	private static int bigScore = 75;

	private GameManagerScript gameManager;
	
	// Use this for initialization
	void Start () {
		spawnTwoAsteroidsProb = 0.25F;
		spawnFourAsteroidsProb = 0.1F;
		
		spawnTime = 5F;
		lastSpawnTime = 0F;
		
		changeProabilityTime = 30F;
		lastChangeProabilityTime = 0F;
		
		bigScale = new Vector3(32, 17, 17);
		mediumScale = new Vector3(18, 10, 10);
		smallScale = new Vector3(5, 3, 3);
		
		enableAsteroids = new List<GameObject>(asteroidsCache);
		disableAsteroids = new List<GameObject>(asteroidsCache);

		for(int i = 0; i < asteroidsCache; i++) {
			GameObject currAsteroid = (GameObject) Instantiate(Resources.Load("AsteroidPrefab"));
			currAsteroid.active = false;
			disableAsteroids.Add(currAsteroid);
        }
	}
	
	void Update () {
		if(getGameManager().getCurrentLives() == 0) return; 
		
		lastChangeProabilityTime += Time.deltaTime;
		if(lastChangeProabilityTime >= changeProabilityTime && spawnTwoAsteroidsProb < 0.8F) {
			lastChangeProabilityTime = 0F;
			spawnTwoAsteroidsProb += 0.1F;
			spawnFourAsteroidsProb += 0.1F;
		}
		
		lastSpawnTime += Time.deltaTime;
		
		if(lastSpawnTime >= spawnTime) {
			lastSpawnTime = 0F;
			if(spawnTime > 3F) {
				spawnTime -= 0.08F;
			}
			
			spawnNewAsteroid();
			float randSpawn = Random.Range(0F,1F);
			if(randSpawn < spawnTwoAsteroidsProb) {
				spawnNewAsteroid();
			} 
			if(randSpawn < spawnFourAsteroidsProb) {
				spawnNewAsteroid();
				spawnNewAsteroid();
			}
		}
	}
	
	private void spawnNewAsteroid() {
		GameObject currAsteroid = getAsteroid();
		AsteroidController asteroidController = (AsteroidController) currAsteroid.GetComponent(typeof(AsteroidController));
		asteroidController.setRandomBorderPosition();
		currAsteroid.transform.localScale = Random.Range(0F,1F) > 0.5f ? bigScale : mediumScale;
//		print(testPosition(asteroidController.transform.position));
	}
	
	private bool testPosition(Vector3 pos) {
		RaycastHit hit;
		GameObject rayCast = GameObject.FindGameObjectWithTag("RayCast");
		rayCast.transform.position = new Vector3(pos.x, 40, pos.z);
		return rayCast.rigidbody.SweepTest(new Vector3(0, -1, 0), out hit, 60);
	}
 	
	public void hitAsteroid(GameObject asteroid) {
		int newScore = 0;
		Vector3 spawnDirection = Vector3.zero;
		float xRand = Random.Range(0.0F,1F);
		float zRand = Random.Range(0.0F,1F);
		float vel   = Random.Range(10F, 20F);
		if(asteroid.transform.localScale.Equals(bigScale)) {
			newScore = bigScore;
			for(int i = 0; i < 3; i++) {
				GameObject currAsteroid = getAsteroid();
				currAsteroid.transform.localScale = mediumScale;
				currAsteroid.transform.position = asteroid.transform.position;
				if(i == 0) spawnDirection = new Vector3(xRand,0,zRand);
				if(i > 0)  spawnDirection = new Vector3(-xRand,0,-zRand);
				if(i > 1)  spawnDirection = new Vector3(xRand,0,-zRand);
				currAsteroid.rigidbody.AddForce(spawnDirection.normalized * vel, ForceMode.Impulse);
				currAsteroid.rigidbody.AddTorque(randomVector3(10F, 20F));
				currAsteroid.transform.rotation = Quaternion.Euler(randomVector3(0F, 180F));
			}
		} else if(asteroid.transform.localScale.Equals(mediumScale)) {
			newScore = mediumScore;
			for(int i = 0; i < 3; i++) {
				GameObject currAsteroid = getAsteroid();
				currAsteroid.transform.localScale = smallScale;
				currAsteroid.transform.position = asteroid.transform.position;
				if(i == 0) spawnDirection = new Vector3(xRand,0,zRand);
				if(i > 0)  spawnDirection = new Vector3(-xRand,0,-zRand);
				if(i > 1)  spawnDirection = new Vector3(xRand,0,-zRand);
				currAsteroid.rigidbody.AddForce(spawnDirection.normalized * vel, ForceMode.Impulse);
				currAsteroid.rigidbody.AddTorque(randomVector3(10F, 20F));
				currAsteroid.transform.rotation = Quaternion.Euler(randomVector3(0F, 180F));
			}
		} else {
			newScore = smallScore;
		}
		
		gameManager = getGameManager();
		gameManager.addScore(newScore);
	}
	
	private GameObject getAsteroid() {
		GameObject currAsteroid;
		if(disableAsteroids.Count == 0) {
			currAsteroid = (GameObject) Instantiate(Resources.Load("AsteroidPrefab"));
		} else {
			currAsteroid = disableAsteroids[0];
			disableAsteroids.RemoveAt(0);			
		}
		enableAsteroids.Add(currAsteroid);
		currAsteroid.SetActiveRecursively(true);

		return currAsteroid;
	}
	
	public void disableAsteroid(GameObject asteroid) {
		asteroid.SetActiveRecursively(false);
		enableAsteroids.Remove(asteroid);
		disableAsteroids.Add(asteroid);
	}
	
	private GameManagerScript getGameManager() {
		if(gameManager == null) {
			gameManager = (GameManagerScript) GameObject.FindGameObjectWithTag("GameManager").GetComponent(typeof(GameManagerScript));
		}
		
		return gameManager;
	}
	
	private Vector3 randomVector3(float min, float max) {
		return new Vector3(Random.Range(min, max),Random.Range(min, max),Random.Range(min, max));
	}

}

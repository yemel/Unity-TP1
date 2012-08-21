using UnityEngine;
using System.Collections.Generic;

public class AsteroidManagerScript : MonoBehaviour {

	private List<GameObject> enableAsteroids;
	private List<GameObject> disableAsteroids;
	
	public int asteroidsCache = 20;
	public float spawnTime;
	
	private Vector3 bigScale, mediumScale, smallScale;
	private float lastSpawnTime;
	
	// Use this for initialization
	void Start () {
		spawnTime = 5F;
		lastSpawnTime = 0F;
		
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
	
	// Update is called once per frame
	void Update () {
		lastSpawnTime += Time.deltaTime;
		if(lastSpawnTime >= spawnTime) {
			lastSpawnTime = 0F;
			GameObject currAsteroid = getAsteroid();
			AsteroidController asteroidController = (AsteroidController) currAsteroid.GetComponent(typeof(AsteroidController));
			asteroidController.setRandomBorderPosition();
			currAsteroid.transform.localScale = Random.Range(0F,1F) > 0.5f ? bigScale : mediumScale;				
		}
	}
	
	public void hitAsteroid(GameObject asteroid) {
		Quaternion rotationVector = Quaternion.AngleAxis(90, Vector3.forward);
		float xRand = Random.Range(0.75F,1F);
		float zRand = Random.Range(0.75F,1F);				
		if(asteroid.transform.localScale.Equals(bigScale)) {
			for(int i = 0; i < 2; i++) {
				GameObject currAsteroid = getAsteroid();
				currAsteroid.transform.localScale = mediumScale;
				currAsteroid.transform.position = asteroid.transform.position;
				Vector3 spawnDirection = new Vector3(xRand * 20F,0,zRand * 20F);
				if(i == 1) spawnDirection = rotationVector * spawnDirection;
				currAsteroid.rigidbody.AddForce(spawnDirection, ForceMode.Impulse);
			}
		} else if(asteroid.transform.localScale.Equals(mediumScale)) {
			for(int i = 0; i < 3; i++) {
				GameObject currAsteroid = getAsteroid();
				currAsteroid.transform.localScale = smallScale;
				currAsteroid.transform.position = asteroid.transform.position;
				currAsteroid.rigidbody.AddForce(xRand * 5F,0,zRand * 5F, ForceMode.Impulse);
			}
		}
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
		
}

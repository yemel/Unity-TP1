using UnityEngine;
using System.Collections;

public class AsteroidManager : MonoBehaviour {

	private GameObject[] asteroids;
	
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
		
		asteroids = new GameObject[asteroidsCache];

		for(int i = 0; i < asteroids.Length; i++){
			asteroids[i] = (GameObject) Instantiate(Resources.Load("AsteroidPrefab"));
			asteroids[i].active = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
		lastSpawnTime += Time.deltaTime;
		if(lastSpawnTime >= spawnTime) {
			lastSpawnTime = 0F;
	        for(int i = 0; i < asteroids.Length; i++){
				if(asteroids[i].active == false) {
					asteroids[i].SetActiveRecursively(true);
					asteroids[i].transform.localScale = Random.Range(0F,1F) > 0.5f ? bigScale : mediumScale;
					break;
				}
	        }

		}	
	}
}

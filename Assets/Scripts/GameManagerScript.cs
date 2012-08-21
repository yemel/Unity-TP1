using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {
	
	private int highScore;
	private int currentScore;
	private int currentLives;
	
	// Use this for initialization
	void Start () {
		highScore = 0;
		currentScore = 0;
		currentLives = 3;
	}
	
	// Update is called once per frame
	void Update () {
		print (currentScore + " " + highScore);
	}
	
	public int getHighScore() {
		return highScore;
	}
	
	public int getCurrentScore() {
		return currentScore;
	}
	
	public void addScore(int newScore) {
		currentScore += newScore;
		if(currentScore > highScore) {
			highScore = currentScore;
		}
	}

	public int getCurrentLives() {
		return currentLives;
	}
	
	public void decrementLives() {
		currentLives--;
	}
	
}

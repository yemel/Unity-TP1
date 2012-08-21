using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {
	
	private int highScore;
	private int currentScore;
	private int currentLives;
	
	// Use this for initialization
	void Start () {
		highScore = PlayerPrefs.GetInt("highScore", 0);
		currentScore = 0;
		currentLives = 3;
	}
	
	void Update () {
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
			PlayerPrefs.SetInt("highScore", highScore);
		}
	}

	public int getCurrentLives() {
		return currentLives;
	}
	
	public void decrementLives() {
		currentLives--;
	}
	
	void OnGUI () {
		GUI.Label(new Rect(Screen.width - 160, Screen.height - 40 , 160, 40), "High Score: " + highScore);
		GUI.Label(new Rect(20, Screen.height - 40 , 190, 30), "Score: " + currentScore);
		GUI.Label(new Rect(20, 20, 160, 40), "Lives: "+ currentLives);
		
		if(currentLives == 0) {
			GUI.Label(new Rect((Screen.width-160)/2, (Screen.height-40)/2 , 160, 40), "Game Over. Play again?");
			if(GUI.Button(new Rect((Screen.width-160)/2, (Screen.height-40)/2+60 , 160, 40), "Restart")) {
				Application.LoadLevel("playScene");
			}
			if(GUI.Button(new Rect((Screen.width-160)/2, (Screen.height-40)/2+120 , 160, 40), "Main menu")) {
				Application.LoadLevel("mainMenu");
			}
		}
	}
}

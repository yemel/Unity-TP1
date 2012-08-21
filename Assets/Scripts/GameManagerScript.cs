using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {
	
	private int highScore;
	private int currentScore;
	private int currentLives;
	
	public GUISkin mySkin;
	
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
		if(currentLives > 0) currentLives--;
	}
	
	void OnGUI () {
		GUIStyle style = new GUIStyle();
		style.fontSize = 18;
		style.font = mySkin.font;
		style.normal.textColor = Color.white;
	
		GUI.Label(new Rect(20, 20, 100, 40), "Lives: "+ currentLives, style);
		GUI.Label(new Rect(150, 20, 160, 40), "Score: " + currentScore, style);
		GUI.Label(new Rect(Screen.width - 300, 20, 300, 40), "High Score: " + highScore, style);
		
		if(currentLives == 0) {
			style.alignment = TextAnchor.MiddleCenter;
			style.fontSize = 70;
			GUI.Label(new Rect((Screen.width-500)/2, (Screen.height+150)/2, 500, 100), "Game Over", style);
			style.fontSize = 25;
			GUI.Label(new Rect((Screen.width-500)/2, (Screen.height+300)/2, 500, 100), "Press any key to play again", style);
			if(Input.anyKeyDown){
				Application.LoadLevel("playScene");
			}
		}
	}
}

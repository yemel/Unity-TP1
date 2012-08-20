using UnityEngine;
using System.Collections;

public class TopBarController : MonoBehaviour {

	public int currentLifes;
	public int currentScore;
	public int currentLevel;
	
	public GUIText lifeGUIText;
	GUIText scoreGUIText;
	GUIText levelGUIText;
	// Use this for initialization
	void Start () {
		currentLifes = 3;
		currentScore = 0;
		currentLevel = 1;
		
		
		//string lifeLabel = "Vidas: ";
		//lifesTopBarLabel.text = lifeLabel;
		//lifesTopBarLabel.fontStyle = FontStyle.Bold;
	}
	
	void Update () {
		
	}
}

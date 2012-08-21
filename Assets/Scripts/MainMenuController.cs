using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {
	public GUISkin mySkin;
	public Texture2D background;
	public static float currentDPI;
	public static int buttonWidth = 180;
	public static int buttonHeight = 20;
	public static int menuHeight = 200;
	public static int menuWidth = buttonWidth + 40;
	public static int menuInitHorizontalPos = 0;
	public static int menuInitVerticalPos = 0;
	public static int menuHorizontalMargin = 20;
	public static int menuVerticalMargin = buttonHeight + 20;
	
	private void OnGUI () {	
		GUIStyle style = new GUIStyle();
		style.fontSize = 80;
		style.font = mySkin.font;
		style.alignment = TextAnchor.MiddleCenter;
		style.normal.textColor = Color.white;
		
		GUI.Label(new Rect((Screen.width/2)-250, 30, 500, 120), "ASTEROIDS", style);
		
		style.fontSize = 25;
		style.normal.textColor = Color.yellow;
		GUI.Label(new Rect((Screen.width-500)/2, 80, 500, 200), "Press any key to play", style);
	}

	void Update () {
		if(Input.anyKeyDown){
			Application.LoadLevel("playScene");
		}
	}
	
	public void centerPosition(int height, int width, out int xPos, out int yPos){
		xPos = (Screen.width-width)/2;
		yPos = (Screen.height-height)/2;
	}
}

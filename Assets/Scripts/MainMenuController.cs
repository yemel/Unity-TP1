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
		//int xpos = (Screen.currentResolution.width-menuHeight)/2;
		//int ypos = (Screen.currentResolution.height-menuWidth)/2;
		int xPos, yPos;
		centerPosition(menuHeight, menuWidth, out xPos, out yPos);
		GUI.BeginGroup( new Rect ( xPos, yPos, menuWidth, menuHeight));
			
			GUI.Box(new Rect(menuInitHorizontalPos, menuInitVerticalPos, menuWidth, menuHeight), "Frogee");
			
			// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
			if(GUI.Button(new Rect(menuHorizontalMargin, menuVerticalMargin,buttonWidth,buttonHeight), "Iniciar Juego")) {
				Application.LoadLevel("playScene");
			}
	
			// Make the second button.
			if(GUI.Button(new Rect(menuHorizontalMargin,menuVerticalMargin * 2,buttonWidth,buttonHeight), "Reglas del Juego")) {
				Application.LoadLevel(2);
			}
			
			// Make the second button.
			if(GUI.Button(new Rect(menuHorizontalMargin,menuVerticalMargin * 3,buttonWidth,buttonHeight), "Scores")) {
				Application.LoadLevel(2);
			}
			
			// Make the second button.
			if(GUI.Button(new Rect(menuHorizontalMargin,menuVerticalMargin * 4,buttonWidth,buttonHeight), "Salir")) {
				Application.Quit();
			}
		GUI.EndGroup();
	}
	
	public void centerPosition(int height, int width, out int xPos, out int yPos){
		xPos = (Screen.currentResolution.width-height)/2;
		yPos = (Screen.currentResolution.height-width)/2;
	}
}

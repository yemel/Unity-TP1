using UnityEngine;
using System.Collections;

public class TimerController : MonoBehaviour {

	float startTime;
	float timeToPlay;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
		//Seconds
		timeToPlay = 60;
	}
	
	// Update is called once per frame
	void OnGUI () {
		float guiTime = timeToPlay - (Time.time - startTime);
		print ("Tiempo = " + guiTime /60);
   		int minutes = (int) guiTime / 60;
   		int seconds =(int) guiTime % 60;
   		int fraction = (int) (guiTime * 100) % 100;
		if(guiTime >= 0){
			string text = string.Format ("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction); 
			GUI.Label(new Rect((Screen.currentResolution.width-100)/2, 1, 100, 30),text);
		}
	}
	
	public float getTime(){
	 return Time.time - startTime;
	}
}

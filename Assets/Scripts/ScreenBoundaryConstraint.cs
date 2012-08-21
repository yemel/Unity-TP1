using UnityEngine;
using System.Collections;

public class ScreenBoundaryConstraint : MonoBehaviour {

	void Update () {
		Vector3 cameraBounds = Camera.main.camera.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,0));
		Vector3 pos = transform.position;
		if(pos.z < -cameraBounds.z) pos.Set(pos.x, pos.y, cameraBounds.z);
		if(pos.z > cameraBounds.z) pos.Set(pos.x, pos.y, -cameraBounds.z);
		if(pos.x < -cameraBounds.x) pos.Set(cameraBounds.x, pos.y, pos.z);
		if(pos.x > cameraBounds.x) pos.Set(-cameraBounds.x, pos.y, pos.z);
		transform.position = new Vector3(pos.x, pos.y, pos.z);
	}
}

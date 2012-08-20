using UnityEngine;
using System.Collections;

public class ScreenBoundaryConstraint : MonoBehaviour {

	void Update () {
		Vector3 pos = transform.position;
		float cameraSize = (float) Camera.main.camera.orthographicSize + 3.0f;
		if(pos.z < -cameraSize) pos.Set(pos.x, pos.y, cameraSize);
		if(pos.z > cameraSize) pos.Set(pos.x, pos.y, -cameraSize);
		if(pos.x < -cameraSize) pos.Set(cameraSize, pos.y, pos.z);
		if(pos.x > cameraSize) pos.Set(-cameraSize, pos.y, pos.z);
		transform.position = new Vector3(pos.x, pos.y, pos.z);
	}
}

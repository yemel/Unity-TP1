using UnityEngine;
using System.Collections;

public class MainMenuShipController : MonoBehaviour {
	
	private float move = 0.005F;
	private float changeDelay = 5F;
	private float lastChange = 0F;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		lastChange += Time.deltaTime;
		if(lastChange >= changeDelay) {
			move = -move;
			lastChange = 0F;
		}
		this.gameObject.transform.position += new Vector3(move, move, 0);
//		this.gameObject.transform.rotation = Quaternion.Euler(new Vector3(move, move, 0));
	}
}

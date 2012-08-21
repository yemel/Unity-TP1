using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour {
	
	public float lifeTime = 1.5F;
	private float lastTime;
	
	void Start () {
		lastTime = 0F;
	}
	
	void Update () {
		lastTime += Time.deltaTime;
		if(lastTime >= lifeTime) {
			DestroyObject(this.gameObject);
		}
	}
}

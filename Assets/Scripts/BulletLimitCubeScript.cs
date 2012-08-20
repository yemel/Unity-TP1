using UnityEngine;
using System.Collections;

public class BulletLimitCubeScript : MonoBehaviour {

	void OnTriggerExit(Collider collider){
		//The bullets are going out
		BulletManagerScript bulletManagerScript = (BulletManagerScript) GameObject.FindGameObjectWithTag("BulletManager").GetComponent(typeof(BulletManagerScript));
		GameObject bullet = collider.gameObject.transform.parent.gameObject;
		bulletManagerScript.DisableBullet(bullet);
	}
	
}

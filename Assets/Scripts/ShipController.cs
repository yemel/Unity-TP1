using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {
	
	public ParticleSystem mainThrust;
	public float movementForce = 12;
	public float breakForce = 1;
	public float deltaTurn = 1;
	
	void Update () {
		updateMovement();
	}
	
	private void updateMovement(){
		float movement 	= Input.GetAxis("Vertical");
		float rotation 	= Input.GetAxis("Horizontal");
		bool isBreaking = Input.GetButton("Break");
		
		if(movement > 0){
			rigidbody.AddRelativeForce(0,-movementForce,0);
			mainThrust.Emit(0);
		} else {
			mainThrust.Emit(-1);
		}
		
		if(rotation != 0){
			float turn = Mathf.Sign(rotation)*deltaTurn;
			transform.Rotate(0,0,turn);
		}
		
		if(isBreaking){
			rigidbody.AddForce(rigidbody.velocity * (-breakForce));
			rigidbody.AddTorque(rigidbody.angularVelocity * (-breakForce));
		}
	}	
}

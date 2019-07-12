using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidMoveVelocityY : MonoBehaviour
{
	//Give a oneTime velocity in y direction

	private Rigidbody rb;			
	public Vector2 speed;

	void Start()
	{
		rb = this.GetComponent<Rigidbody>();

		float randomNumb = Random.Range(speed.x, speed.y);

		rb.velocity = new Vector3(0, 1, 0) * randomNumb;

	}


}

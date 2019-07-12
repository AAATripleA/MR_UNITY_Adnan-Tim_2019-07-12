using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideWithDestroy : MonoBehaviour
{

	//Objekt zerstören, die mit der Wand hinter dem Spieler collidieren (OBJECT POOLING wäre performanter)

	public string theTag = "destroy";

	//HIT CRYSTAL OR ASTEROID
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag(theTag))
		{
			Destroy(this.gameObject);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalCollideWithPlayer : MonoBehaviour
{

	//Kristal trifft Spieler
	//Spieler kriegt Punkte
	//Sound und Effekt werde versetzt X mal abgespielt, je nach aus wie vielen einzlenen Kristallen der aufgesammelte Kristal bestand.

	public string playerTag = "Player";

	public int XCrystals = 1;

	public int EachXGivesYPoints = 5;

	public GameObject soundVFX;
	public float soundOffSetInSec = 0.4f;

	//HIT CRYSTAL OR ASTEROID
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag(playerTag))
		{
			StartCoroutine(SoundOffset());
		}
	}

	IEnumerator SoundOffset()
	{

		for (int i = 0; i < XCrystals; i++) { 

			UIScore_SINGELTON.instance.addScore(EachXGivesYPoints);

			Instantiate(soundVFX, this.transform.position, this.transform.rotation);

			yield return new WaitForSeconds(soundOffSetInSec);
		}

		Destroy(this.gameObject);
	}


}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollideWithPlayer : MonoBehaviour
{
	//Asteroid trifft auf spieler. Spieler erleidet schaden. SoundEffectObjekt wird an trefferstelle gespawned. THIS(Dieser Asteroid wird zerstört)


	public string playerTag = "Player";
	public int damageAmount = 1;

	public GameObject soundVFX;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			playerSINGELTON.instance.TakeDamage(damageAmount);


			Instantiate(soundVFX, this.transform.position, this.transform.rotation);

			Destroy(this.gameObject);
		}
	}
}

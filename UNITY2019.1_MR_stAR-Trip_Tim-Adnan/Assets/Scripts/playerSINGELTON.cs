using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSINGELTON : MonoBehaviour
{
	//Spieler Character -> Health, DamageAnimation and Collider turned off when game is over

	//Make this script a Singelton and therefore everywhere easy accesable  
	[HideInInspector]
	public static playerSINGELTON instance;

	public Animator animatorDamgeImage;		//get my Animator


	public int health = 5;					//Setup player health in inspector

	private Collider myCollider;			//Get my collider
	//private Rigidbody myRb;

	private void Awake()					//Setup this singelton instance and prevent 2 of the same kind
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Debug.LogWarning("There are multiple player-objects, ma negga!");
			return;
		}
	}

	private void Start()						//Get my collider
	{
		myCollider = this.GetComponent<Collider>();
	}

	public void TakeDamage (int damage)			//substract from health, play animation
	{
		health -= damage;

		if (animatorDamgeImage != null)
			animatorDamgeImage.Play("UI_HealthDamage", -1, 0f);
		else
			Debug.LogWarning("No Damage indicator set?!");

	}


	public void colliderTurnOff()				//Turn of Collider, when called
	{
		myCollider.enabled = false;
	}

}

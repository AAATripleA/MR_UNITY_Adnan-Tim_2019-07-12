using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
	//Manage Health UI according to current player health (Heart Container)

	public Image[] hearts;					//give me the position of each heart
	public Sprite fullHeart;				//How does a full heart look like?
	public Sprite emptyHeart;				//and when it is empthy

	[SerializeField]						
	private int numOfHearts;				
	private int health;				

	private void Start()
	{
		numOfHearts = playerSINGELTON.instance.health;
		health = numOfHearts;
	}


	private void Update()
	{
		//Allways get the health

		health = playerSINGELTON.instance.health;

		//if changed, change visual heart representation
		if (health > numOfHearts)
			health = numOfHearts;

		//Display hearts
		for (int i = 0; i < hearts.Length; i++)
		{
			//below health = heart available else empthy
			if (i < health)
				hearts[i].sprite = fullHeart;
			else
				hearts[i].sprite = emptyHeart;

			//If basic heart amounts changes
			if (i < numOfHearts)
				hearts[i].enabled = true;
			else
				hearts[i].enabled = false;

		}
	}
}

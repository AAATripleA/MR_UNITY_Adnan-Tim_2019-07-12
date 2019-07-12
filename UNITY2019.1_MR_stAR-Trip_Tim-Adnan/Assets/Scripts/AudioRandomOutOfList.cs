using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandomOutOfList : MonoBehaviour
{
	//Zufälligen AudioClip auswählen und abspielen
	//When dieser zuende ist, THIS (GameObject) zerstören.


	private AudioSource myAS;
	public AudioClip[] clips;

	private void Start()
	{
		myAS = this.GetComponent<AudioSource>();

		int randomNumb = Random.Range(0, clips.Length);

		AudioClip randomClip = clips[randomNumb];

		myAS.clip = randomClip;
		myAS.Play();
	}

	private void Update()
	{
		if (!myAS.isPlaying)
			Destroy(this.gameObject);
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIScore_SINGELTON : MonoBehaviour
{
	[HideInInspector]
	public static UIScore_SINGELTON instance;					//Singelton = Überall verfügbar								//ZUKUNFT: SINGELTON nicht verwenden --> Zu starke Abhängigkeiten (Quick & Dirty), stattdessen ScriptableObjects

	[Header("ScoreUI")]											//ScoreText -> Zahl ändern
	public TextMeshProUGUI scoreText;


	private Animator myAnimator;								//Mit Animator popUp effekt abspielen beim einsammeln
	[SerializeField] //Im Inspector sicher
	private int scorePoints = 0;								

	
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Debug.LogWarning("There are multiple UIScore-objects, ma negga!");
			return;
		}
	}


	private void Start()										//ScorePoint aufsetzen, Animator schnappen
	{
		scoreText.text = scorePoints.ToString();
		myAnimator = this.GetComponent<Animator>();
	}

	public void addScore(int scorePoint)						//ScorePoint addieren, darstellen, Animation abspielen
	{
		scorePoints += scorePoint;
		scoreText.text = scorePoints.ToString();
		myAnimator.Play("UI_ScorePointAdded", -1, 0f);

	}


}

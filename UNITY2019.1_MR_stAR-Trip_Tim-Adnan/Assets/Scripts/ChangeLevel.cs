using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
	//Laden einer spezifischen szene beim "Enablen" des Obejekts
	public string szenenName = "";

	void OnEnable()
	{
		SceneManager.LoadScene(szenenName);
	}
    
}






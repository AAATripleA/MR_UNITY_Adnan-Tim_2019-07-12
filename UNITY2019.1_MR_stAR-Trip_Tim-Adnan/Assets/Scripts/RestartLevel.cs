using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
	//One enable restart THIS scene
	void OnEnable()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}

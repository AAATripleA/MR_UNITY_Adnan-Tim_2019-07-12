using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{
	//Cause a oneTime random rotation

    [SerializeField]
    private Vector2 randomTumble;

	//Give a random value between a and b and apply as rotation.
	void Start()
    {
		float random = Random.Range(randomTumble.x, randomTumble.y);

        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * random;
    }
}
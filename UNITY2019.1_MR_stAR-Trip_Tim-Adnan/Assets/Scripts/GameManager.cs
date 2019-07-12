using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	//ZUSAMMENFASSUNG:
	/*	Start Spiel, steuert Spawnverhalten und erklärt das Spiel für beendet.
	 */

	[Header("SPAWN Times")]					//For Coroutine which starts the game-loop
	public float startWait;					//Wait till first objectSpawn
	public float spawnWait;                 //Wait after each spawn   <-- Will be manipulated troughout the game --> According to the Music!

	[Header("SPAWN To MUSIC")]				//Parameters to allow spawnSpeed according to music 
	public MusicChangeParameter[] musicChangeParameters;	//therefore parameters to know when to change: spawnWait, spawnChanceOfSpecificObjects (Asteroids, Crystals), duration/ trackMoment) 
	public bool dynamicSpawning = false;					//for debugging = deactivate it to test values without waiting till trackposition is reached

	[Header("SPAWN Config")]				//Collect Objects that will be spawned
	public float MAXcrystalChanceToSpawn;	//Astroid spawnchance will be automatically calculated when crystal spawnChance is known
	public ObjectsToSpawn[] crystals;		//give me your crystal Objects and there individual chance to spawn
	public ObjectsToSpawn[] asteroids;      //give me your asteroids Objects and there individual chance to spawn
	public SpawnPosition crystalSpawnPos;   //give me there specific spawnRange (X, Y, Z)
	public SpawnPosition asteroidsSpawnPos;

	[Header("WIN || LOSE")]					//Give me the Win and Lose UI, which i will display, when the condition is met
	public GameObject winUI;
	public GameObject loseUI;



	private float randomNumber0To100 = 0;		//calculate random number (1-100%) to decide later to spawn a specific object from a specific group
	private GameObject gameObjectToSpawn;       //hold the randomly selected Object to spawn

	private AudioSource myAS;                   //Get the AudioSource to change game According to the music and to stop it when losing


	//START GAME
	private void Start()
	{
		StartCoroutine(StartGame());			//Start the game-loop

		myAS = this.GetComponent<AudioSource>(); //get the audio source of my gameObject
	}


	private void Update()
	{
		//Spawnen dynamisch zur Musik --> Parameter (Wann/ Dauer, Was, Häufigkeit) werden im Inspector zugewiesen 
		dynamicSpawningAccordingToTrack();
	}


	//IMPORTANT: Method will be called only ONCE! and will be keeped in a loop till Win/ Lose
	IEnumerator StartGame()
	{
		//Start first Spawn after X seconds
		yield return new WaitForSeconds(startWait);

		//TRAPED in a Game-Loop till Win or Lose happens. :D  --> Win = Music reaches end ; Lose = Health reaches 0
		while (true)
		{
			//Spawn a random object according to spawnchance at random spawnposition <-- Setup in Inspector
			SpawnObject();

			//Check if condition is met to Win or Lose and END the loop an therefore the Game.
			if (EndGame())
				break;



			yield return new WaitForSeconds(spawnWait);	//After EACH SPAWN wait x seconds bevor while starts again

		}
	}

	//Spawn "randomly" picked Objects from a list at a random position  <-- Setup in the Inspector
	private void SpawnObject()
	{
		//1. We have 2 Lists (Asteroid, Crystals) --> Pick one randomly according to there "pickChance" 
		//2. Picked one List, NOW pick from that list a "random" Object (Each one has its own spawnChance) and keep it in a variable and get over to spawning
		//3. Spawn that Object in the given Range(X, Y, Z) you setup in the Inspector for that specific object-group (Asteroids/ Crystalls)
		
		//1.
		randomNumber0To100 = Random.Range(1, 101);  //1-100%

		ObjectsToSpawn[] selectedSpawnList = randomNumber0To100 <= MAXcrystalChanceToSpawn ? crystals : asteroids;              //Select which group to spawn from according to the spawnChance!

		//2.
		randomNumber0To100 = Random.Range(1, 101);  //1-100%

		foreach (ObjectsToSpawn spawnObject in selectedSpawnList)
		{

			if (spawnObject.objectToSpawns != null)
			{
				if (randomNumber0To100 >= spawnObject.spawnchanceA && randomNumber0To100 <= spawnObject.spawnchanceB)
				{
					//Pick randomly one variation of that object. (e.g. one randomly out of small Asteroids)
					gameObjectToSpawn = spawnObject.objectToSpawns[Random.Range(0, spawnObject.objectToSpawns.Length)];
					break;
				}
			}
			else
				Debug.LogWarning("One SpawnEntry is Damaged!");
		}

		//3.
		//SELECT Spawn Position according to selected object to spawn
		//If Object is a crystal, spawn at own position
		//If Object is a Asteroid, spawn at own Position
		if (gameObjectToSpawn == null)
			Debug.LogWarning("Spawnobject konnte nicht ausgewählt werden. Siehe angegebne Wahrscheinlichkeit, ma nigger!");
		else
		{
			SpawnPosition spawnPosRange = gameObjectToSpawn.tag == "crystal" ? crystalSpawnPos : asteroidsSpawnPos;
			Vector3 spawnPosition = new Vector3(
				Random.Range(spawnPosRange.X_Range.x, spawnPosRange.X_Range.y),
				Random.Range(spawnPosRange.Y_Range.x, spawnPosRange.Y_Range.y),
				Random.Range(spawnPosRange.Z_Range.x, spawnPosRange.Z_Range.y)
				);

			//Instantiate finally
			GameObject instantObstacle = Instantiate(gameObjectToSpawn, spawnPosition, gameObjectToSpawn.transform.rotation);
			//instantObstacle.transform.parent = imageTarget.transform;															//become Child of image target !!!! TURNED OFF --> Can cause problems!
		}
	}

	//Change SpawnAMOUNT, its duration and whatToSpawn according to the Music!
	private void dynamicSpawningAccordingToTrack()
	{
		if (dynamicSpawning)					//Do i even want it? --> Useful, when debugging
		{
			foreach (MusicChangeParameter thisParameter in musicChangeParameters)				//Get Each given struct for a Track position, for as long as you find the one which suits the trackposition
			{																					//and then change waitAfterEachSpawnTime and the chanceToSpawnCrystal and therefor not asteroids
				if (myAS.time < thisParameter.trackPosition)
				{
					spawnWait = thisParameter.newSpawnWaitTime;
					MAXcrystalChanceToSpawn = thisParameter.crystalSpawnChance;

					break;																		//I found the right object for this trackposition and change everything according to it.
				}
			}
		}
	}

	////EndGame = RETURN true or false --> GameOver or Win == Break;
	private bool EndGame()
	{
		//YOU WIN_If you survived til the end of the music-track
		if (!myAS.isPlaying)
		{
			winUI.SetActive(true);						//Turn on Win-UI

			playerSINGELTON.instance.colliderTurnOff(); //Player should not collide with anything anymore

			return true;								//return to Coroutine which end now.
		}

		//GAME OVER
		else if (playerSINGELTON.instance.health <= 0)
		{
			myAS.Stop();
			loseUI.SetActive(true);                     //Turn on Lose-UI

			playerSINGELTON.instance.colliderTurnOff(); //Player should not collide with anything anymore

			return true;								//return to Coroutine which end now.
		}


		return false;									//Nope, we are currently playing

	}



	//STRUCT = komfortabler Weg um multiple Attribute/Variablen, die eng im Zusammenhang stehen zu organisieren. Besonders wenn sie öfter benutzt werden (Listen). Sieht Übersichtlich im Inspector aus.
	[System.Serializable] //Allowes to view in inspector
	public struct ObjectsToSpawn			//Basic information for an Object to spawn (ObjektDerSelbenSorte und ihre Wahrscheinlichkeit (20% = 0 - 20)
	{
		public GameObject [] objectToSpawns;
		public float spawnchanceA;
		public float spawnchanceB;
	}

	[System.Serializable]
	public struct SpawnPosition            //Basic information for SpawnArea (X, Y and Z) for e.g. a specific object group (Crystal, Asteroid)
	{
		public Vector2 X_Range;
		public Vector2 Y_Range;
		public Vector2 Z_Range;
	}


	[System.Serializable]
	public struct MusicChangeParameter          //Basic information for dynmaicSpawningAccordingToMusic
	{
		public float trackPosition;				
		public float newSpawnWaitTime;
		public int crystalSpawnChance;
	}
}

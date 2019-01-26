using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour {

	//*** Reference to class
	public static GameplayManager instance;

	public  enum GameState{
		Tutorial,
		Activegame
	}

	public GameState currentGameState = GameState.Tutorial;

	//*** Lis of different types of weapons
	public List<GunData> WeaponTypes = new List<GunData>() ;

	public EnemyWavegenerator enemyWaves;

	public Transform overheadCamera;
	public Transform driverCamera;

	[HideInInspector]
	public Transform playerTankTransform;


	public enum CameraOptions{
		TopDown,
		DriveView
	}

	private CameraOptions cameraView;

	public bool waveIsActive;


	public int waveIndex = 0;

	public string waveNickname;
	public float waveSpawnRate;

	public SpawnPoint[] spawnPoints = new SpawnPoint[0];

	public HealthSpawner healthSpawns;

	// Use this for initialization
	void Awake () {

		if (instance == null) {

			instance = this;
		}
		
	}

	void Start(){
		

	}


	public void BeginGame(){

		//*** Get current spawn points
		GetSpawnPoints ();

		//*** Begin enemy wave timer
		BeginWaveTimer();

		//*** Close tutorial Panel
		UIController.instance.CloseTutorialPanel();

		//*** shaw Wave Info
		UIController.instance.RenderWaveUI ();

		//*** Enable Game State
		currentGameState = GameState.Activegame;
	}


	public  GunData GetWeapon(GunData.gunType weaponType){

		int selectedWeaponIndex = 0;

		for (int i = 0; i < WeaponTypes.Count; i++) {

			//*** If we find our weapon in our weapon list, update active gun settings
			if (weaponType == WeaponTypes[i].WeaponType) {

				selectedWeaponIndex = i;
				break;

			}
		}


		return WeaponTypes [selectedWeaponIndex];
	}
	
	public void SetPlayerTransform(Transform pPlayerTransform){

		playerTankTransform = pPlayerTransform;
	}

	public void GenerateEnemyWaves(){

		//*** Generate Next Wave of enemies
		enemyWaves.GenerateNextWave (waveIndex);

	}

	public void BeginWaveTimer(){

		//*** Set wave nickname
		waveNickname = enemyWaves.enemyRounds [waveIndex].waveNickName;

		//*** Set wave spawn rate
		waveSpawnRate = enemyWaves.enemyRounds [waveIndex].spawnrate;

		//**** Begin UI Wave timer and Render new Wave info
		UIController.instance.RenderWaveInfo();

		//*** Generate next wave of enemies
		GenerateEnemyWaves();

		//*** spawn health pickups
		healthSpawns.spawnHealthItems ();

	}

	public void BeginRound(){
		
		waveIsActive = true;

		//*** Activate spawns
		Activate_DeactivateSpawnPoints (true);
	}


	public void EndRound(){

		waveIsActive = false;

		//*** Deactivate spawns
		Activate_DeactivateSpawnPoints (false);


		waveIndex++;

		if (waveIndex > enemyWaves.enemyRounds.Count) {

			//*** If players have completed all waves show Completion UI

		} else {

			//*** else ready up next round
			UIController.instance.ShowWaveCompletionText();

		}
	}


	public void GetSpawnPoints(){

		//Get all available spawn points
		spawnPoints = GameObject.FindObjectsOfType<SpawnPoint> ();
	}


	public void Activate_DeactivateSpawnPoints( bool pSetSpawnsActive){

		//*** Iterate through spawns
		for (int i = 0; i < spawnPoints.Length; i++) {

			//*** Set spawn active or inactive
			spawnPoints [i].spawnAvailable = pSetSpawnsActive;

		}
	}

	public void CheckForWavecompletion(){

		if (enemyWaves.currentdefeatedEnemyWaveCount >= enemyWaves.totalEnemiesInWave) {

			//*** Player completed Wave
			EndRound ();
		}


	}

	public void EnemyDestroyed(){

		//*** Increment enemy destroyed
		enemyWaves.currentdefeatedEnemyWaveCount++;

		//*** Wave Completion
		CheckForWavecompletion ();

	}
}

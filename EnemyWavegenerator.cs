using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class EnemyWavegenerator  {

	public float delayBetweenRounds;

	[HideInInspector]
	public int spawnedEnemies;
	public int currentdefeatedEnemyWaveCount;
	public int totalEnemiesInWave;

	public float currentSpawnRate;


	[Header("Enemy Waves")]
	public List<EnemyWaveRound> enemyRounds;

	[HideInInspector]
	public List<EnemySpawnData> basicEnemySpawnList;

	[HideInInspector]
	public List<EnemySpawnData> shuffledEnemySpawnList;



	public void GenerateNextWave(int pWaveIndex){

		//*** Clear enemy list
		ClearEnemyLists ();

		//*** create enemy list
		CreateEnemyList (pWaveIndex);

		//*** shuffle enemy list to spawn enemies at different intervals
		ShuffleEnemyList();

	}


	public void CreateEnemyList( int pWaveIndex){

		//Will hold number of Enemys present in wave
		int enemyCount = 0 ;

		//*** Cached reference to current enemy gameobject
		GameObject currentEnemyGameObject = null;

			//*** Iterate through different enemy spawn types
			for(int x = 0 ; x < enemyRounds[pWaveIndex].EnemyTypesInWave.Count; x++){
				
				//*** Get number of enemies  spawning in the wave
				enemyCount = enemyRounds [pWaveIndex].EnemyTypesInWave [x].enemiesInWave;

				//*** Create Spawn data
				for (int z = 0; z < enemyCount; z++) {

					//*** Get Current enemy GameObject
					currentEnemyGameObject = enemyRounds [pWaveIndex].EnemyTypesInWave [x].enemyType;

					//*** Create enemy spawn data
					EnemySpawnData enemySpawn = new EnemySpawnData();

					//*** Assign reference to enemy 
					enemySpawn.enemyGameObject = currentEnemyGameObject;  
						 
					//*** Get random enemy position seed for sorting
					enemySpawn.spawnSeed = Random.Range(0,500);

					//*** Add enemy Data to the list
					basicEnemySpawnList.Add(enemySpawn);

				}

			}

	}


	public void ShuffleEnemyList(){

		//*** Create shuffled Enemylist
		shuffledEnemySpawnList = basicEnemySpawnList.OrderBy (p => p.spawnSeed).ToList ();

		//*** Get number of enemies available in the wave
		totalEnemiesInWave = shuffledEnemySpawnList.Count;

	}


	public void ClearEnemyLists(){

		//*** Clear enmies
		basicEnemySpawnList.Clear ();
		shuffledEnemySpawnList.Clear ();
	}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyWaveRound {
	public string waveNickName;
	public float spawnrate;
	public List<EnemyWaveData> EnemyTypesInWave;
}

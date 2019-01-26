using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour {

	public Transform target;
	public NavMeshAgent navmeshAgent;


	// Use this for initialization
	void Start () {

		//*** Set Player As target


	}
	
	// Update is called once per frame
	void Update () {
		if (GameplayManager.instance.playerTankTransform != null) {

			navmeshAgent.SetDestination (GameplayManager.instance.playerTankTransform.position);


		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBase : MonoBehaviour {

	public enum fighterState:int{
		Intro = 0,
		Fighting = 1,
		RestingInCorner = 2

	}

	public fighterState currentState;
	public GameObject Opponent;

	[Header("Stats")]
	public TankStats Attributes;

	public Transform selfTransform;
	public Rigidbody rigidBody;


	public bool lockY;

	// Use this for initialization
	public void Start () {
		selfTransform = this.gameObject.transform;
	}
	
	// Update is called once per frame
	public void Update () {




		//*** Look at Opponent
		if (currentState == fighterState.Fighting) {

			//this.gameObject.transform.LookAt (opponentTransform);


		}

		if (lockY)
			this.gameObject.transform.transform.position = new Vector3(selfTransform.position.x, 1f, selfTransform.position.z);

		rigidBody.ResetCenterOfMass ();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldTank : MonoBehaviour {

	[Header("PlayerControls")]
	public KeyCode left;
	public KeyCode right;
	public KeyCode forward;
	public KeyCode backward;
	public KeyCode fireWeapon;

	private float XMovement;
	private float ZMovement;
	private bool moved;


	public float movementSpeed = 5f;


	public GameObject tankParent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//*** Reset Movement
		XMovement = 0f;
		ZMovement = 0f;
		moved = false;


		//*** Move from left to right 
		if(Input.GetKey(left)){
			moved = true;
			transform.Rotate((Vector3.up * Time.deltaTime * movementSpeed) * -2f);
		}else if(Input.GetKey(right)){
			moved = true;
			transform.Rotate((Vector3.up * Time.deltaTime *movementSpeed) * 2f);
		}



		//*** Move forward and Back
		if(Input.GetKey(forward)){
			moved = true;
			ZMovement = movementSpeed;
		}else if(Input.GetKey(backward)){
			moved = true;
			ZMovement = movementSpeed * -1f;
		}


		//*** Move Tank
		if(moved)
			transform.Translate(new Vector3(XMovement,0f,ZMovement));

		//*** Set Tank parent to Match our players Position
		tankParent.transform.position = this.gameObject.transform.position;
	}
}

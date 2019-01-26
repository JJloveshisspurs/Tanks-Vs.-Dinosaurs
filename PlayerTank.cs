using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTank : TankBase {

	[Header("PlayerControls")]
	public KeyCode left;
	public KeyCode right;
	public KeyCode forward;
	public KeyCode backward;
	public KeyCode fireWeapon;

	public KeyCode weaponSelect1;
	public KeyCode weaponSelect2;
	public KeyCode weaponSelect3;

	public KeyCode OverHeadCamera;
	public KeyCode DriveCamera;

	public GunData activegun;

	private float XMovement;
	private float ZMovement;
	private bool moved;

	//*** Index of last fired shell
	public int shellIndex;

	private Projectile lastshell;

	private float firingRateTimer;
	private float firingRate;
	private Vector3 shootingOffset = new  Vector3(0f,0f,0f);

	// Use this for initialization
	void Start () {
		base.Start ();

		//*** Set pistol as default weapon
		SetActiveweapon (0);

		CameraController.instance.SetCameraTarget (this.gameObject,
			CameraController.CameraOptions.DriveView);

		GameplayManager.instance.SetPlayerTransform (this.gameObject.transform);
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();

		//*** Reset Movement
		XMovement = 0f;
		ZMovement = 0f;
		moved = false;


		//*** Move from left to right 
		if(Input.GetKey(left)){
			moved = true;
			transform.Rotate((Vector3.up * Time.deltaTime * Attributes.rotationSpeed) * -1f);
		}else if(Input.GetKey(right)){
			moved = true;
			transform.Rotate(Vector3.up * Time.deltaTime *Attributes.rotationSpeed);
		}



		//*** Move forward and Back
		if(Input.GetKey(forward)){
			moved = true;
			ZMovement = Attributes.speed;
		}else if(Input.GetKey(backward)){
			moved = true;
			ZMovement = Attributes.speed * -1f;
		}
			
		//*** Increment timer
		firingRateTimer += Time.deltaTime;

		//*** Check against firing rate
		if (Input.GetKey(fireWeapon)  && firingRateTimer >= firingRate) {

			//** Reset Firing Rate timer
			firingRateTimer = 0f;

			//*** Reset shell Index
			shellIndex = 0;

			//*** Begin Firing Weapons
			FireWeapon ();
		}


		//*** Weapon Switch Checks
		if (Input.GetKeyDown (weaponSelect1)) {
			SetActiveweapon (0);
		}

		if (Input.GetKeyDown (weaponSelect2)) {
			SetActiveweapon (1);
		}

		if (Input.GetKeyDown (weaponSelect3)) {
			SetActiveweapon (2);
		}

		if (Input.GetKeyDown (OverHeadCamera)) {

			CameraController.instance.SetCamera (CameraController.CameraOptions.TopDown);
		}


		if (Input.GetKeyDown (DriveCamera)) {

			CameraController.instance.SetCamera (CameraController.CameraOptions.DriveView);
		}



		//*** Move Tank
		if(moved)
			transform.Translate(new Vector3(XMovement,0f,ZMovement));

	}

	public void FireWeapon(){


		StartCoroutine (FireNextShell (activegun.projectileInfo[shellIndex].firingDelay));

	}


	IEnumerator FireNextShell( float fireDelay){



		//*** Get Players Current rotation
		Vector3 rotation = transform.rotation.eulerAngles;

		//*** Add Rotation angle of the Shell to players Rotation Angle
		rotation = rotation + activegun.projectileInfo[shellIndex].firingAngle;


		//*** Get firing position and offset
		Vector3 firingPosition = transform.position + shootingOffset;



		yield return new  WaitForSeconds(fireDelay);


		//*** Instantiate shell withcalculated firing angle ( players angle + firing angle of projectile itself)
		lastshell =  Instantiate (activegun.projectileInfo[shellIndex].projectilePrefab, 
			firingPosition ,  Quaternion.Euler(rotation)).GetComponent<Projectile>();

		//*** Set Projectile info
		lastshell.SetProjectile(activegun.ProjectileFlightSpeed,
			activegun.ammoLifeSpan,
			activegun.damageOnHit);

		//*** Increment index
		shellIndex++;


		if(shellIndex < activegun.projectileInfo.Count )
			FireWeapon();
	}


	public void SetActiveweapon(int pWeaponNum){

		Debug.Log ("Setting Weapon");


		switch (pWeaponNum) {

		case 0:  


			activegun =	GameplayManager.instance.GetWeapon (GunData.gunType.pistol);
			firingRate = activegun.firingRate;

			UIController.instance.RenderWeaponSelection (activegun.nickName);
			break;


		case 1:  

			activegun =	GameplayManager.instance.GetWeapon (GunData.gunType.machineGun);
			firingRate = activegun.firingRate;
			UIController.instance.RenderWeaponSelection (activegun.nickName);
			break;


		case 2:  

			activegun =	GameplayManager.instance.GetWeapon (GunData.gunType.ShotGun);
			firingRate = activegun.firingRate;
			UIController.instance.RenderWeaponSelection (activegun.nickName);
			break;

		default:  


			break;



		}
	}
}

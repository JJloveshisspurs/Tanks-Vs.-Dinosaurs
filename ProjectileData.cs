using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProjectileData  {

	private int projectileDamage;

	//*** Angle to fire at relative to Player Tank
	public Vector3 firingAngle;

	public float firingDelay;

	//*** Associated sound of Projectile firing
	public AudioClip firingSound;

	public GameObject projectilePrefab;


}

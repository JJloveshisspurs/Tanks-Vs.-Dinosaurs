using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GunData  {

	public enum gunType{
		pistol,
		machineGun,
		ShotGun
	}

	public gunType WeaponType;
	public string nickName;

	public List<ProjectileData> projectileInfo = new List<ProjectileData> ();
	public float firingRate;
	public float ammoLifeSpan;
	public int damageOnHit;

	//*** Speed at which Projectile Moves
	public float ProjectileFlightSpeed;

}

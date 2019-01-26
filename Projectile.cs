using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float projectileFlightSpeed;

	public float projectileDeathDelay;

	public int ProjectileDamage;

	// Use this for initialization
	void Start () {
		
	}

	void Onenable(){



	}


	
	// Update is called once per frame
	void Update () {

		transform.Translate( (Vector3.forward * Time.deltaTime) * projectileFlightSpeed);
	}


	public void SetProjectile(float flightSpeed, float projectileLifetime, int damageValue){

		//*** Set Lifespan for Projectile if it doesn't collide with something
		Destroy (this.gameObject, projectileLifetime);

		//*** Set projectile damage
		ProjectileDamage = damageValue;

		//*** Set Projectile fligtSpeed
		projectileFlightSpeed = flightSpeed;


	}

	void OnCollisionEnter(Collision collision)
	{
		//*** Destroy this gameobject
		Destroy (this.gameObject);


	}
}

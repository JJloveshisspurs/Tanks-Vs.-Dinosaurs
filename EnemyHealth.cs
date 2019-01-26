using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthSystem {

	private Projectile projectileCollision;

	// Use this for initialization
	void Start () {
		//*** call initial start
		base.Start ();
	}
	
	void OnCollisionEnter(Collision collision)
	{
		foreach (ContactPoint contact in collision.contacts)
		{
			Debug.DrawRay(contact.point, contact.normal, Color.white);
		}

		//*** 10 corresponds to the bullet Layer , may need to set a global property
		if (collision.gameObject.layer == 10) {

			Debug.Log ("Dinosaur was hit !!!!!!");

			projectileCollision = collision.gameObject.GetComponent ("Projectile") as Projectile;

			//*** Retriece and store damage number
			int damage = projectileCollision.ProjectileDamage;

			Debug.Log ("Projectile Hit enemy for " + damage + " damage!");
				currentHealth = currentHealth - damage;

			//*** Check if dead and handle it
			if (currentHealth == 0) {
				Death ();
			} else {



			}
		}
	}



	public void Death(){

		//*** Mark enemy dead
		GameplayManager.instance.EnemyDestroyed();

		//*** Kill dionasur character
		Destroy (this.gameObject);

	}
}

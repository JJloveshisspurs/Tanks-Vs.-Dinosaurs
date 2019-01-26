using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthSystem {

	// Use this for initialization
	void Start () {
		//*** call initial start
		base.Start ();

		UIController.instance.RenderHealth (base.currentHealth);
	}
	
	void OnCollisionEnter(Collision collision)
	{
		foreach (ContactPoint contact in collision.contacts)
		{
			Debug.DrawRay(contact.point, contact.normal, Color.white);
		}

		//*** 9 corresponds to the Dinasaur Layer , may need to set a global property
		if (collision.gameObject.layer == 9) {

			Debug.Log ("Tank was hit !!!!!!");

			EnemyDamage enemyAttack = collision.gameObject.GetComponent ("EnemyDamage") as EnemyDamage;

			int damage = enemyAttack.enemyDamage;

			Debug.Log (" Player Tahnk has been attacked for " + damage + " !!!!!");

			if(currentHealth >0)
			currentHealth = currentHealth - damage;

		}


		//*** 9 corresponds to the Tank Layer , may need to set a global property
		if (collision.gameObject.layer == 11) {

			Debug.Log ("Tank Touched item !!!");

			Item itemPickedUp = collision.gameObject.GetComponent ("Item") as Item;

			ApplyItem (itemPickedUp);

		}

		UIController.instance.RenderHealth (base.currentHealth);
	}

	public void ApplyItem(Item pItem){

		switch (pItem.currentItem.currentType) {

		case ItemData.itemTypes.Health:
			
			if(currentHealth < startingHealth){
			currentHealth = currentHealth + pItem.currentItem.amount;
				Debug.Log(" Picked up health, adding health !");

					}
			break;

		case ItemData.itemTypes.Currency:


			break;

		default:


			break;



		}
		//*** Destroy touched item 
		Destroy (pItem.gameObject);

	}



}

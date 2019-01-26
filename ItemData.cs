using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData  {

	public enum itemTypes{
		Health,
		Currency


	}

	public itemTypes currentType;

	public int amount;


}

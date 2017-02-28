using UnityEngine;
using System.Collections;

public class MurderWeapon : Item {
	//Subclass of item depicting how the item was used to kill the victim (steve)

	//__Variables__
	private string steve_description;

	//__Constructor__
	//Get the variables: prefab, clueID, description, sprite from the item class
	public MurderWeapon(GameObject prefab, string clueID, string description, Sprite sprite, string steve_description) : base(prefab, clueID, description, sprite) {
		this.steve_description = steve_description;
	}

	//Accessors
	public string getSteveDescription() {
		return this.steve_description;
	}
}


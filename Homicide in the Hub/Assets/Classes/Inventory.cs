using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory{
	//Inventory is used to store the items held by the player. Note that no verbal clues are
	//stored here as they are stored in the logbook. 

	//__Variables__
	//Define the inventory as a list items
	private List<Item> inventory = new List<Item> ();

	//__Constructor__
	public Inventory (){
	}


	//__Operations__
	//Adds the argument item to the inventory
	public void AddItemToInventory(Item item){
		inventory.Add (item);
		GameMaster.instance.clueCollected (); 	//Increments clue count for current player ADDITION BY WEDUNNIT
	}


	//Mainly used when starting a new game
	public void Reset(){
		this.inventory.Clear ();
	}

	//Accessors
	public int GetListLength(){
		return this.inventory.Count;
	}

	public List<Item> GetInventory(){
		return this.inventory;
	}

}

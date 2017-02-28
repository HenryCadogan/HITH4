using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Scene{
	//Defines the variables and methods used to define a scene. 
	//Each scene depicts a playable room that represents a room in the RCH 

	//__Variables__
	private string name;
	private List<NonPlayerCharacter> characters = new List<NonPlayerCharacter> (); //List of characters to be placed in the room
	private List<Item> items = new List<Item> ();									//List of items to be placed in the room
    // NEW FOR ASSESSMENT 3 
    private Item key; // the key which will open up the underground lab is set to false to start with. False -> no key, True -> key is in that room.  

	//__Constuctor__
	public Scene (string name) {
		this.name = name;
	}

	//__Methods__
	public void AddNPCToArray(NonPlayerCharacter character){
		//Adds the argument 'character' to the list of characters for this instance of the scene
		this.characters.Add (character);
	}

	public void AddItemToArray(Item item){
		//Adds the argument 'item' to the list of characters for this instance of the scene
		this.items.Add (item);
	}

	public void ResetScene(){
		//Used to reset scenes from a previous playthough
		this.characters.Clear ();
		this.items.Clear ();
	}

    // NEW FOR ASSESSMENT 3 - locked room feature 
    public void setKey(Item inkey)
    {
        key = inkey;
    }

	//Accessors
	public string GetName(){
		return this.name;
	}

	public List<NonPlayerCharacter> GetCharacters(){
		return this.characters;
	}

	public List<Item> GetItems(){
		return this.items;
	}


    // NEW FOR ASSESSMENT 3 - locked room feature 
    public Item getKey()
    {
        return this.key;
    }

    public bool hasKey()
    {
        if (key != null)
        {
            return true;
        } else
        {
            return false;

        }
    }
}

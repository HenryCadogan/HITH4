using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour {
	//Placed on the item prefabs
	//Much like the characterinteraction script:
	//-Tells the prefab which item it is
	//-Adds the item to the inventory when clicked on.

	//__Variables__
	private Item item = null;

	//Called when the item is clicked on 
	void OnMouseDown(){
			//Adds the item to the inventory, updates the notebook and destroys the item gameobject.
			NotebookManager.instance.inventory.AddItemToInventory (item);
		    GameMaster.instance.ClueCollected (); 	                                                //Increments clue count for current player ADDITION BY WEDUNNIT
			NotebookManager.instance.UpdateNotebook ();

			// ADDED FOR ASSESSMENT 3 - Key //
			GameObject.FindWithTag ("local").GetComponent<QuestioningScript> ().UnignoreNPC ();//npc is now unignored
			
			//Plays mysterious sfx by adding audio source to the local scripts game object (an instance is present in every scene), and playing the sound
			GameObject.Find ("Local Scripts").AddComponent<AudioSource> ();							//ADDITION BY WEDUNNIT
			GameObject.Find ("Local Scripts").GetComponent<AudioSource> ().clip = Resources.Load<AudioClip> ("Sounds/mysterious-sfx"); //ADDITION BY WEDUNNIT
			GameObject.Find ("Local Scripts").GetComponent<AudioSource> ().Play ();					//ADDITION BY WEDUNNIT

			if (GameMaster.instance.UseTurn ()) {	//reduces turns & changes scenes if turns expire ADDITION BY WEDUNNIT
				GameMaster.instance.SwitchPlayers();
			};	

			Destroy (gameObject);

       

	}

	//Sets the item for the prefab
	public void SetItem(Item item){
		this.item = item;
	}
}

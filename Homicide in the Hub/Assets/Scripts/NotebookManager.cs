using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NotebookManager : MonoBehaviour {
	//Handles the logging, displaying and updating of the logbook
	//The Toggles, back button and submit button are only visible in the interrogation room
	//The list on the left hand side of the notebook GUI is a list of all the items collected followed by a list of the verbal clues collected.
	//Logbook refers to the list of VerbalClues
	//Inventory refers to the list of Items
	//We have chosen to call the combination of these two things a notebook

	//__Variables__
	public Inventory inventory = new Inventory();	//Holds the Items
	public Logbook logbook = new Logbook();			//Holds the VerbalClues
	public static NotebookManager instance = null;	//Used for Singleton reference

	//Arrays
	//Public to allow for drag and drop in inspector
	public Text[] clueTexts = new Text[20]; 
	public Button[] clueButtons = new Button[20];
	public Toggle[] clueToggles = new Toggle[20];

	//Public to allow for drag and drop in inspector
	public Text clueNameText;
	public Text clueDescriptionText;
	public Image clueImage;
	public int requiredNumberOfClues = 3;
	public Text clueTitle;
	public Sprite questionMark;
	public Button submitButton;
	public Button backButton;

	//lists of item and verbal clues selected by player by toggling toggle buttons
	private List<Item> selectedCluesItem = new List<Item>();
	private List<VerbalClue> selectedCluesVerbal = new List<VerbalClue>();

	void Awake () {  //Makes this a singleton class on awake
		if (instance == null) { //Does an instance already exist?
			instance = this;	//If not set instance to this
		} else if (instance != this) { //If it already exists and is not this then destroy this
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject); //Set this to not be destroyed when reloading scene
	}

	public void UpdateNotebook(){
		//If the Notebook is used in the Interrogation room, show the toggle buttons used
		if (SceneManager.GetActiveScene ().name == "Interrogation Room") {
			ResetSelectedClues ();
			ResetAllToggles ();			//Resets toggles when re-entering interrogation room
			ShowNeededToggles ();
			clueTitle.text = "Select "+requiredNumberOfClues+" Clues (" + (selectedCluesItem.Count + selectedCluesVerbal.Count) + "/" + requiredNumberOfClues + ")";
			submitButton.interactable = false;
		} else {
			HideAllToggles ();
			clueTitle.text = "Clues Obtained (" + (inventory.GetInventory ().Count + logbook.GetLogbook ().Count) + "/12)";
			
		}


		//Update Listing
		int topOfList = 0;

		//Display Items
		for (int i = 0; i < (inventory.GetInventory ().Count); i++) {
			clueTexts [topOfList].text = " - "+inventory.GetInventory () [i].getID ();
			topOfList += 1;
		}

		//Display Verbal Clues
		for (int j = 0; j < (logbook.GetLogbook().Count); j++) {
			clueTexts [topOfList].text = " - "+logbook.GetLogbook () [j].getID ();
			topOfList += 1;
		}

		//Reset not used items from previous playthrough
		for (int z = 0; z < (20-(inventory.GetInventory ().Count + logbook.GetLogbook ().Count)); z++) {
			clueTexts [topOfList].text = "";
			topOfList += 1;
		}

	}

    // NEW FOR ASSESSMENT 3  - scoring///

    public int clue_count()
    {
        return (inventory.GetInventory().Count + logbook.GetLogbook().Count);   // Returns the number of the total clues that have been found by the player by the end of the game.  there are a total of 12 per game
    }
		
	public void ShowClueInfomation(int index){

		//Check if the clue is in the given range. 
		if (index < inventory.GetInventory ().Count+logbook.GetLogbook ().Count){
			//Detect if it is an item
			if (index < inventory.GetInventory ().Count) {
				Item clue = inventory.GetInventory () [index];
				clueNameText.text = clue.getID ();  
				clueDescriptionText.text = clue.getDescription ();
				clueImage.sprite = clue.GetSprite ();
			
				//Otherwise it must be a Verbal Clue
			} else {
				VerbalClue clue = logbook.GetLogbook () [index - inventory.GetInventory ().Count];
				clueNameText.text = clue.getID ();  
				clueDescriptionText.text = clue.getDescription ();
				clueImage.sprite = questionMark;
			}

		}
	}

	public void AddToSelectedClues(int reference){
		//Adds the selected clue passed by reference in the inspector. Called when a toggle button value changes

		//If toggled on
		if (clueToggles [reference].isOn == true) {
			if (reference < inventory.GetInventory ().Count) {		//If clue reference is an item
				Item clue = inventory.GetInventory () [reference];
				selectedCluesItem.Add (clue);						//Add to selected Item clues
			} else {
				VerbalClue clue = logbook.GetLogbook () [reference - inventory.GetInventory ().Count];
				selectedCluesVerbal.Add (clue);						//Otherwise must be a VerbalClue so add to selected VerbalClues
			}
		//If toggled off:
		} else {
			if (reference < inventory.GetInventory ().Count) {		//If clue reference is an item
				Item clue = inventory.GetInventory () [reference];
				selectedCluesItem.Remove (clue);					//Remove clue from selected item clues
			} else {
				VerbalClue clue = logbook.GetLogbook () [reference - inventory.GetInventory ().Count];
				selectedCluesVerbal.Remove (clue);					//Otherwise must be a VerbalClue so remove from selected VerbalClues
			}
		}

		//
		if ((selectedCluesItem.Count + selectedCluesVerbal.Count) == requiredNumberOfClues) { //If a total of (3) clues are selected make the submit button interactable
			submitButton.interactable = true;
		} else {
			submitButton.interactable = false;												//Otherwise make uninteratable
		}
		clueTitle.text = "Select "+requiredNumberOfClues+" Clues (" + (selectedCluesItem.Count + selectedCluesVerbal.Count) + "/" + requiredNumberOfClues + ")";	//Update title text
	}


	//Toggles
	private void ShowNeededToggles(){
		for (int i = 0; i < (inventory.GetInventory().Count + logbook.GetLogbook().Count); i++) {	//Show all toggles
			clueToggles [i].gameObject.SetActive (true);
		}
		backButton.gameObject.SetActive (true);			//Show other buttons too
		submitButton.gameObject.SetActive (true);
	}

	//Hides all toggles
	private void HideAllToggles(){							
		for (int i = 0; i < 20; i++) {
			clueToggles [i].gameObject.SetActive (false);
		}
		backButton.gameObject.SetActive (false);
		submitButton.gameObject.SetActive (false);
	}

	public List<Item> GetSelectedItemClues(){
		return this.selectedCluesItem;
	}
	public List<VerbalClue> GetSelectedVerbalClues(){
		return this.selectedCluesVerbal;
	}

	//Sets all toggles to off
	private void ResetAllToggles(){				
		for (int i = 0; i < 20; i++) {
			clueToggles [i].isOn = false;;
		}
	}

	//Resets selected clues for a new playthrough
	public void ResetSelectedClues(){			
		selectedCluesItem.Clear ();
		selectedCluesVerbal.Clear ();
	}
}

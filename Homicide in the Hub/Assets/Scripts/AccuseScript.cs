using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class AccuseScript : MonoBehaviour {
	/*-Script used when the player chooses the accuse option in the Interrogation room
	 *-Handles the UI of this area and the comparison of the selected clues with the relevant clues 
	 *whereby relevant clues are clues that are required to win the game
	 *-In order to win the selected clues must be a subset of the relevant clues
	 */

	//__Variables__
	//public to allow for darging
	private GameObject notebookMenu;
	private NonPlayerCharacter character;
	public GameObject optionsMenu;
	public GameObject verbal;
	public Text verbalText;

	public void Start() {
		character = InterrogationScript.instance.GetInterrogationCharacter();
		NotebookManager.instance.UpdateNotebook ();
		notebookMenu = GameObject.Find("Notebook Canvas").transform.GetChild(0).gameObject;
		notebookMenu.SetActive (true);
		Button backButton = GameObject.FindGameObjectWithTag ("Back").GetComponent<Button>();
		Button submitButton = GameObject.FindGameObjectWithTag ("Submit").GetComponent<Button>();
		notebookMenu.SetActive (false);

		//When the button is pressed calls the function after the =>
		backButton.onClick.AddListener (() => BackToMenu ());
		submitButton.onClick.AddListener (() => CompareEvidence ());
	}

	public void OpenNotebook(){
		//Called by Accuse Button being pressed (see inspector)
		NotebookManager.instance.UpdateNotebook ();
		notebookMenu.SetActive (true);
	}
		
	private void BackToMenu(){
		//Used to close the notebook and open the interogation menu
		notebookMenu.SetActive (false);
		optionsMenu.SetActive (true);
	}

	private void CompareEvidence(){
		//Compares the selected clues with required (relevant) clues

		//Accuse accused correctly
		bool accusation = true;

		//Get Selected Clues
		List<Item> selectedItemClues = NotebookManager.instance.GetSelectedItemClues ();
		List<VerbalClue> selectedVerbalClues = NotebookManager.instance.GetSelectedVerbalClues ();

		//Get required clues
		List<Item> relevantItemClues = GameMaster.instance.GetRelevantItems ();
		List<VerbalClue> relevantVerbalClues = GameMaster.instance.GetRelevantVerbalClues ();

		//Check each list of relevant clues contains a selected clue if not set accusation to false
		foreach (Item clue in selectedItemClues) {
			if (!relevantItemClues.Contains (clue)) {
				accusation = false;
			} 
		}
		foreach (VerbalClue clue in selectedVerbalClues) {
			if (!relevantVerbalClues.Contains(clue)) {
				accusation = false;
			}
		}

		AccusationResult (accusation);
	}

	private void AccusationResult(bool accusation){
		//Check if accusation is true and is the acutal murderer
		if ((accusation == true) && (character.IsMurderer ())) {
			//If so go to win screen
			notebookMenu.SetActive (false);
            GameMaster.instance.stop_timer();  // stop the timer when the player wins
			SceneManager.LoadScene ("Win Screen");
		} else {
			//If not go to the lose screen 
			notebookMenu.SetActive (false);
			verbal.SetActive (true);
            GameMaster.instance.stop_timer();
            SceneManager.LoadScene("Lose Screen");

		}
	}

}


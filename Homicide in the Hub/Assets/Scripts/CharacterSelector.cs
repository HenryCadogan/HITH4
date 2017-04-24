using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour {
	/*Handles the character selection screen
	 * -Instantiates and defines the detectives
	 * -Passes the chosen detective to the GameMaster
	 * -Handles GUI of the character selector screen
	 */


	//Detecive variable declaration
	//Public to allow for drag and drop in inspector
	public Sprite chaseHunterSprite;
	public Sprite johnnySlickSprite;
	public Sprite adamFounderSprite;
	private PlayerCharacter chaseHunter;
	private PlayerCharacter johnnySlick;
	private PlayerCharacter adamFounder;

	//Querstioning Styles for detectives
	private string[] chaseHunterQuestioningStyles = new string[3] {"Forceful","Intimidating","Condescending"};
	private string[] johnnySlickQuestioningStyles = new string[3] {"Wisecracking","Rushed","Coaxing"};
	private string[] adamFounderQuestioningStyles = new string[3] {"Inquisitive","Kind","Inspiring"};

	PlayerCharacter[] detectives;

	//GUI References
	//Public to allow for drag and drop in inspector
	public Text GUIName;
	public Image GUIImage;
	public Text GUIQuestioningStyle;
	public Text GUIDescription;
	private int detectiveCounter = 0;

	public bool isMultiGame; //ADDITION BY WEDUNNIT
	private int player1Detective = 	-1; //ADDITION BY WEDUNNIT
	private int currentPlayer = 0;		//ADDITION BY WEDUNNIT


	// Use this for initialization
	void Start () {
		//Initalise detectives
		chaseHunter = new PlayerCharacter ("Chase Hunter", chaseHunterSprite, "The Loose Cannon", "Aggressive", chaseHunterQuestioningStyles, "An ill tempered detective who will do whatever it takes to get to the bottom of a crime." );
		johnnySlick = new PlayerCharacter ("Johnny Slick", johnnySlickSprite, "The Greaseball", "Wisecracking",johnnySlickQuestioningStyles, "A witty detective who finds the comedic value in everything... even death apparently." );
		adamFounder = new PlayerCharacter ("Adam Founder", adamFounderSprite, "Good Cop", "By the Book", adamFounderQuestioningStyles,"A by the book cop who uses proper detective techniques to solve mysteries" );
		detectives =  new PlayerCharacter[3] {chaseHunter, johnnySlick, adamFounder};
		ChangeDetective(); //Sets first detective to be viewed in GUI
	}

	//Called when right button is pressed
	public void CycleUpDetectives(){
		detectiveCounter += 1;
		if (detectiveCounter >= 3) {
			detectiveCounter = 0;
		}

		//If the selected detective has already been chosen, switch to the next.
		if (detectiveCounter == player1Detective) {	//ADDITON BY WEDUNNIT
			detectiveCounter += 1;					//ADDITON BY WEDUNNIT
			if (detectiveCounter >= 3) {			//ADDITON BY WEDUNNIT
				detectiveCounter = 0;				//ADDITON BY WEDUNNIT
			}
		}
		ChangeDetective();
	}

	//Called when left button is pressed
	public void CycleDownDetectives(){
		detectiveCounter -= 1;
		if (detectiveCounter <= -1) {
			detectiveCounter = 2;
		}

		//If the selected detective has already been chosen, switch to the next.
		if (detectiveCounter == player1Detective) {	//ADDITON BY WEDUNNIT
			detectiveCounter -= 1;					//ADDITON BY WEDUNNIT
			if (detectiveCounter <= -1) {			//ADDITON BY WEDUNNIT
				detectiveCounter = 2;				//ADDITON BY WEDUNNIT
			}
		}
		ChangeDetective();
	}

	//Updates the GUI with the selected detective
	private void ChangeDetective(){
		GUIName.text = detectives[detectiveCounter].getCharacterID ();
		GUIImage.sprite =  detectives[detectiveCounter].getSprite ();
		GUIQuestioningStyle.text =  "Questioning Style: "+ detectives[detectiveCounter].GetOverallQuestioningStyle();
		GUIDescription.text = detectives [detectiveCounter].GetDescription ();
	}

	//Called when the play button is pressed
	public void SelectDetective(){
		if (!isMultiGame) {
			GameMaster.instance.CreateNewGame (detectives [detectiveCounter]);	//UPDATED BY WEDUNNIT
			SceneManager.LoadScene ("Atrium");
		}else if (currentPlayer >= 1) {	//If all detectives have chosen //ADDITION BY WEDUNNIT
			GameMaster.instance.CreateNewGame (detectives [player1Detective], detectives [detectiveCounter], true);	//UPDATED BY WEDUNNIT
			SceneManager.LoadScene ("Atrium");
		} else {
			currentPlayer++; 	//ADDITON BY WEDUNNIT
			player1Detective = detectiveCounter;
			CycleDownDetectives ();
			GameObject.Find("Title").GetComponent<Text>().text = "Player "+(currentPlayer+1).ToString()+ ": Select your Detective";
		}
	}

}
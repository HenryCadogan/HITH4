using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CharacterInteraction : MonoBehaviour {
	//Used on the NonPlayerCharacter prefabs 
	//Tells the prefab which character it is and when the character is clicked passes this to the interrogation room and loads it

	//Which character it is
	private NonPlayerCharacter character;

	//Sets the character
	public void SetCharacter(NonPlayerCharacter character){
		this.character = character;
	}

	//When the character is clicked on
	void OnMouseDown() {
		if (GameMaster.instance.GetTurns () > 0) {			//ADDITION BY WEDUNNIT
			//Pass the character and current scene to the interrogation script to be used in the interrogation room
			InterrogationScript.instance.SetInterrogationCharacter (character);
			Debug.Log (character.getNickname ());

			if (!(GameObject.FindWithTag ("local").GetComponent<QuestioningScript> ().Isignored (character.getNickname ()))) {
				InterrogationScript.instance.SetReturnScene (SceneManager.GetActiveScene ().name);
				SceneManager.LoadScene ("Interrogation Room");
			}  

		}
    }
}
            

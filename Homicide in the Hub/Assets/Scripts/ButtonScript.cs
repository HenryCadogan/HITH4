using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {
    //Functions used are called on button presses. Functions assigned on a button are assigned in the inspector


	public void LoadScene(string scene){
		//Loads the given scene, and removes scenario objects if the game is quit.
		if(scene == "Main Menu" && (GameObject.Find("GlobalScripts") != null) && (GameObject.Find("Notebook Canvas") != null)){	//ADDITION BY WEDUNNIT
			Destroy(GameObject.Find("GlobalScripts")); 							//ADDITION BY WEDUNNIT
			Destroy(GameObject.Find("Notebook Canvas")); 						//ADDITION BY WEDUNNIT
		}																		//ADDITION BY WEDUNNIT

		if (GameObject.Find ("SFXSource") != null) {							//ADDITION BY WEDUNNIT
			GameObject.Find ("SFXSource").GetComponent<AudioSource> ().Play ();	//ADDITION BY WEDUNNIT
		}																		//ADDITION BY WEDUNNIT
		SceneManager.LoadScene(scene);
	}

	public void QuitGame(){
		//Closes the game
		Application.Quit ();
	}

    public void back()  // NEW FOR ASSESSMENT 3 
    {
        //Loads the previously stored scene in InterrogationScript.
        string previousScene = InterrogationScript.instance.GetReturnScene();
        SceneManager.LoadScene(previousScene);
		GameMaster.instance.UseTurn ();					//ADDITION BY WEDUNNIT
    }

	public void IgnoreNPC()
    {
        //Loads the previously stored scene in InterrogationScript.
        //Only used in the interogation room.
        //makes that npc unable to talk to untill anothe clue is found
        GameObject.FindWithTag("local").GetComponent<QuestioningScript>().IgnoreNPC(InterrogationScript.instance.GetInterrogationCharacter().getNickname());
        string previousScene = InterrogationScript.instance.GetReturnScene();
        
        SceneManager.LoadScene(previousScene);

    }

	public void LoadMainMenu(){
		SceneManager.LoadScene ("Main Menu");
	}
}

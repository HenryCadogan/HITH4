using UnityEngine;
using System.Collections;

public class InterrogationScript : MonoBehaviour {
	//Used to pass variables from the previous room to the interrogation room


	//__Variables__
	private string previousScene;
	public NonPlayerCharacter character;

	public static InterrogationScript instance = null;

	//Makes a Singleton
	void Awake () {  						//Makes this a singleton class on awake
		if (instance == null) { 			//Does an instance already exist?
			instance = this;				//If not set instance to this
		} else if (instance != this) { 		//If it already exists and is not this then destroy this
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject); 	//Set this to not be destroyed when reloading scene
	}
		
	//Setters
	public void SetInterrogationCharacter(NonPlayerCharacter character){
		this.character = character;
        Debug.Log(character);
	}

	public void SetReturnScene(string sceneName){
		//sets the scene to be returned to after interogation
		this.previousScene = sceneName;
	}

	//Accessors
	public string GetReturnScene(){
		return this.previousScene;
	}
		
	public NonPlayerCharacter GetInterrogationCharacter(){
		return this.character;
	}
}

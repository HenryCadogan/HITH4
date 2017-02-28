using UnityEngine;
using System.Collections;

public class PlayerCharacter : Character {
	//Inherits various variables and methods from the abstract class Character. 
	//PlayerCharacter is used to define the detectives.

	//__Variables__
	private string[] questioningStyles;
	private string description;
	private string overallStyle; 

	//__Constuctor__
	//Inherits characterID, sprite and nickname from Character
	public PlayerCharacter(string characterID,Sprite sprite, string nickname,string overallStyle,string[] questioningStyles, string description) :  base(characterID, sprite,nickname) {
		this.questioningStyles = questioningStyles;
		this.overallStyle = overallStyle;
		this.description = description;
	}

	//__Methods__
	//Accessors
	public string[] GetQuestioningStyles(){
		return this.questioningStyles;
	}

	public string GetOverallQuestioningStyle(){
		return this.overallStyle;
	}

	public string GetDescription(){
		return this.description;
	}
}

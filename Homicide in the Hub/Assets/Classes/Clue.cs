using UnityEngine;
using System.Collections;

abstract public class Clue {
	//Abstract class denoting the variables and operations shared by Item and VerbalClue


	//__Variables__
	private string clueID;
	private string description;

	//__Constructor__
	//Assigns the clue name and description to the instance
	public Clue(string clueID, string description){
		this.clueID = clueID;
		this.description = description;
	}

	//_Accessors__
	public string getID() {
		return this.clueID;
	}

	public string getDescription() {
		return this.description;
	}

}

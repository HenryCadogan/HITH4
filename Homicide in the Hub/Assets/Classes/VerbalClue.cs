using UnityEngine;
using System.Collections;

public class VerbalClue : Clue {
	//Subclass of Clue inherites variables from Clue
	//Owner could be used in Notebook to highlight who you recieved the clue from

	//__Varables__
	private NonPlayerCharacter owner;

	//__Constuctor__
	//Inherits clueID and description from clue
	public VerbalClue(string clueID, string description) : base(clueID, description){
	}

	//Setters
	public void SetOwner (NonPlayerCharacter owner) {
		this.owner = owner;
	}

	//__Accessors__
	public NonPlayerCharacter GetOwner () {
		return this.owner;
	}

}

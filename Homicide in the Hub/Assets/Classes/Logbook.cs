using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Logbook {
	//Stores the Verbal Clues the player collects throughout a playthrough 

	//__Variables__
	//Defines the logbook as a list of VerbalClues
	private List<VerbalClue> logbook = new List<VerbalClue> ();

	//__Construtor__
	public Logbook(){
	}

	//__Operators__
	public void Reset(){
		this.logbook.Clear ();
	}

	public void AddVerbalClueToLogbook(VerbalClue clue){
		logbook.Add(clue);
	}

	//Accessors
	public List<VerbalClue> GetLogbook(){
		return this.logbook;
	}

	public int GetListLength(){
		return logbook.Count;
	}
}

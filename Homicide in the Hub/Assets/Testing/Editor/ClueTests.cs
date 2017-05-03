using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class ClueClassForTesting : Clue {

	public ClueClassForTesting(string clueID, string description) : base(clueID, description) {

	}
}


public class ClueTests {

	[Test]
	public void GetClueIDTest()
	{
		//Arrange
		var clueID = "My Clue ID";
		var clue = new ClueClassForTesting(clueID,null);

		//Assert
		//Can get correct clue id
		Assert.AreEqual(clueID, clue.getID ());
	}

	[Test]
	public void GetClueDescriptionTest()
	{
		//Arrange
		var clueDescription = "My Clue Description";
		var clue = new ClueClassForTesting(null,clueDescription);

		//Assert
		//Can get correct description
		Assert.AreEqual(clueDescription, clue.getDescription ());
	}
}

using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class VerbalClueTests {

	[Test]
	public void SetOwnerTest()
	{
		//Arrange
		var verbalClue = new VerbalClue(null,null);
		var owner = new NonPlayerCharacter (null,null,null,null,null,null);

		//Act
		verbalClue.SetOwner (owner);
	
		//Assert
		Assert.AreSame(verbalClue.GetOwner (), owner);
	}

	[Test]
	public void GetOwnerTest()
	{
		//Arrange
		var verbalClue = new VerbalClue(null,null);
		var owner = new NonPlayerCharacter (null,null,null,null,null,null);
		verbalClue.SetOwner (owner);
		//Act
		//Try to rename the GameObject
		var fetchedOwner = verbalClue.GetOwner ();

		//Assert
		//The object has a new name
		Assert.AreSame(fetchedOwner, owner);
	}
}

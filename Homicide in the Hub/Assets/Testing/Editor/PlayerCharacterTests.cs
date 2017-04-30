using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class PlayerCharacterTesting {

	[Test]
	public void GetQuestioningStylesTest()
	{
		//Arrange
		string[] questioningStyles = {"style1","style2","style3"};
		var player = new PlayerCharacter(null,null, null,null,questioningStyles,null);

		//Assert
		//The object has a new name
		Assert.AreSame(questioningStyles, player.GetQuestioningStyles ());
	}

	[Test]
	public void GetOverallQuestioningStyleTest()
	{
		//Arrange
		var questioningStyle = "My Questioning Style";
		var player = new PlayerCharacter(null,null, null,questioningStyle,null,null);

		//Assert
		//The object has a new name
		Assert.AreSame(questioningStyle, player.GetOverallQuestioningStyle ());
	}

	[Test]
	public void GetDescriptionTest()
	{
		//Arrange
		var description = "My Description";
		var player = new PlayerCharacter(null,null, null,null,null,description);

		//Assert
		//The object has a new name
		Assert.AreSame(description, player.GetDescription ());
	}
}

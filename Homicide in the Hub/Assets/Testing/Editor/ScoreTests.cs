using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class ScoreTests {

	[Test]
	public void ConvertTimeTest()
	{
        //Arrange
        ShowScore scoreclass = new ShowScore();

        //Act
        //Convert the time and store in a variable
        string converted_time = scoreclass.convert_time(90);


        //Assert
        //Check the converted time is correct
        Assert.AreEqual("1:30",converted_time);
	}

	[Test]
	public void CalculateTest()
	{
		//Arrange
		ShowScore scoreclass1 = new ShowScore();

		//Act
        //Calculate the score and store in a variable
		double calculated_score = scoreclass1.caluclate_score(90,2);

		//Assert
        //Check calculated score is correct
		Assert.AreEqual(30, calculated_score);
	}
}

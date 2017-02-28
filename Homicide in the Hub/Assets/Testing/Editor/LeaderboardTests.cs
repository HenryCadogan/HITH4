using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using UnityEngine.UI;

public class LeaderboardTests {

	[Test]
	public void LeaderbaordResetpoistion1Test()
	{
		//Arrange
		Leaderboard leaderboard = new Leaderboard();

        //Act
        // reset all of the leaderbaord scores to 0
        leaderboard.resetscores();
		
		//Assert
        // checks that the key in the playerprefs dictionary with the tag "Score1" is reset to the value 0 
		Assert.AreEqual(PlayerPrefs.GetInt("Score1"),0);
	}

    [Test]
    public void LeaderbaordResetpoistion2Test()
    {
        //Arrange
        Leaderboard leaderboard = new Leaderboard();

        //Act
        // reset all of the leaderbaord scores to 0
        leaderboard.resetscores();

        //Assert
        // checks that the key in the playerprefs dictionary with the tag "Score2" is reset to the value 0 
        Assert.AreEqual(PlayerPrefs.GetInt("Score2"), 0);
    }

    [Test]
    public void LeaderbaordResetpoistion3Test()
    {
        //Arrange
        Leaderboard leaderboard = new Leaderboard();

        //Act
        // reset all of the leaderbaord scores to 0
        leaderboard.resetscores();

        //Assert
        // checks that the key in the playerprefs dictionary with the tag "Score3" is reset to the value 0 
        Assert.AreEqual(PlayerPrefs.GetInt("Score3"), 0);
    }




}

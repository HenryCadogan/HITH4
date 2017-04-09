using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using UnityEngine.UI;

public class LeaderboardTests{
    private Leaderboard leaderboard;

    [TestFixtureSetUp]
    public void LeaderboardSetup(){
        leaderboard = new Leaderboard();
    }

    [Test]
	public void LeaderboardResetpoistion1Test()
	{
        leaderboard.resetscores();
		
		//Assert
        // checks that the key in the playerprefs dictionary with the tag "Score1" is reset to the value 0 
		Assert.AreEqual(PlayerPrefs.GetInt("Score1"),0);
	}

    [Test]
    public void LeaderboardResetpoistion2Test()
    {
       //Act
        // reset all of the leaderbaord scores to 0
        leaderboard.resetscores();

        //Assert
        // checks that the key in the playerprefs dictionary with the tag "Score2" is reset to the value 0 
        Assert.AreEqual(PlayerPrefs.GetInt("Score2"), 0);
    }

    [Test]
    public void LeaderboardrdResetpoistion3Test()
    {
        //Act
        // reset all of the leaderbaord scores to 0
        leaderboard.resetscores();

        //Assert
        // checks that the key in the playerprefs dictionary with the tag "Score3" is reset to the value 0 
        Assert.AreEqual(PlayerPrefs.GetInt("Score3"), 0);
    }




}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour {

    // WHOLE SCRIPT IS NEW FOR ASSESSMENT 3 - LEADERBOARD

    // Whole script is new for assessment three. It is used to display the highscores on the leaderbaord. scores will be saved even if player closes the game
    // playerprefs is used which implements a dictionary to store teh values. 

//    public Text firstscore;   // made public so that the right text objects can be selected inside of unity 
//    public Text secondscore;
//    public Text thirdscore;
//
//    public void uploadscores()    // procedure used to update the scores in the leaderbaord. called when the leaderboard scene is loaded and when scores are reset
//    {
//        if (PlayerPrefs.HasKey("Score1"))
//        {
//            firstscore.text = PlayerPrefs.GetInt("Score1").ToString();   // the firstscore text box is loaded with the value assocaited with the key Score1 but only if a key is present 
//        }
//        if (PlayerPrefs.HasKey("Score2"))
//        {
//            secondscore.text = PlayerPrefs.GetInt("Score2").ToString();  // same as score 1 
//        }
//        if (PlayerPrefs.HasKey("Score3"))
//        {
//            thirdscore.text = PlayerPrefs.GetInt("Score3").ToString();  // same as score 1 
//        }
//    } 

	//CLASS ADDITION BY WEDUNNIT
	/// <summary>
	/// The text object that displays the scores of the leaderboard.
	/// </summary>
	public Text scoreGUI;	//dragged in unity editor
	/// <summary>
	/// The text object that displays the names of the players on the leaderboard.
	/// </summary>
	public Text nameGUI;

	private List<KeyValuePair<string,int>> scoreList = new List<KeyValuePair<string,int>>();	//List of pairs


	/// <summary>
	/// Gets the scores from file & stores them in scoreList.
	/// </summary>
	private void GetScores(){
		KeyValuePair<string,int> scorePair = new KeyValuePair<string,int> ();
		using (StreamReader sr = new StreamReader("leaderboard.txt"))
		{
			int score;
			string name;
			while (sr.EndOfStream == false){
				name = sr.ReadLine();
				score = int.Parse(sr.ReadLine());
				scorePair = new KeyValuePair<string,int> (name, score);
				//print (scorePair.Key);
				//print (scorePair.Value);
				scoreList.Add (scorePair);
			}
			sr.Close();
		}
		scoreList = scoreList.OrderByDescending(x => x.Value).ToList();	//sorts list based on value using linq
	}

	/// <summary>
	/// Shows the scores on leaderboard.
	/// </summary>
	private void ShowScores(){
		string scoreText = "";	//string to be showin in textbox
		string nameText = "";
		for (int i = 0; i < scoreList.Count; i++) {
			scoreText = scoreText + scoreList [i].Value + "\r\n";
			nameText = nameText + scoreList [i].Key + "\r\n";
		}
		if (scoreGUI != null) {
			scoreGUI.text = scoreText;
		}
		if (nameGUI != null) {
			nameGUI.text = nameText;
		}
	}

    public void resetscores()  // procedure used to reset all of the highscores to 0
    {
        //PlayerPrefs.SetInt("Score1", 0);
        //PlayerPrefs.SetInt("Score2", 0);
        //PlayerPrefs.SetInt("Score3", 0);
        //uploadscores();
		if(System.IO.File.Exists("leaderboard.txt"))
		{
			try
			{
				System.IO.File.WriteAllText("leaderboard.txt",string.Empty);
			}
			catch (System.IO.IOException e)
			{
				Console.WriteLine(e.Message);
				return;
			}
		}
    }

    // Use this for initialization
    void Start()
    {
		//uploadscores(); // called when the leaderbaord scene is loaded for the first time
		GetScores ();
		ShowScores ();
    }


}    

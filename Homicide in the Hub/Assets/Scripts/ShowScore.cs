using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ShowScore : MonoBehaviour {

    // ALL IS NEW FOR ASSESSMENT 3 - score 
    public Text scoredisplay;


    private int clues_found;                 // the number of clues found during the game
    private int time;                        // gets the time taken to complete the game in seconds 
    private int final_score;                 // final calculated score used in when adding to high scores

    public void loadvars()
    {
        clues_found = NotebookManager.instance.clue_count();
        time = (int)GameMaster.instance.get_timer();
    }

    public void set_score(int screen)   // sets the score for game and sets the disaplys. Also will call set_highscore if the score is more than 0. 
    {
        loadvars();
        if (screen == 0)
        {
            scoredisplay.text = ("SCORE: \n\n" +                                    // score title 
            "Time taken: " + convert_time(time) +                                   // calls convert time to give the time taken by the player and a nice readable format 
            "\nClues found: " + clues_found + "/12 ") +                             // number of clues found out of 12 
            "\n\n Overall Score: " + caluclate_score(time,clues_found).ToString("0") + " /100";     // overall score out of 100 
            set_highscore();                                                        // call to set_highscore to test if the new score will be on the leaderboard
        } else
        {
            scoredisplay.text = ("SCORE: \n\n" +
            "Time taken: " + convert_time(time) +
            "\nClues found: " + clues_found + "/12 ") +
            "\n\n Overall Score: 0 /100";

        }
    }

    public string convert_time(int time)  // procedure is used to convert the time in second into a time format of mins:seconds so it displays better 
    {
        int mins  = -1;
        int seconds = 0;
        while (time > 0 )
        {
            time = time - 60;
            mins = mins + 1;
        }
        seconds = time + 60;
        if (seconds < 10)
        {
            return mins.ToString() + ":0" + seconds.ToString();
        }
        return mins.ToString() + ":" + seconds.ToString();
    }

    public double caluclate_score(int time, int clues_found)  // procedure used to calculate a score for the game 
    {
        double mul;
        if (time <= 90)   // firstly checks how long it took the player to complete the game and accordingly sets a multiplyer
        {
            mul = 2;
        } else if ( time <= 150 && time > 91)
        {
            mul = 1.5;
        } else if (time > 151 && time <= 210)
        {
            mul = 1.25;
        } else
        {
            mul = 1;
        }
        double score = ((clues_found * 5) * mul)+10;   // formula to set the score based on clues found and mul set
        if (score > 100 )
        {
            final_score = 100;  // sets the final score for use by the set_highscore procedure  
            return 100;
        } else 
        {
            final_score = (int)score; //sets the final score for use by the set_highscore procedure
            return score;             // return the score 
        }
      
    }

    // NEW FOR ASSESSMENT 3 - LEADERBAORD 
    public void set_highscore()  // procedure called by set_score to test if the score is good enough to get a place on the leaderboard
    {
        int third = PlayerPrefs.GetInt("Score3");  // get the score that is in third place
        if (final_score > third)
        {
            int second = PlayerPrefs.GetInt("Score2");  // get the score that is in second place 
            if (final_score > second)
            {
                int first = PlayerPrefs.GetInt("Score1");   // get the score that is in first place
                if (final_score > first)
                {
                    PlayerPrefs.SetInt("Score3", PlayerPrefs.GetInt("Score2"));   // sets third
                    PlayerPrefs.SetInt("Score2", PlayerPrefs.GetInt("Score1"));   // sets second 
                    PlayerPrefs.SetInt("Score1", final_score);                    // sets first with new score  
                    scoredisplay.text += "\n HIGHSCORE ADDED  1ST PLACE";

                } else
                {
                    PlayerPrefs.SetInt("Score3", PlayerPrefs.GetInt("Score2"));   // set third to second 
                    PlayerPrefs.SetInt("Score2", final_score);                    // set second to the new score 
                    scoredisplay.text += "\n HIGHSCORE ADDED  2ND PLACE";
                }
            } else
            {
                PlayerPrefs.SetInt("Score3", final_score);  // set the third with the new sore
                scoredisplay.text += "\n HIGHSCORE ADDED  3RD PLACE";
            }
        } 

        
    }

    // Use this for initialization
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Win Screen")  // if the win screen is loaded calcualte score 
        {
            set_score(0);
        } else
        {
            set_score(1);  // if the lose screen is loaded set score to 0
        }
        
    }
}

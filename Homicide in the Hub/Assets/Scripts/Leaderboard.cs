using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour {

    // WHOLE SCRIPT IS NEW FOR ASSESSMENT 3 - LEADERBOARD

    // Whole script is new for assessment three. It is used to display the highscores on the leaderbaord. scores will be saved even if player closes the game
    // playerprefs is used which implements a dictionary to store teh values. 

    public Text firstscore;   // made public so that the right text objects can be selected inside of unity 
    public Text secondscore;
    public Text thirdscore;

    public void uploadscores()    // procedure used to update the scores in the leaderbaord. called when the leaderboard scene is loaded and when scores are reset
    {
        if (PlayerPrefs.HasKey("Score1"))
        {
            firstscore.text = PlayerPrefs.GetInt("Score1").ToString();   // the firstscore text box is loaded with the value assocaited with the key Score1 but only if a key is present 
        }
        if (PlayerPrefs.HasKey("Score2"))
        {
            secondscore.text = PlayerPrefs.GetInt("Score2").ToString();  // same as score 1 
        }
        if (PlayerPrefs.HasKey("Score3"))
        {
            thirdscore.text = PlayerPrefs.GetInt("Score3").ToString();  // same as score 1 
        }
    } 

    public void resetscores()  // procedure used to reset all of the highscores to 0
    {
        PlayerPrefs.SetInt("Score1", 0);
        PlayerPrefs.SetInt("Score2", 0);
        PlayerPrefs.SetInt("Score3", 0);
        //uploadscores();
    }

    // Use this for initialization
    void Start()
    {
        uploadscores(); // called when the leaderbaord scene is loaded for the first time 
    }


}    

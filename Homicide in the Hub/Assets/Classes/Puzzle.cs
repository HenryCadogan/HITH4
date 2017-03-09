using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Classes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Class to be instantiated everytime a new puzzle is to be solved. The puzzle will load in the text from a JSON file each instance.
/// </summary>
public class Puzzle
{

    private string riddleText, correctAnswer;
    private List<string> wrongAnswers;
    private List<GameObject> answerButtons;
    private GameObject riddleTextObject;

    public GameObject panel;


    //used for determining if the correct button has been pressed
    private int correctIndex;


    public Puzzle(){
        wrongAnswers = new List<string>();
        answerButtons = new List<GameObject>();
        riddleTextObject = GameObject.Find("RiddleText");
        LoadJSON();


        //set riddle text to be the loaded in riddle

        //assign answers to buttons
    }

    private void LoadJSON(){
        JSONObject jo = new JSONObject(File.ReadAllText("Riddles.JSON"));
        //pick random from keys
        int index = Random.Range(0, jo.keys.Count);
        //set the riddle object for simplicity
        JSONObject riddle = jo.GetField(jo.keys[index]);
        //set the riddle text
        riddleText = riddle.GetField("RiddleText").str;
        //set the correct answer text
        correctAnswer = riddle.GetField("CorrectAnswer").str;
        //set the list of incorrect answers
        foreach (var line in riddle.GetField("WrongAnswers").list)
        {
            wrongAnswers.Add(line.str);
        }

        // debugs for JSON Loading
        Debug.Log(riddleText);
        Debug.Log(correctAnswer);
        Debug.Log(wrongAnswers[0]);
    }


    public string GetCorrectAnwer(){
        return correctAnswer;
    }

    public string GetRiddleText(){
        return riddleText;
    }

    public List<string> GetWrongAnswers()
    {
        return wrongAnswers;
    }



}

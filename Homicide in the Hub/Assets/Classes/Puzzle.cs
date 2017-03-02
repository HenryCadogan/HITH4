using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using Assets.Classes;
using UnityEngine;
/// <summary>
/// Class to be instantiated everytime a new puzzle is to be solved. The puzzle will load in the text from a JSON file each instance.
/// </summary>
public class Puzzle
{

    private string riddleText, correctAnswer;
    private List<string> wrongAnswers;



    public Puzzle()
    {
        wrongAnswers = new List<string>();
        LoadJSON();

    }

    private void LoadJSON()
    {
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

        Debug.Log(riddleText);
        Debug.Log(correctAnswer);
        Debug.Log(wrongAnswers.ToString());

    }

    public string GetCorrectAnwer()
    {
        return correctAnswer;
    }


    public string GetRiddleText()
    {
        return riddleText;
    }


}

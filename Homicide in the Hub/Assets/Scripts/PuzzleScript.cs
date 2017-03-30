using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PuzzleScript : MonoBehaviour
{

    public GameObject buttonPanel;
    public Puzzle puzzle;
    private int correctIndex;
    private List<GameObject> answerButtons;


    // Use this for initialization
    void Start()
    {
        //creates a new puzzle object
        puzzle = new Puzzle();
        //instantiate the list;
        answerButtons = new List<GameObject>();
        GameObject.Find("RiddleText").GetComponent<Text>().text = puzzle.GetRiddleText();

        //get the canvas buttons into the list
        for (int x = 0; x <=2; x++)
        {
            answerButtons.Add(buttonPanel.transform.GetChild(x).gameObject);
        }
        AssignAnswers();
    }

    //assigns answers to the buttons
    private void AssignAnswers(){
        //randomise the correct button
        correctIndex = Random.Range(0, 3);
        print("Correct Button is: " + correctIndex);
        //set the correct answer button
        answerButtons[correctIndex].GetComponentInChildren<Text>().text = puzzle.GetCorrectAnwer();
        answerButtons.RemoveAt(correctIndex);
        //assign the incorrect answers
        for (int x = 0; x <= 1; x++){
            answerButtons[x].GetComponentInChildren<Text>().text = puzzle.GetWrongAnswers()[x];
        }
    }

    public void IsCorrect(int index){
        if (index == correctIndex){
            //go into the locked room
            GameMaster.instance.GetPlayerCharacter().unlockPuzzle();
            SceneManager.LoadScene(GameMaster.instance.getLockedRoomIndex());
        }else{
            //use up a turn and return the user to the previous room they were in
            GameMaster.instance.useTurn();
            SceneManager.LoadScene(GameMaster.instance.getPreviousRoom());
        }
    }



}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class PuzzleScript : MonoBehaviour
{

    public GameObject buttonPanel;
    public Puzzle puzzle;
    private int correctIndex;
    private List<GameObject> answerButtons = new List<GameObject>();


    // Use this for initialization
    void Start()
    {
        puzzle = new Puzzle();
        GameObject.Find("RiddleText").GetComponent<Text>().text = puzzle.GetRiddleText();

        for (int x = 0; x <=2; x++)
        {
            print(buttonPanel.transform.GetChild(x).gameObject.name);

            answerButtons.Add(buttonPanel.transform.GetChild(x).gameObject);
        }
        Debug.Log("Answer button count: " + answerButtons.ToList().Count);
        print(puzzle.GetWrongAnswers()[0]);
        AssignAnswers();

    }


    private void AssignAnswers(){
        correctIndex = Random.Range(0, 2);
        for (int x = 0; x <= 2; x++)
        {
            print(x);
            Debug.Log(answerButtons[x].GetComponentInChildren<Text>().text);

            if (x == correctIndex)
            {
                Debug.Log(puzzle == null);
                Debug.Log(puzzle.GetCorrectAnwer());
                answerButtons[x].GetComponentInChildren<Text>().text = puzzle.GetCorrectAnwer();
            }

            //todo clean this up (read: make it nice)
            Debug.Log(puzzle == null);
            Debug.Log(puzzle.GetWrongAnswers());
            answerButtons[x].GetComponentInChildren<Text>().text = puzzle.GetWrongAnswers()[0];
        }
    }

    public void IsCorrect(int index){
        if (index == correctIndex)
        {
            //todo scene transition to the locked room
            SceneManager.LoadScene(GameMaster.instance.getPreviousRoom());
        }
        else
        {
            //todo transition into any other room/the room the character was in.
            GameMaster.instance.useTurn();
            SceneManager.LoadScene(GameMaster.instance.getPreviousRoom());
        }
    }

}

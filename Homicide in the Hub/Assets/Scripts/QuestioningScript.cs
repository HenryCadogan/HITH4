using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestioningScript : MonoBehaviour
{
    //Used when the player selects the questioning option but also to set the correct sprites in the scene when entering it
    //Handles the GUI portion of this part of interrogation.
    //We have chosen to use 'interogation' when refering to questioning or accusing a character


    //__Variables__
    //public to allow drag and drop in inspector
    private PlayerCharacter detective;
    private NonPlayerCharacter character;
    public GameObject detectiveGameObject;
    public GameObject characterGameObject;
	public Text characterName; //ADDITION BY WEDUNNIT

    public Text[] detectiveStylesText = new Text[3]; //Where Left-most button is 1 and rightmost is 3
    public Text clueSpeech;         //Where the clue text is written to
    // new for ASSESSMENT 3 // 
    static private string[] ignpcs = new string[10];  //holds all of the npcs that are being ignored 
    static private int numIgnoredNPCs = 0;  //the number of npcs that are being ignored the variable is static so that the value deosnt change over the the different scenes loaded 
    
    void Start()
    {
        if (SceneManager.GetActiveScene().name == ("Interrogation Room"))
        {
            //Get detective and character from singletons
            detective = GameMaster.instance.GetPlayerCharacter();
            character = InterrogationScript.instance.GetInterrogationCharacter();

            //Change sprites to correct characters
            SpriteRenderer detectiveSR = detectiveGameObject.GetComponent<SpriteRenderer>();
            detectiveSR.sprite = detective.getSprite();

            SpriteRenderer characterSR = characterGameObject.GetComponent<SpriteRenderer>();
            characterSR.sprite = character.getSprite();

			characterName.text = character.getCharacterID (); //ADDITION BY WEDUNNIT


            //Set Text in Style buttons to Styles of chosen detective
            for (int i = 0; i < 3; i++)
            {
                detectiveStylesText[i].text = detective.GetQuestioningStyles()[i];
            }
        }
    }

    public void QuestionCharacter(int reference)
    {
        //reference passes the question style reference i.e leftmost button=0, middle=1, rightmost=2
        string choice;
        List<string> weaknesses;
        string response;
        choice = GetQuestioningChoice(reference);
        weaknesses = character.GetWeaknesses();

        if ((weaknesses.Contains(choice)) && (character.getVerbalClue() != null))
        { //If chosen style is a weakness to the character
            VerbalClue clue = character.getVerbalClue();
            if (!NotebookManager.instance.logbook.GetLogbook().Contains(character.getVerbalClue()))
            { //If the logbook doesnt contain the clue already
                response = "Clue Added: " + clue.getDescription();                  //Change the responce and add to the logbook
                NotebookManager.instance.logbook.AddVerbalClueToLogbook(clue);
                NotebookManager.instance.UpdateNotebook();
            }
            else
            {
                response = "Clue Already Obtained";         //Otherwise state the clue is already in the logbook
            }
        }
        else
        {
            response = character.GetResponse(choice);           //Otherwise just give the responce
        }
        clueSpeech.text = response;     //Update the UI Text with the appropriate repsonce
    }

    private string GetQuestioningChoice(int reference)
    {
        string choice = detective.GetQuestioningStyles()[reference];
        return choice;
    }

           // NEW FOR ASSSESSMENT 3 // 
            // Ignore Procedures //

    //adds a new npc to the array of ignored npcs
     public void IgnoreNPC(string ignoreCharacter)
    {
        ignpcs[numIgnoredNPCs] = ignoreCharacter;
        numIgnoredNPCs += 1;
    } 


    //clears the list ofignored npcs
    public void UnignoreNPC()
    {
        for (int x = 0; x <= numIgnoredNPCs; x += 1) //loops through the list
        {
            ignpcs[x] = null; //clears list
        }
        numIgnoredNPCs = 0; //resets num of npcs in list
    }

    //checks if a npc is currently ignored
    //returns true if they are ignored, false if not
    public bool Isignored(string name)
    {
        int check = 0;
        for (int i = 0; i<10 ; i++)
        {
            if (ignpcs[i] == name)
            {
                check = 1;
            }
        }

        if (check == 0)
        {
            return false;
        }
         else
        {
            return true;
        }

        
        
    }
}
   

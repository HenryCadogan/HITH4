using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//Used for editing text compnents ADDITION BY WEDUNNIT

public class GameMaster : MonoBehaviour {
	/* Initialises all of the objects required generate the mystery and the game world except the detectives and verbal clues. 
	 * Distibutes the clues and characters throughout the scenes.
	*/
	public Scenario scenario; 

	//Arrays 
	public static GameMaster instance;
	static public Scene[] scenes;
	public Item[] itemClues;
	public VerbalClue[] verbalClues;
	public NonPlayerCharacter[] characters;
	public List<Clue> relevantClues;
	private MurderWeapon[] murderWeapons;
	public PlayerCharacter[] playerCharacters = new PlayerCharacter[2];	//ADDITION BY WEDUNNIT

    // NEW FOR ASSESSMENT 3 - locked room feature
    public Item keyobj;
    private bool foundkey = false;

	//NPC Sprites
	//Made public to allow for dragging and dropping of Sprites
	public Sprite pirateSprite;
	public Sprite mimesSprite;
	public Sprite millionaireSprite;
	public Sprite cowgirlSprite;
	public Sprite romanSprite;
	public Sprite wizardSprite;
    // NEW FOR ASSESSEMNT 3 - adding mroe npcs 
	public Sprite robotSprite;
	public Sprite astrogirlSprite;
	public Sprite chefSprite;
	public Sprite madscientistSprite;

	//NPC Prefabs
	//Made public to allow for dragging and dropping of prefabs
	public GameObject piratePref;
	public GameObject mimesPref;
	public GameObject millionarePref;
	public GameObject cowgirlPref;
	public GameObject romanPref;
	public GameObject wizardPref;

    // NEW FOR ASSESSMENT 3 - adding more npcs 
	public GameObject robotPref;
	public GameObject astrogirlPref;
	public GameObject chefPref;
	public GameObject madscientistPref;


	//Item Sprite decleratation
	//Made public to allow for dragging and dropping of sprites
	public Sprite cutlassSprite;
	public Sprite poisonSprite;
	public Sprite garroteSprite;
	public Sprite knifeSprite;
	public Sprite laserGunSprite;
	public Sprite leadPipeSprite;
	public Sprite westernPistolSprite;
	public Sprite wizardStaffSprite;
	public Sprite beretSprite;
	public Sprite footprintsSprite;
	public Sprite glovesSprite;
	public Sprite wineSprite;
	public Sprite shatteredGlassSprite;
	public Sprite shrapnelSprite;
	public Sprite smellyDeathSprite;
	public Sprite spellbookSprite;
	public Sprite tripwireSprite;
    public Sprite keySprite;

	//Item Prefabs 
	//Made public to allow for dragging and dropping of prefabs
	public GameObject cutlassPrefab;
	public GameObject poisonPrefab;
	public GameObject garrotePrefab;
	public GameObject knifePrefab;
	public GameObject laserGunPrefab;
	public GameObject leadPipePrefab;
	public GameObject westernPistolPrefab;
	public GameObject wizardStaffPrefab;
	public GameObject beretPrefab;
	public GameObject footprintsPrefab;
	public GameObject glovesPrefab;
	public GameObject winePrefab;
	public GameObject shatteredGlassPrefab;
	public GameObject shrapnelPrefab;
	public GameObject smellyDeathPrefab;
	public GameObject spellbookPrefab;
	public GameObject tripwirePrefab;
    public GameObject keyPrefab;

	private NonPlayerCharacter murderer;
    
	//Arrays for riddle room 		BY WEDUNNIT
    private int lockedRoomIndex;
	private string lockedRoomName;
    private bool[] playerHasPassedRiddle = new bool[2];
    private int[] playerPreviousRoom = new int[2];

	//Relevant Clues
	private List<Item> relevant_items;
	private List<VerbalClue> relevant_verbal_clues;


    // floats for the timer
	private float[] timers = {0f,0f};
    private bool run_timer = false;

	//Multiplayer Variables ADDITION BY WEDUNNIT
	public bool isMultiplayer;
	private const int TURNS_PER_GO = 2;
	int currentTurns = TURNS_PER_GO;
	int currentPlayerIndex = 0;
	int[] collectedClueCount = new int[2];	//stores collected clue counters used for scoring
	public string[] playerCurrentRoom = new string[2] {"Atrium","Atrium"}; 	//stores room occupied by each player for when detectives switch

	//Score variables
	private float[] ScoreArray;

	//Sets as a Singleton
	void Awake () {  //Makes this a singleton class on awake
		if (instance == null) { //Does an instance already exist?
			instance = this;	//If not set instance to this
		} else if (instance != this) { //If it already exists and is not this then destroy this
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject); //Set this to not be destroyed when reloading scene
	}


    void Start()
    {
        //Initialises Variables
        //Responce Arrays
        string[] pirateResponses = new string[9]
        {
            "Shiver me timbers I know nothing!",
            "Arrr matey it ain’t that difficult to understand.",
            "Shiver me timbers, how dare ye threaten me!",
            "Arrr, no matey I really don’t think I do. I saw nothing!",
            "Ho ho ho, Arr matey I didn’t see anything!",
            "Shiver me timbers ye need to plan ye lunch breaks better!",
            "Arrr matey I saw nothing of import.",
            "Arr, matey I suppose ye do, but I saw nothing.",
            "Arrr matey I don’t think ye need my help to solve this conundrum."
        };

        string[] mimeResponses = new string[9]
        {
            "The mimes are taken aback, but contribute nothing more.",
            "The mimes shake their heads.",
            "The mimes flinch, but tell you nothing.",
            "The mimes shake their heads.",
            "The mimes imitate laughter but tell you nothing.",
            "The mimes look at their wrist watches with a puzzled expression.",
            "The mines mime out some routine which doesn’t make sense and contribute nothing.",
            "The mimes look at you with passing curiosity but contribute nothing more.",
            "The mimes shake their heads. They tell you nothing."
        };

        string[] millionaireResponses = new string[9]
        {
            "Don’t try and force me to tell you anything. I’ve got more money than you.",
            "Don’t patronise me you cretin. I’ve got more money than you.",
            "How dare you threaten me you lunatic, I’ve got more money than you.",
            "No my dear fellow for you see I have more money than you.",
            "Ha ha ha. Not that funny dear fellow, you’ll need more money to make it funnier.",
            "My good man, I know that time is money, but you can’t rush magnificence!",
            "My good man, there isn’t enough money around here to warrant seeing anything.",
            "I thank you for your kindness, but it would be better with some patronage!",
            "My good man, you don’t need my help to solve this. Not to mention there’s no money involved."
        };

        string[] cowgirlResponses = new string[9]
        {
            "I appreciate your candour pardner but I didn’t see anything.",
            "Pardner I do understand, I just didn’t see anything.",
            "Pardner I don’t appreciate threats so don’t try it.",
            "Pardner I didn’t see anything, I wasn’t paying attention.",
            "Ha ha ha, funny pardner but I still didn’t see anything.",
            "I don’t know nuthin'. If you’ve got to be gone by high noon, I’d go ask someone else.",
            "I understand pardner but I didn’t see anything",
            "Thank you pardner, but I didn’t see anything. ",
            "Pardner, you’ll have to solve this one without my help, I didn’t see anything."
        };

        string[] romanResponses = new string[9]
        {
            "What Ho! I understand you want to solve the problem but I saw nothing!",
            "What Ho! Yes I understand, but I saw nothing!",
            "What Ho! Don’t try and threaten me you madman!",
            "What Ho! I didn’t see anything.",
            "What Ho ho ho ho! That’s funny but I saw nothing of interest.",
            "What Ho! I feel you’re trying to rush an answer out of me! Nay I say, Nay!",
            "What Ho! My good man you are inquisitive but I don’t know anything.",
            "What Ho! Thanks my good man but I didn’t see anything.",
            "What Ho!  My good man I’m sorry but I saw nothing."
        };

        string[] wizardResponses = new string[9]
        {
            "Errrm...are you sure I can’t interest you in some merchandise instead?",
            "Errrm...I do understand what is going on, I just didn’t see anything.",
            "Errrm...I think you might need to calm down, I’ve got something for that.",
            "Errrm...I saw nothing but I have seen some of my merchandise, would you like some?",
            "Hee hee hee...that's funny. Errrm...but I still saw nothing though.",
            "*Looks around shiftily* Sorry mate, I don’t know anything.",
            "Errrm...I understand, but wouldn’t you prefer to buy some merchandise instead?",
            "Errrm...yes there was something…. But I’ve forgotten it now.",
            "Errrm...are you sure? I’m not that useful really."
        };

        // NEW FOR ASSSESSMENT 3 - REPOSNES FOR ADDING NEW NPCS

        string[] astrogirlResponses = new string[9]
        {
            "Hah, as if you can force me to say anything incriminating, earthling!",
            "Hey, careful. You don't know who you're talking to!",
            "Okay, friend, you ain't fooling anyone with that scary attitude!",
            "I'd like to help a respectable man such as yourself, but I haven’t seen anything.",
            "Hahaha, I might just steal that joke! Yet, I haven't seen anything.",
            "Hold your horses, mate, I haven't seen anything of note!",
            "All of these questions aren't going to magically make me remember something I haven't seen.",
            "Well, aren't you a kind fellow? Still, I haven't seen anything.",
            "Hey, you know more than me about this, so you clearly don't need my help."
        };

        string[] chefRepsonses = new string[9]
        {
            "Sacré bleu! You are very rude, monsieur detective!",
            "How dare you patronise me, monsieur! I am the best Chef in the world!",
            "Oh! No need for this, my friend! I have not seen one thing!",
            "Qui, qui! I would love to help, but I know nothing.",
            "Hahaha, very funny, monsieur! However, I don't know anything aside from the culinary arts!",
            "Monsieur, slow down, please! English is not my first language!",
            "So many questions, but I don't have the answer for any of them.",
            "Oh! That is very kind, monsieur, but the only thing I know is how great my food is!",
            "What did you say, monsieur? Would you like to taste this freshly baked baguette?"
        };

        string[] madscientistRepsonses = new string[9]
        {
            "Do you know who you are talking to! Watch your language, peasant!",
            "This patronising attitude of yours is annoying, I don't know anything that would interest you!",
            "Hahaha, as if someone like you could intimidate someone like me!",
            "I have no interest in this affair. It's not like I am the guilty one this time!",
            "Your childish humour doesn’t compel me to help you at all…",
            "I have no respect for people who lack patience! There's nothing I can tell you!",
            "You ask too many questions for a pathetic commoner such as yourself.",
            "You’re too kind for your own good, detective. I have no information regarding this case.",
            "Very inspiring, but I have no interest in solving this murder."
        };

        string[] robotResponses = new string[9]
        {
            "Goodness me! I am not programmed to answer to this kind of attitude, beep boop.",
            "Don't you dare patronise me, you mean glob of grease! Beep boop.",
            "Oh my, you are scary… I don't know anything, unfortunately. Beep boop.",
            "Helping humans is part of my protocol. However, I haven't seen anything important. Beep boop.",
            "You humans and your weird sense of humour… I could never understand it. Beep boop.",
            "I can perform thousands of calculations every second… Even so I don't have any important information about this terrible crime. Beep boop.",
            "If I told you half of the things I've heard about all of the people here, you'd short circuit… Oh wait, humans don't short circuit.",
            "Oh my, that is very kind of you to say, detective! I haven't seen anything important though.",
            "I'm sorry, detective, but there's nothing that I can do to help you. I'm sure you'll manage on your own."
        };

        // NEW FOR ASSESSEMNT 3 - IGNORE
        NonPlayerCharacter[] ignoredNPCs = new NonPlayerCharacter[6];

        //Weaknesses
        List<string> pirateWeaknesses = new List<string> {"Forceful", "Wisecracking", "Kind"};
        List<string> mimeWeaknesses = new List<string> {"Intimidating", "Coaxing", "Inspiring"};
        List<string> millionaireWeaknesses = new List<string> {"Forceful", "Rushed", "Kind"};
        List<string> cowgirlWeaknesses = new List<string> {"Condescending", "Wisecracking", "Inspiring"};
        List<string> romanWeaknesses = new List<string> {"Condescending", "Coaxing", "Inquisitive"};
        List<string> wizardWeaknesses = new List<string> {"Intimidating", "Rushed", "Inquisitive"};
        List<string> robotWeaknesses = new List<string> {"Intimidating", "Coaxing", "Kind"};
        List<string> astrogirlWeaknesses = new List<string> {"Forceful", "Wisecracking", "Inspiring"};
        List<string> chefWeaknesses = new List<string> {"Condescending", "Coaxing", "Inquisitie"};
        List<string> madscientistWeaknesses = new List<string> {"Forceful", "Rushed", "Kind"};


        //Defining NPC's
        NonPlayerCharacter pirate = new NonPlayerCharacter("Captain Bluebottle", pirateSprite, "Salty Seadog",
            piratePref, pirateWeaknesses, pirateResponses);
        NonPlayerCharacter mimes = new NonPlayerCharacter("The Mime Twins", mimesSprite, "mimes", mimesPref,
            mimeWeaknesses, mimeResponses);
        NonPlayerCharacter millionaire = new NonPlayerCharacter("Sir Worchester", millionaireSprite, "Money Bags",
            millionarePref, millionaireWeaknesses, millionaireResponses);
        NonPlayerCharacter cowgirl = new NonPlayerCharacter("Jesse Ranger", cowgirlSprite, "Outlaw", cowgirlPref,
            cowgirlWeaknesses, cowgirlResponses);
        NonPlayerCharacter roman = new NonPlayerCharacter("Celcius Maximus", romanSprite, "Legionnaire", romanPref,
            romanWeaknesses, romanResponses);
        NonPlayerCharacter wizard = new NonPlayerCharacter("Randolf the Deep Purple", wizardSprite, "Dodgy Dealer",
            wizardPref, wizardWeaknesses, wizardResponses);

        // NEW FOR ASSESSMETN 3 - ADDING NEW NPCS //
        NonPlayerCharacter robot = new NonPlayerCharacter("Droid Mayweather", robotSprite, "Mean Machine", robotPref,
            robotWeaknesses, robotResponses);
        NonPlayerCharacter astrogirl = new NonPlayerCharacter("Astrigirl", astrogirlSprite, "Spacegirl", astrogirlPref,
            astrogirlWeaknesses, astrogirlResponses);
        NonPlayerCharacter chef = new NonPlayerCharacter("Philip Mingot", chefSprite, "The Gastronomer", chefPref,
            chefWeaknesses, chefRepsonses);
        NonPlayerCharacter madscientist = new NonPlayerCharacter("Professor Bon Vose", madscientistSprite, "Dr. Evil",
            madscientistPref, madscientistWeaknesses, madscientistRepsonses);

        //Defining Scenes
        Scene controlRoom = new Scene("Control Room");
        Scene kitchen = new Scene("Kitchen");
        Scene lectureTheatre = new Scene("Lecture Theatre");
        Scene lakehouse = new Scene("Lakehouse");
        Scene islandOfInteraction = new Scene("Island of Interaction");
        Scene roof = new Scene("Roof");
        Scene atrium = new Scene("Atrium");
        Scene undergroundLab = new Scene("Underground Lab");

        //Defining Items
        MurderWeapon cutlass = new MurderWeapon(cutlassPrefab, "Cutlass", "A worn and well used cutlass", cutlassSprite,
            "SD");
        MurderWeapon poison = new MurderWeapon(poisonPrefab, "Empty Poison Bottle", "An empty poison bottle ",
            poisonSprite, "SD");
        MurderWeapon garrote = new MurderWeapon(garrotePrefab, "Garrote", "Used for strangling a victim to death",
            garroteSprite, "SD");
        MurderWeapon knife = new MurderWeapon(knifePrefab, "Knife", "An incredibly sharp tool meant for cutting meat",
            knifeSprite, "SD");
        MurderWeapon laserGun = new MurderWeapon(laserGunPrefab, "Laser Gun",
            "It's still warm which implies it has been recently fired", laserGunSprite, "SD");
        MurderWeapon leadPipe = new MurderWeapon(leadPipePrefab, "Lead Pipe",
            "It's a bit battered with a few dents on the side", leadPipeSprite, "SD");
        MurderWeapon westernPistol = new MurderWeapon(westernPistolPrefab, "Western Pistol",
            "The gunpowder residue implies it has been recently fired", westernPistolSprite, "SD");
        MurderWeapon wizardStaff = new MurderWeapon(wizardStaffPrefab, "Wizard Staff",
            "The gems still seem to be glow as if it has been used recently", wizardStaffSprite, "SD");
        Item beret = new Item(beretPrefab, "Beret", "A hat most stereotypically worn by the French", beretSprite);
        Item footprints = new Item(footprintsPrefab, "Bloody Footprints",
            "Bloody footprints most likely left by the murderer", footprintsSprite);
        Item gloves = new Item(glovesPrefab, "Bloody Gloves", "Bloody gloves most likely used by the murderer",
            glovesSprite);
        Item wine = new Item(winePrefab, "Fine Wine", "An expensive vintage that's close to 100 years old", wineSprite);
        Item shatteredGlass = new Item(shatteredGlassPrefab, "Shattered Glass",
            "Broken glass shards spread quite close together", shatteredGlassSprite);
        Item shrapnel = new Item(shrapnelPrefab, "Shrapnel", "Shrapnel from an explosion or gun being fired",
            shrapnelSprite);
        Item smellyDeath = new Item(smellyDeathPrefab, "Smelly Death", "All that remains of the victim",
            smellyDeathSprite);
        Item spellbook = new Item(spellbookPrefab, "Spellbook",
            "A spellbook used by those who practise in the magic arts", spellbookSprite);
        Item tripwire = new Item(tripwirePrefab, "Tripwire",
            "A used tripwire most likely used to immobilize the victim", tripwireSprite);

        // NEW FOR ASSESSMENT 3 - LOCKED ROOM FEATURE
        Item key = new Item(keyPrefab, "Key", "Key has the words underground lab on it", keySprite);

        murderWeapons = new MurderWeapon[8]
            {cutlass, poison, garrote, knife, laserGun, leadPipe, westernPistol, wizardStaff};
        itemClues = new Item[9]
            {beret, footprints, gloves, wine, shatteredGlass, shrapnel, smellyDeath, spellbook, tripwire};
        characters = new NonPlayerCharacter[10]
            {pirate, mimes, millionaire, cowgirl, roman, wizard, robot, astrogirl, chef, madscientist};
        scenes = new Scene[8]
            {atrium, lectureTheatre, lakehouse, controlRoom, kitchen, islandOfInteraction, roof, undergroundLab};
        keyobj = key;
        //Set locked room index
        lockedRoomIndex = Random.Range(5, 11);
        Debug.Log("Locked room is: " + SceneManager.GetSceneByBuildIndex(lockedRoomIndex).name + "At Build index: " + lockedRoomIndex);
    }

	public int GetScore(int playerIndex){
		return (int)ScoreArray [playerIndex];
	}

	public void GivePoints(float points, int playerIndex){
		//TODO: assert
		ScoreArray[playerIndex] += points;
	}

	public void TakePoints(float points, int playerIndex){
		//TODO: assert
		ScoreArray[playerIndex] -= points;
	}

	public void GivePoints(float points){
		//TODO: assert
		ScoreArray[currentPlayerIndex] += points;
	}

	public void TakePoints(float points){
		//TODO: assert
		ScoreArray[currentPlayerIndex] -= points;
	}

	//TODO: refactor these two out
   public int GetP1Score(){
		int intScore = (int)ScoreArray[0];
		return intScore;
	}

	public int GetP2Score(){
		int intScore = (int)ScoreArray[1];
		return intScore;
	}

    void AssignNPCsToScenes(NonPlayerCharacter[] characters, Scene[] scenes){
		int sceneCounter = 0;
		Shuffler shuffler = new Shuffler ();
		shuffler.Shuffle (characters);
		shuffler.Shuffle (scenes);
		foreach (NonPlayerCharacter character in characters){	//For every character in the randomly shuffled array
			scenes [sceneCounter].AddNPCToArray (character);		//Assign a character to a scene
			sceneCounter += 1;                                  //Increment sceneCounter

            //NEW FOR ASSESSMENT 3 - ADDING NEW NPC INTO THE GAME //

            if (sceneCounter >= scenes.Length) {					//If the counter is above or equal to the number of scenes cycle to the first scene 
				sceneCounter = 0;
			}
		}
	}

	void AssignItemsToScenes(Item[] items, Scene[] scenes) {
		int sceneIndex = 0;
		Shuffler shuffler = new Shuffler ();
		shuffler.Shuffle (items);
		shuffler.Shuffle (scenes);
        foreach (Item item in items) {
            scenes[sceneIndex].AddItemToArray(item);
            sceneIndex++;
            if (sceneIndex > scenes.Length) {
                sceneIndex = 0;
            }
            // NEW FOR ASSESSMENT 3 - LOCKED ROOM 
        }
    }

	public void CreateNewGame(PlayerCharacter detective, PlayerCharacter detective2=null, bool isMulti = false){ //Called when the player presses play //UPDATED BY WEDUNNIT
		//Reset values from a previous playthough
		ResetNotebook();
		ResetAll(scenes);

		//Create a Scenario
		scenario = new Scenario (murderWeapons, itemClues, characters);
		isMultiplayer = isMulti; 	//ADDITON BY WEDUNNIT

		scenario.chooseMotive ();
		string motive = scenario.getMotive ();
		murderer = scenario.chooseMurderer ();
		scenario.chooseWeapon ();
		MurderWeapon weapon = scenario.getWeapon ();
       

		scenario.CreateVerbalClues (motive, weapon, murderer); 
		scenario.BuildCluePools (motive, murderer, weapon);
		scenario.DistributeVerbalClues (murderer);

		itemClues = scenario.getItemCluePool ().ToArray ();
		characters = scenario.getNPCs ();
		verbalClues = scenario.getVerbalCluePool ().ToArray ();
		relevant_items = scenario.getRelevantItems ();
		relevant_verbal_clues = scenario.getRelevantVerbalClues ();
		relevantClues = scenario.getRelevantClues (); 
        

		//Assign To rooms
		AssignNPCsToScenes (characters,scenes);				//Assigns NPCS to scenes
		AssignItemsToScenes (itemClues,scenes);				//Assigns Items to scenes

		//Assigns detectives to array. Detective 2 is null if the game is not multiplayer
		playerCharacters[0] = detective;					//ADDITON BY WEDUNNIT
		playerCharacters[1] = detective2;					//ADDITON BY WEDUNNIT

		if (isMultiplayer) {
			ScoreArray = new float[2]{ 1000, 1000 };
		} else {
			ScoreArray = new float[1]{ 1000 };
		}
	}	

	public void SwitchPlayers(){							//alternates the current character & switches to their room ADDITION BY WEDUNNIT
		currentPlayerIndex = 1 - currentPlayerIndex;
		currentTurns = TURNS_PER_GO;
		GameObject.Find("Local Scripts").GetComponent<LevelManager> ().DisplayCharacterChange();
	}

	public PlayerCharacter GetPlayerCharacter(){
		if (currentTurns <= 0 && isMultiplayer) {		//ADDITION BY WEDUNNIT
			SwitchPlayers ();								//ADDITION BY WEDUNNIT
		}
		return playerCharacters[currentPlayerIndex];
	}

	public int GetCurrentPlayerIndex(){						//ADDITION BY WEDUNNIT
		return currentPlayerIndex;
	}



	public Scene GetScene(string sceneName){
	    foreach (Scene scene in scenes){
	        if (scene.GetName () == sceneName) {
	            return scene;
	        }
	    }
	    return null;
	}

	public void ClueCollected(){							//method to increment score count each time a clue is collected, used for multiplayer scoring ADDITION BY WEDUNNIT
		collectedClueCount [currentPlayerIndex]++;
		GivePoints (50, currentPlayerIndex);
		print ("Clues collected by current player: " + collectedClueCount [currentPlayerIndex].ToString());
	}

	public void ResetAll(Scene[] scenes){
		foreach (Scene scene in scenes) {
			scene.ResetScene ();
		}

	}

	private void DisplayTurns(int currentTurns){	//Displays current turns to the screen if playing multiplayer ADDITION BY WEDUNNIT,
		if (isMultiplayer) {
			if (GameObject.Find ("Turn Counter") != null) {
				GameObject.Find ("Turn Counter").GetComponent<Text> ().text = "Turns remaining: " + currentTurns;
			}
		}
	}

	public int GetTurns(){		//ADDITIOM BY WEDUNNIT
		return currentTurns;
	}

	/// <summary>
	/// Uses a turn. ADDITION BY WEDUNNIT
	/// </summary>
	/// <returns><c>true</c>, if there are turns left, <c>false</c> otherwise.</returns>
	public bool UseTurn(){
		currentTurns--;
		DisplayTurns (currentTurns);
		return currentTurns <= 0;
	}

	public List<Item> GetRelevantItems(){
		return relevant_items;
	}

	public List<VerbalClue> GetRelevantVerbalClues(){
		return relevant_verbal_clues;
	}

	public string GetMurderer(){
		return murderer.getCharacterID();
	}

	private void ResetNotebook(){
		NotebookManager.instance.ResetSelectedClues ();
		NotebookManager.instance.logbook.Reset();	//Reset logbook
		NotebookManager.instance.inventory.Reset();	//Reset inventory
		NotebookManager.instance.UpdateNotebook();
	}

    // NEW FOR ASSESSMENT 3 - TIMER FOR SCORING IN THE GAME 

    public void stop_timer() // stop the timer called when the game is finshed 
    {
        run_timer = false;
    }
    public void set_timer() // called when the game starts 
    {
        run_timer = true;
    }
    public float get_timer()  // called at the end to calaute the score based on the time taken 
    {
		return timers[currentPlayerIndex];
    }

    private void Update()  //update function will update the variable timer which holds hte time taken in the game by 1 every second. 
    {
        if (run_timer)
        {
			TakePoints (Time.deltaTime, currentPlayerIndex);
//			timers[currentPlayerIndex] += Time.deltaTime;  	// time.deltatime is a built in which uses seconds to indicate when to update values by 1
			for (int i = 0; i<ScoreArray.Length; i++){							//For both characters, print score each frame ADDITION BY WEDUNNIT
				string textBoxName = "Player " + (i + 1).ToString() + " Time";				//ADDIITON BY WEDUNNIT
				string displayedText = textBoxName + ": " + GetScore(i).ToString();	//ADDITION BY WEDUNNIT
				if (GameObject.Find (textBoxName) != null){									//ADDITION BY WEDUNNIT
					GameObject.Find (textBoxName).GetComponent<Text>().text = displayedText;	// Updates relevent buttonPanel, ADDITION BY WEDUNNIT
				}
			}
        }
    }

    // NEW FOR ASSSESSMENT 3 - LOCKED ROOM FEATURE
    // KEY IS BEING REPLACED BY RIDDLE FOR ASSESSMENT 4

    //THE FOLLOWING ARE ADDITIONS BY WEDUNNIT
    //checks if the current player has passed the riddle in this instance of the game
    public bool HasPassedRiddle(){
        return playerHasPassedRiddle[currentPlayerIndex];
    }

    //The current player has passed the riddle
    public void PassRiddle(){
        playerHasPassedRiddle[currentPlayerIndex] = true;
    }

    //returns the index of the last room that the player was in, incase they fail the riddle
    public int GetPreviousRoom(){
        return playerPreviousRoom[currentPlayerIndex];
    }

	/// <summary>
	/// Stores currently loaded room for each player
	/// </summary>
	/// <param name="buildIndex">Build index.</param>
	public void SaveCurrentPlayerRoom(string level){	//BY WEDUNNIT
		this.playerCurrentRoom[currentPlayerIndex] = level;
	}

	public void SetLockedRoomName(string name){
		lockedRoomName = name;
	}

	public string GetLockedRoomName (){
		return lockedRoomName;
	}

    //sets the current room to be the last room the player was in when they traverse to another room
    public void SetPreviousRoom(int roomIndex){
        playerPreviousRoom[currentPlayerIndex] = roomIndex;
    }
    //returns the integer index of the locked room for which the riddle has to be passed in order to enter
    public int GetLockedRoomIndex(){
        return lockedRoomIndex;
    }

}
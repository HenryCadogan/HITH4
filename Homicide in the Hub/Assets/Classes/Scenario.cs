using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Scenario
	{

	// Arrays to store elements passed to Scenario class from GameMaster
	private MurderWeapon[] weapons;
	private Item[] item_clues;
	private VerbalClue[] verbal_clues;
	private NonPlayerCharacter[] npcs;

	// Intialising variables which will be returned to GameMaster
	private List<Item> item_clue_pool = new List<Item> ();
	private List<VerbalClue> verbal_clue_pool = new List<VerbalClue> ();
	private List<Item> relevant_item_clues = new List<Item> ();
	private List<VerbalClue> relevant_verbal_clues = new List<VerbalClue> ();
	private MurderWeapon weapon;
	private string motive;

	private string[] motives = { "homewrecker", "loanshark", "promotion", "unfriended", "blackmail", "avenge_friend", "avenge_pet" };

	// Constructor for Scenario
	public Scenario (MurderWeapon[] murder_weapons, Item[] items, NonPlayerCharacter[] characters)
	{
		weapons = murder_weapons;
		item_clues = items;
		npcs = characters;
	}

	// Chooses a random weapon from the MurderWeapon array 'weapons'
	public void chooseWeapon() {
		Shuffler shuffler = new Shuffler ();
		shuffler.Shuffle (weapons);
		weapon = weapons [0];
       
	}
			
	// Chooses a random motive from the string[] 'motives' initialised above
	public void chooseMotive() {
		Shuffler shuffler = new Shuffler ();
		shuffler.Shuffle (motives);
		motive = motives [0];
	}

	// Chooses and returns a random murderer from the NonPlayerCharacter[] 'npcs'
	public NonPlayerCharacter chooseMurderer() {
		Shuffler shuffler = new Shuffler ();
		shuffler.Shuffle (npcs);
		NonPlayerCharacter murderer = npcs [0];
		murderer.SetAsMurderer ();
        Debug.Log(murderer.getCharacterID()); // write to console the murderer 
		return murderer;
	}

	// given a murderer, weapon and motive, creates a VerbalClue[] containing 6 relevant verbal clues
	public void CreateVerbalClues(string motive, MurderWeapon weapon, NonPlayerCharacter murderer) {

		string murderer_name = murderer.getNickname ();
		string weapon_name = weapon.getID ();

		VerbalClue disposing_of_weapon = new VerbalClue ("Disposing of a Weapon", "I saw "+murderer_name+" trying to " +
			"dispose of a "+weapon_name+".");

		string old_friends_description = "The victim and "+murderer_name+" fell out ";
		string motive_clause = ".";
		if (motive == "homewrecker") {
			string partner_gender;
			int binary = Random.Range (0, 1);
			if (binary == 0) {
				partner_gender = "wife";
			} else {
				partner_gender = "husband";
			}
			motive_clause = "because the victim slept with their "+partner_gender+".";  
		}
		if (motive == "loanshark") {
			motive_clause = "because "+murderer_name+" was in debt to the victim."; 
		}
		if (motive == "promotion") {
			motive_clause = "because they were in competition professionally.";
		}
		if (motive == "unfriended") {
			motive_clause = "because the victim unfriended "+murderer_name+" on Facebook.";
		}
		if (motive == "blackmail") {
			motive_clause = "because the victim knew "+murderer_name+"'s darkest secret.";
		}

		if (motive == "avenge_friend") {
			old_friends_description = "";
			motive_clause = murderer_name+" holds the victim responsible for a friend's death."; 
		}
		if (motive == "avenge_pet") {
			string species;
			int rand = Random.Range (0, 4);
			if (rand == 0) {
				species = "parrot";
			} else if (rand == 1) {
				species = "chihuahua";
			} else if (rand == 2) {
				species = "iguana";
			} else if (rand == 3) {
				species = "goldfish";
			} else {
				species = "rattlesnake";
			}
			string cause_of_death;
			rand = Random.Range (0,4);
			if (rand == 0) {
				cause_of_death = "starvation";
			} else if (rand == 1) {
				cause_of_death = "loneliness";
			} else if (rand == 2) {
				cause_of_death = "a broken heart";
			} else if (rand == 3) {
				cause_of_death = "boredom";
			} else {
				cause_of_death = "electrocution";
			}
			old_friends_description = "";
			motive_clause = "The victim was looking after "+murderer_name+"'s "+species+" when it " +
				"died of "+cause_of_death+".";  
		}
		old_friends_description += motive_clause;
		VerbalClue old_friends = new VerbalClue ("Old Friends", old_friends_description);

		VerbalClue old_enemies = new VerbalClue ("Old Enemies", "Rumour is that the victim had an unpleasant " +
			"history with "+murderer_name+".");

		VerbalClue last_seen_with = new VerbalClue ("Last Seen With", "I saw the victim alone with "+murderer_name+" just a few " +
			"minutes before their body was discovered.");

		string altercation_description = murderer_name+"and the victim had an altercation about ";
		motive_clause = ".";
		if (motive == "homewrecker") {
			string partner_gender;
			int binary = Random.Range (0, 1);
			if (binary == 0) {
				partner_gender = "wife";
			} else {
				partner_gender = "husband";
			}
			motive_clause = "the victim sleeping with their "+partner_gender+".";  
		}
		if (motive == "loanshark") {
			motive_clause = murderer_name+" being in debt to the victim."; 
		}
		if (motive == "promotion") {
			motive_clause = "them being in competition professionally.";
		}
		if (motive == "unfriended") {
			motive_clause = "the victim unfriending them on Facebook.";
		}
		if (motive == "blackmail") {
			motive_clause = "the victim having found out their darkest secret.";
		}
		if (motive == "avenge_friend") {
			
			motive_clause = "the death of a mutual friend."; 
		}
		if (motive == "avenge_pet") {
			motive_clause = murderer_name + "'s pet having died."; 
		}

		VerbalClue altercation = new VerbalClue ("An Altercation", altercation_description);

		int random = Random.Range (0, npcs.Count ());
		string character_name = npcs [random].getCharacterID ();
		VerbalClue changed_story = new VerbalClue ("Stories Have Changed", murderer_name+" told me they last saw the " +
			"victim before 8pm, but told "+character_name+" they spoke to the victim after 9pm.");

		verbal_clues = new VerbalClue[6] {
			old_friends,
			altercation,
			disposing_of_weapon,
			old_enemies,
			last_seen_with,
			changed_story
		};
	}

	// Creates two lists of clues, one containing 3 verbal clues (two relevant, one useless) and one containing 6 item clues (2 relevent, 4 useless).
	public void BuildCluePools(string motive, NonPlayerCharacter murderer, MurderWeapon weapon) {

		// one of the relevant item clues is the murder weapon, there is only ever one MurderWeapon item present in the game.
		item_clue_pool.Add (weapon);
		relevant_item_clues.Add (weapon);

		int pick_motive_clue = Random.Range (0, 1); // 'old friends' or 'altercation'
		if (motive == "homewrecker") {
			relevant_item_clues.Add (item_clues [pick_motive_clue]);
			verbal_clue_pool.Add (verbal_clues [pick_motive_clue]);
		} else if (motive == "loanshark") {
			verbal_clue_pool.Add (verbal_clues [pick_motive_clue]);
			relevant_verbal_clues.Add (verbal_clues [pick_motive_clue]);
		} else if (motive == "promotion") {
			verbal_clue_pool.Add (verbal_clues [pick_motive_clue]);
			relevant_verbal_clues.Add (verbal_clues [pick_motive_clue]);
		} else if (motive == "unfriended") {
			verbal_clue_pool.Add (verbal_clues [pick_motive_clue]);
			relevant_verbal_clues.Add (verbal_clues [pick_motive_clue]);
		} else if (motive == "blackmail") {
			verbal_clue_pool.Add (verbal_clues [pick_motive_clue]);
			relevant_verbal_clues.Add (verbal_clues [pick_motive_clue]);
		} else if (motive == "avenge_friend") {
			verbal_clue_pool.Add (verbal_clues [pick_motive_clue]);
			relevant_verbal_clues.Add (verbal_clues [pick_motive_clue]);
		} else if (motive == "avenge_pet") {
			verbal_clue_pool.Add (verbal_clues [pick_motive_clue]);
			relevant_verbal_clues.Add (verbal_clues [pick_motive_clue]);
		}

		int pick_weapon_clue = Random.Range (2, 5);
		verbal_clue_pool.Add (verbal_clues [pick_weapon_clue ]);
		relevant_verbal_clues.Add (verbal_clues [pick_weapon_clue ]);

		if (murderer.getCharacterID() == "Captain Bluebottle") {
			item_clue_pool.Add (item_clues [4]); // shattered glass
			relevant_item_clues.Add (item_clues [4]);
		} else if (murderer.getCharacterID() == "The Mime Twins") {
			item_clue_pool.Add (item_clues [0]); // beret
			relevant_item_clues.Add (item_clues [0]);
		} else if (murderer.getCharacterID() == "Sir Worchester") {
			item_clue_pool.Add (item_clues [2]); // gloves
			relevant_item_clues.Add (item_clues [2]);
		} else if (murderer.getCharacterID() == "Jesse Ranger") {
			item_clue_pool.Add (item_clues [8]); // tripwire
			relevant_item_clues.Add (item_clues [8]);
		} else if (murderer.getCharacterID() == "Celcius Maximus") {
			item_clue_pool.Add (item_clues [3]); // wine
			relevant_item_clues.Add (item_clues [3]);
		} else if (murderer.getCharacterID() == "Randolf the Deep Purple") {
			item_clue_pool.Add (item_clues [7]); // spellbook
			relevant_item_clues.Add (item_clues [7]);
		}

		// add the 6 irrelevant physical clues - NEW FOR ASSESSMENT 3  
		while (item_clue_pool.Count() < 8) {
			int index = Random.Range (0, item_clues.Count()-1);
			if (item_clue_pool.Contains (item_clues [index]) == false) {
				item_clue_pool.Add (item_clues [index]);
			}
		}
			
		// add 'red herring' verbal clue
		int weapon_index = Random.Range(0,weapons.Count()-1);
		string red_herring_weapon = weapons [weapon_index].getID ();
		while (red_herring_weapon == weapon.getID() ) {
			weapon_index = Random.Range(0,weapons.Count()-1);
			red_herring_weapon = weapons [weapon_index].getID ();
		}
		int npc_index = Random.Range(0,npcs.Count()-1);
		string red_herring_character = npcs [npc_index ].getCharacterID  ();
		while (red_herring_character == murderer.getCharacterID () ) {
			npc_index = Random.Range(0,npcs.Count()-1);
			red_herring_character = npcs [npc_index ].getCharacterID ();
		}


		// create and then choose one of two irrelevant verbal clues
		VerbalClue police_failure = new VerbalClue ("Lack of Evidence", "The police think the victim was killed " +
		                            "using a " + red_herring_weapon + ", but they can’t find evidence of one.");

		VerbalClue shifty_looking = new VerbalClue ("Looking Shifty", "I think I saw "+red_herring_character+" acting suspiciously.");

		VerbalClue[] red_herring_verbal_clues = new VerbalClue[2] { police_failure, shifty_looking };
		int herring_index = Random.Range (0,1);
		verbal_clue_pool.Add (red_herring_verbal_clues [herring_index]);
	}

	// distribute the 3 verbal clues among the NPC characters in NonPlayerCharacter[] 'npcs'
	public void DistributeVerbalClues(NonPlayerCharacter murderer) {
		int index = 0;
		List<NonPlayerCharacter> npcs_list = npcs.ToList (); 
		npcs_list.Remove (murderer);
		while (index < verbal_clue_pool.Count()) {
			NonPlayerCharacter character = npcs_list [Random.Range (0, npcs_list.Count ())];
			if (character.getVerbalClue() == null) {
				character.setVerbalClue (verbal_clue_pool [index]);
				verbal_clue_pool [index].SetOwner (character); 
				index++;
			}
		}
		npcs_list.Add (murderer);
		npcs = npcs_list.ToArray (); 
	}

	// Setters and Accessors
	public List<Item> getItemCluePool () {
		return item_clue_pool; 
	}

	public List<VerbalClue> getVerbalCluePool () {
		return verbal_clue_pool;
	}

	public MurderWeapon getWeapon () {
		return weapon;
	}

	public string getMotive() {
		return motive;
	}

	public NonPlayerCharacter[] getNPCs () {
		return npcs;
	}

	public List<Item> getRelevantItems () {
		return relevant_item_clues;
	}

	public List<VerbalClue> getRelevantVerbalClues () {
		return relevant_verbal_clues; 
	}

	public List<Clue> getRelevantClues () {
		List<Clue> relevant_clues = new List<Clue> ();

		for (int i = 0; (relevant_verbal_clues.Count) > i; i++) {
			relevant_clues.Add (relevant_verbal_clues [i]);
		}
		for (int i = 0; (relevant_item_clues.Count) > i; i++) {
			relevant_clues.Add (relevant_item_clues [i]);
		}

        // added to write in the console the clues needed to win the game so it can be tested easier 

        for (int i = 0; i < 3; i++)   
        {
            Debug.Log(relevant_clues[i].getID());
        }
        return relevant_clues;
	}
}

// Here is a precise URL of the executable on the team website
// http://wedunnit.me/webfiles/ass3/HomicideInTheHub-Win.zip

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameOver : MonoBehaviour {
	// CLASS ADDITION BY WEDUNNIT

	/// <summary>
	/// The text object that contains the player's score.
	/// </summary>
	public Text p1scoreText;
	/// <summary>
	/// The text object that contains the player's score.
	/// </summary>
	public Text p2scoreText;
	public Text spscoreText;
	/// <summary>
	/// The text object that the player writes their name to.
	/// </summary>
	public Text p1nameField;
	/// <summary>
	/// The text object that the player writes their name to.
	/// </summary>
	public Text p2nameField;
	public Text spnameField;
	/// <summary>
	/// The final score of the player.
	/// </summary>
	private int p1Score;
	/// <summary>
	/// The final score of the player.
	/// </summary>
	private int p2Score;
	public GameObject p1Hud;
	public GameObject p2Hud;

	/// <summary>
	/// Initialise this instance.
	/// </summary>
	void Start () {
		bool isMP = GameMaster.instance.isMultiplayer;
		if (isMP) {
			P2Start ();
		} else {
			P1Start ();
		}
	}
	void P1Start(){
		p1Hud.SetActive (true);
		GameMaster gMaster = FindObjectOfType<GameMaster> ();	// Find the current Game Master object
		p1Score = gMaster.GetScore (0);
		Text spText = spscoreText.GetComponent<Text> ();		// Get the text component of the text box...
		spText.text = "Your final score score: " + p1Score;
		Destroy(GameObject.Find("GlobalScripts")); // As we no longer need the GlobalScripts and NotebookCanvas objects...
		Destroy(GameObject.Find("NotebookCanvas")); // We can now get rid of them.
	}

	void P2Start(){
		p2Hud.SetActive (true);
		GameMaster gMaster = FindObjectOfType<GameMaster> ();	// Find the current Game Master object
		p1Score = gMaster.GetScore (0);							// Get the player's score
		p2Score = gMaster.GetScore(1);
		Text p1Text = p1scoreText.GetComponent<Text> ();		// Get the text component of the text box...
		p1Text.text = "Player 1's score: " + p1Score;
		Text p2Text = p2scoreText.GetComponent<Text> ();		// Get the text component of the text box...
		p2Text.text = "Player 2's score: " + p2Score;
		Destroy(GameObject.Find("GlobalScripts")); // As we no longer need the GlobalScripts and NotebookCanvas objects...
		Destroy(GameObject.Find("NotebookCanvas")); // We can now get rid of them.
	}

	public void P1CloseScreen(){
		string spInput = spnameField.text;			// Fetch the user's name from the field.
		if (spInput == "") {						// If it's blank, assign it a dummy value.
			spInput = "Some Unnamed Detective";
		}
		using (StreamWriter sw = new StreamWriter ("leaderboard.txt", true)) {
			sw.WriteLine (spInput);				// Write the name and score to leaderboard.txt.
			sw.WriteLine (p1Score.ToString ());
		}
		SceneManager.LoadScene ("Main Menu");		// Then return to main menu.
	}

	/// <summary>
	/// Closes the screen and returns to the main menu.
	/// </summary>
	public void P2CloseScreen(){
		string p1Input = p1nameField.text;			// Fetch the user's name from the field.
		if (p1Input == "") {						// If it's blank, assign it a dummy value.
			p1Input = "Some Unnamed Detective";
		}
		string p2Input = p2nameField.text;			// Fetch the user's name from the field.
		if (p2Input == "") {						// If it's blank, assign it a dummy value.
			p2Input = "Some Unnamed Detective";
		}
		using (StreamWriter sw = new StreamWriter ("leaderboard.txt", true)) {
			sw.WriteLine (p1Input);				// Write the name and score to leaderboard.txt.
			sw.WriteLine (p1Score.ToString ());
			sw.WriteLine (p2Input);
			sw.WriteLine (p2Score.ToString());
		}
		SceneManager.LoadScene ("Main Menu");		// Then return to main menu.
	}
}

using UnityEngine;
using System.Collections;

public class InputManager1 : MonoBehaviour {
	//Handles the input keys 'I' and 'M' pause menu and the map and notebook icons

	//States if the relative menu is open
	private bool isMapvisible = false;
	private bool isMenuvisible = false;
	private bool isNotebookvisible = false;

	public GameObject map;
	public GameObject pauseMenu; 
	private GameObject notebookMenu;
	public GameObject detective; 
	private PlayerMovement playerMovement;
	private bool mapIconPressed = false;
	private bool notebookIconPressed = false;

	void Start () {
		//Ensures the player can move at the start
		playerMovement = detective.GetComponent<PlayerMovement>();
		Time.timeScale = 1; 
		playerMovement.enabled = true;
		notebookMenu = GameObject.Find("Notebook Canvas").transform.GetChild(0).gameObject;
	}

	//Every frame
	void Update () {

		//Map
		if (!isMenuvisible && !isNotebookvisible) {					//If other menus are not open
			if (Input.GetKeyDown (KeyCode.M) || mapIconPressed) { 	//If M key pressed or UI icon pressed
				isMapvisible = !isMapvisible;						//Toggle visibiltiy
				if (isMapvisible) {	
					StopGame (map);									//Pause game if map is visble
				} else {
					ResumeGame (map);								//Resume game if map is not visible
				}
				mapIconPressed = false;								//Reset icon being pressed
			}
		}

		//Pause Menu
		if (!isMapvisible && !isNotebookvisible) {					//If other menus are not open
			if (Input.GetKeyDown (KeyCode.Escape)) {				//If ESC key pressed
				isMenuvisible = !isMenuvisible;						//Toggle visibiltiy
				if (isMenuvisible) {
					StopGame (pauseMenu);							//Pause game if is visble
				} else {
					ResumeGame (pauseMenu);							//Resume game if map is not visible
				}
			}

		}

		//Notebook
		if (!isMapvisible && !isMenuvisible) {								//If other menus are not open
			if (Input.GetKeyDown (KeyCode.I) || notebookIconPressed) {		//If ESC key pressed
				isNotebookvisible = !isNotebookvisible;						//Toggle visibiltiy
				if (isNotebookvisible) {
					NotebookManager.instance.UpdateNotebook ();				//Update notebook
					StopGame (notebookMenu);								//Pause game if is visble
				} else {
					ResumeGame (notebookMenu);								//Resume game if map is not visible
				}
				notebookIconPressed = false;
			}

		}

	}

	void StopGame(GameObject menu){
		//Stops ingame time and playermovement
		//Time.timeScale = 0;    // removed so that the timer wil work 
		playerMovement.enabled = false;
        
		menu.SetActive (true);
	}
		
	public void ResumeGame(GameObject menu){
		//Resumes ingame time and playermovement
		//Time.timeScale = 1;    // removed so that the timer wil work 
		playerMovement.enabled = true;
		menu.SetActive (false);
	}

	public void MapIconPressed(){
		//Called when map icon is pressed
		mapIconPressed = true;
	}

	public void NotebookIconPressed(){
		//Called when Notepad icon is pressed
		notebookIconPressed = true;
	}
}

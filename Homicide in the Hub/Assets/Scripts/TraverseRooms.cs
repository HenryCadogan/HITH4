using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class TraverseRooms : MonoBehaviour {
	//Used on the map to load the appropriate level.
	//Placed on the child objects of the map defining the hitboxes (Polygon Collider 2D) 

    public string level;
    public int buildIndex;

	//When the area on the map is clicked load the respective level
	void OnMouseDown() {
		GameMaster.instance.UseTurn ();				//ADDITION BY WEDUNNIT
        print("Trying to traverse to" + level);

	    print("Player has passed riddle:" + GameMaster.instance.HasPassedRiddle());

        if (buildIndex == GameMaster.instance.GetLockedRoomIndex() && GameMaster.instance.HasPassedRiddle()) //NEW FOR ASSESSMENT 3 check if underground lab is being loaded and check if key is found
		{
			GameMaster.instance.SetPreviousRoom(SceneManager.GetActiveScene().buildIndex);
			GameMaster.instance.LoadRoom (level);	//Updates current room for players in case of player switch when new room is loaded BY WEDUNNIT
			SceneManager.LoadScene(level);
        } else if(buildIndex == GameMaster.instance.GetLockedRoomIndex() && !GameMaster.instance.HasPassedRiddle())  // if riddle has not been passed load riddle room BY WEDUNNIT
        {
            GameMaster.instance.SetPreviousRoom(SceneManager.GetActiveScene().buildIndex);
			GameMaster.instance.SetLockedRoomName (level);
            SceneManager.LoadScene(16); //loads the riddle scene
        } else{
            //set the current room to the previous room and then go to new room
            GameMaster.instance.SetPreviousRoom(SceneManager.GetActiveScene().buildIndex);
			GameMaster.instance.LoadRoom (level);	//Updates current room for players in case of player switch when new room is loaded BY WEDUNNIT
			SceneManager.LoadScene(level);
        }
	}
}

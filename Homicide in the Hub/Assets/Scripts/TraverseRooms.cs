using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TraverseRooms : MonoBehaviour {
	//Used on the map to load the appropriate level.
	//Placed on the child objects of the map defining the hitboxes (Polygon Collider 2D) 

	public string level;	//Public to allow for changing in inspector.

	//When the area on the map is clicked load the respective level
	void OnMouseDown() {
        if (level == "Underground Lab" && GameMaster.instance.iskeyfound()) //NEW FOR ASSESSMENT 3 check if underground lab is being loaded and check if key is found 
        {
            SceneManager.LoadScene(level);  // if so load the underground lab 
        } else if(level == "Underground Lab" && !GameMaster.instance.iskeyfound())  // if no key has been found do not load the room
        {

        } else
        {
            SceneManager.LoadScene(level);
        }

 
		
	}
}

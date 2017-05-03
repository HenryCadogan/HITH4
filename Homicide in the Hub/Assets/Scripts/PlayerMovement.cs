using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	//Used to move the detective around the scene
	//Player van only move within the boundaries set in the array boundaries

	//__Variables__
	//Public to allow for malipulation at each scene
	public float speed = 2.0f;
	public float[] boundaries = new float[4]; 	//First element is min of x, second max of x, third min of y, fourth max of y.
	private Vector2 targetPosition;				//Where the player clicks on the screen
	private SpriteRenderer spriteRenderer;

	void Start () {
		targetPosition = transform.position;	//Initialises the target position to the current position to stop moving as soon as enterered scene
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}

	void Update () {
		if (Input.GetMouseButtonDown(0)) {		//When left mouse button is pressed
			targetPosition= Camera.main.ScreenToWorldPoint(Input.mousePosition);		
			spriteRenderer.flipX = (targetPosition.x < transform.position.x); //Flips the detective sprite based on where the player clicks. 
		}
		//Boundaries for movement//REMOVED BY WEDUNNIT AS WE DONT THINK CHARACTER MOVEMENT IS NECCESSARY
		//if ((targetPosition.x > boundaries [0]) && (targetPosition.x < boundaries [1]) && (targetPosition.y > boundaries [2]) && (targetPosition.y < boundaries [3])) {//REMOVED BY WEDUNNIT AS WE DONT THINK CHARACTER MOVEMENT IS NECCESSARY
		//	transform.position = Vector3.MoveTowards (transform.position, targetPosition, speed * Time.deltaTime);//REMOVED BY WEDUNNIT AS WE DONT THINK CHARACTER MOVEMENT IS NECCESSARY
		//} 
	}    
}
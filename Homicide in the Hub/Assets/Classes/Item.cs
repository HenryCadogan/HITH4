using UnityEngine;
using System.Collections;

public class Item : Clue {
	//

	private GameObject prefab;
	private Sprite sprite;
    

	public Item(GameObject prefab, string clueID, string description, Sprite sprite) : base(clueID, description) {
		this.prefab = prefab;
		this.sprite = sprite;
        
	}
		
	public Sprite GetSprite(){
		return this.sprite;
	}

	public GameObject GetPrefab(){
		return this.prefab;
	}
  
		
}

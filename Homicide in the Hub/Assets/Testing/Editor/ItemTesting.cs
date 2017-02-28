using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class ItemTesting {

	[Test]
	public void GetItemSpriteTest()
	{
		var itemSprite = new Sprite ();

		//Arrange
		var item = new Item(null, null, null, itemSprite);

		//Assert
		//The object has a new name
		Assert.AreSame(itemSprite, item.GetSprite ());
	}

	[Test]
	public void GetItemPrefabTest()
	{
		var itemPrefab = new GameObject();

		//Arrange
		var item = new Item(itemPrefab, null, null, null);

		//Assert
		//The object has a new name
		Assert.AreSame(itemPrefab, item.GetPrefab ());
	}
}

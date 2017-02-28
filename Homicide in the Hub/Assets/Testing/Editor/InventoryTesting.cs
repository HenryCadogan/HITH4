using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class InventoryTesting {

	[Test]
	public void AddItemToInventoryTest()
	{
		//Arrange
		var inventory = new Inventory();
		var item = new Item(null,null,null,null);

		//Act
		//To add item to inventory
		inventory.AddItemToInventory (item);

		//Assert
		//The inventory contains the the item
		Assert.IsTrue (inventory.GetInventory ().Contains (item));
	}

	[Test]
	public void ResetInventoryTest()
	{
		//Arrange
		var inventory = new Inventory();
		var item = new Item(null,null,null,null);

		//Act
		//To add item to inventory
		inventory.AddItemToInventory (item);
		inventory.Reset ();

		//Assert
		//The inventory is empty
		Assert.IsEmpty (inventory.GetInventory ());
	}

	[Test]
	public void GetLengthOfInventoryTest()
	{
		//Arrange
		var inventory = new Inventory();
		var item = new Item(null,null,null,null);

		//Act
		//To add item to inventory
		inventory.AddItemToInventory (item);

		//Assert
		//The inventory is has length one
		Assert.AreEqual (inventory.GetListLength (),1);
	}

}

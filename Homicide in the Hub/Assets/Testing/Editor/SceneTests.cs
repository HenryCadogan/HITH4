using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class SceneTests {

	[Test]
	public void GetSceneNameTest()
	{
		//Arrange
		var sceneName = "My Scene Name";
		var scene = new Scene(sceneName);

		//Assert
		Assert.AreSame (scene.GetName (),sceneName);
	}

	[Test]
	public void AddNPCToArrayTest()
	{
		//Arrange
		var scene = new Scene(null);
		var npc = new NonPlayerCharacter(null,null,null,null,null,null);

		//Act
		scene.AddNPCToArray (npc);

		//Assert
		Assert.IsTrue (scene.GetCharacters ().Contains (npc));
	}

	[Test]
	public void AddItemToArrayTest()
	{
		//Arrange
		var scene = new Scene(null);
		var item = new Item(null,null,null,null);

		//Act
		scene.AddItemToArray (item);

		//Assert
		Assert.IsTrue (scene.GetItems ().Contains (item));
	}

	[Test]
	public void ResetTest()
	{
		//Arrange
		var scene = new Scene(null);
		var item = new Item(null,null,null,null);
		var npc = new NonPlayerCharacter(null,null,null,null,null,null);

		//Act
		scene.AddItemToArray (item);
		scene.AddNPCToArray (npc);
		scene.ResetScene ();

		//Assert
		Assert.IsEmpty (scene.GetCharacters ());
		Assert.IsEmpty (scene.GetItems ());
	}


}

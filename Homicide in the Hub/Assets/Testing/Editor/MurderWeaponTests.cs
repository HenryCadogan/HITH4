using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class MurderWeaponTests {

	[Test]
	public void GetDescriptionTest()
	{
		//Arrange
		var description = "My Description";
		var murderWeapon = new MurderWeapon(null,null,null,null,description);


		//Assert
		//The object has a new name
		Assert.AreSame(description, murderWeapon.getSteveDescription ());
	}
}

using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using UnityEngine.UI;

public class CharacterClassForTesting : Character {

	public CharacterClassForTesting (string characterID, Sprite sprite, string nickname) :  base(characterID, sprite, nickname){

	}
}

public class CharacterTests {

	[Test]
	public void GetCharacterNameTest()
	{
		//Arrange
		var characterName = "My Character";
		var character = new CharacterClassForTesting (characterName,null, null);

		//Assert
		//Can get correct name
		Assert.AreEqual(characterName, character.getCharacterID ());
	}

	[Test]
	public void GetCharacterSpriteTest()
	{
		//Arrange
		Sprite characterSprite = new Sprite ();
		var character = new CharacterClassForTesting(null,characterSprite, null);

		//Assert
		//Can get correct sprite
		Assert.AreEqual(character.getSprite (), characterSprite);
	}

	[Test]
	public void GetCharacterNicknameTest()
	{
		//Arrange
		var characterNickname = "My Nickname";
		var character = new CharacterClassForTesting(null,null, characterNickname);

		//Assert
		//Can get correct nickname
		Assert.AreEqual(characterNickname, character.getNickname ());
	}

}

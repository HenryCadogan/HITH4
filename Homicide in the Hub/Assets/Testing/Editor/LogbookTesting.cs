using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class LogbookTesting {

	[Test]
	public void AddVerbalClueToLogbookTest()
	{
		//Arrange
		var logbook = new Logbook();
		var verbalClue = new VerbalClue(null,null);

		//Act
		//To add verbalClue to logbook
		logbook.AddVerbalClueToLogbook (verbalClue);

		//Assert
		//The logbook contains the the verbalClue
		Assert.IsTrue (logbook.GetLogbook ().Contains (verbalClue));
	}

	[Test]
	public void ResetLogbookTest()
	{
		//Arrange
		var logbook = new Logbook();
		var verbalClue = new VerbalClue(null,null);

		//Act
		//To add verbalClue to logbook
		logbook.AddVerbalClueToLogbook (verbalClue);
		logbook.Reset ();

		//Assert
		//The logbook is empty
		Assert.IsEmpty (logbook.GetLogbook ());
	}

	[Test]
	public void GetLengthOfLogbookTest()
	{
		//Arrange
		var logbook = new Logbook();
		var verbalClue = new VerbalClue(null,null);

		//Act
		//To add verbalClue to logbook
		logbook.AddVerbalClueToLogbook (verbalClue);

		//Assert
		//The logbook is has length one
		Assert.AreEqual (logbook.GetListLength (),1);
	}
}

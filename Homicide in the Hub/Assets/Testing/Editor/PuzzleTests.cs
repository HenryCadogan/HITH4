using System;
using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class PuzzleTests{

	private Puzzle puzzle;


	//Set up a new instance of the puzzle for each test
	[TestFixtureSetUp]
	public void PuzzleTestSetup(){
		puzzle = new Puzzle();
	}

	[Test]
	public void TestWrongAnswers(){
		//check the list is not empty
		Assert.AreNotEqual(0,puzzle.GetWrongAnswers().Count);

		//check that each answer itself is not empty
		foreach (String answer in puzzle.GetWrongAnswers()){
			Assert.IsNotEmpty(answer);
		}
	}

	//test that the Riddle is not an empty string
	[Test]
	public void TestRiddleText(){
		Assert.IsNotNull(puzzle.GetRiddleText());
	}

	//test that the correct answer is also loaded
	[Test]
	public void TestCorrectAnswer(){
		Assert.IsNotNull(puzzle.GetCorrectAnswer());
	}
}

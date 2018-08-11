using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;


public class RandomGPATileTest {

	[Test]
	public void TestPlayerRandomGPATileFinalLand()
	{
		GameObject playerObj = new GameObject("Player");
		playerObj.AddComponent<BoxCollider>();
		Player p = playerObj.AddComponent<Player>();

		p.moves_remaining = 1;
		p.gpa = 2.0f;

		GameObject tile1  = new GameObject();
		GameObject tile2 = new GameObject();
		GameTile currTile = tile1.AddComponent<NormalTile>();
		RandomGPAMod nextTile = tile2.AddComponent<RandomGPAMod>();

        currTile.nextTile = nextTile.transform;

		p.UpdateCurrentTile(currTile); //Set the players current tile

		p.MoveNext(); //Tell the player to move

		nextTile.OnFinalLand(p); //Call next tile land

		Assert.That(System.Math.Abs(p.GetGPA() - (2.0 + nextTile.randomGPAModPoints)) < 0.001); //Ensure GPA is increased by the random amount
	}
}

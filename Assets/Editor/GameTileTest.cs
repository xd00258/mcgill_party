using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class GameTileTest {

    [Test]
	public void TestPlayerNormalTileFinalLand()
    {
        GameObject playerObj = new GameObject("Player");
        playerObj.AddComponent<BoxCollider>();
        Player p = playerObj.AddComponent<Player>();

        p.moves_remaining = 1;
        p.SetGPA(2.0f);

        //Create current and next tile
        GameObject tile1 = new GameObject();
        GameObject tile2 = new GameObject();
        GameTile currTile = tile1.AddComponent<NormalTile>();
        GameTile nextTile = tile2.AddComponent<NormalTile>();

        currTile.nextTile = nextTile.transform;

        p.UpdateCurrentTile(currTile); //Set the players current tile

        p.MoveNext(); //Tell the player to move

        nextTile.OnFinalLand(p); //Call next tile land

        Assert.AreEqual(p.GetGPA(), 2.0f); //Ensure GPA is unaffected
    }

    [Test]
    public void TestPlayerRedTileFinalLand()
    {
        GameObject playerObj = new GameObject("Player");
        playerObj.AddComponent<BoxCollider>();
        Player p = playerObj.AddComponent<Player>();

        p.moves_remaining = 1;
        p.SetGPA(2.0f);

        //Create current and next tile
        GameObject tile1 = new GameObject();
        GameObject tile2 = new GameObject();
        GameTile currTile = tile1.AddComponent<NormalTile>();
        GPAModTile nextTile = tile2.AddComponent<GPAModTile>();

        nextTile.color = GPAModTile.Color.RED;

        currTile.nextTile = nextTile.transform;

        p.UpdateCurrentTile(currTile); //Set the players current tile

        p.MoveNext(); //Tell the player to move

        nextTile.OnFinalLand(p); //Call next tile land

        Assert.AreEqual(p.GetGPA(), 1.7f); //Ensure GPA is the decreased by 0.3
    }

    [Test]
    public void TestPlayerGreenTileFinalLand()
    {
        GameObject playerObj = new GameObject("Player");
        playerObj.AddComponent<BoxCollider>();
        Player p = playerObj.AddComponent<Player>();

        p.moves_remaining = 1;
        p.SetGPA(2.0f);

        //Create current and next tile
        GameObject tile1 = new GameObject();
        GameObject tile2 = new GameObject();
        GameTile currTile = tile1.AddComponent<NormalTile>();
        GPAModTile nextTile = tile2.AddComponent<GPAModTile>();

        nextTile.color = GPAModTile.Color.GREEN;
        //Create current and next tile
        
        currTile.nextTile = nextTile.transform;

        p.UpdateCurrentTile(currTile); //Set the players current tile

        p.MoveNext(); //Tell the player to move

        nextTile.OnFinalLand(p); //Call next tile land

        Assert.AreEqual(p.GetGPA(), 2.3f); //Ensure GPA is increased by 0.3
    }
}

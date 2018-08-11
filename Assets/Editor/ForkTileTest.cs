using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class ForkTileTest
{

    [Test]
    public void TestPlayerForkTileFinalLand()
    {
        GameObject tile1  = new GameObject();
        GameObject tile2 = new GameObject();
        GameTile currTile = tile1.AddComponent<NormalTile>();
        ForkTile nextTile = tile2.AddComponent<ForkTile>();

        currTile.nextTile = nextTile.gameObject.transform;

        GameObject playerObj = new GameObject("Player");
        playerObj.AddComponent<BoxCollider>();
        Player p = playerObj.AddComponent<Player>();

        p.moves_remaining = 1;
        p.gpa = 2.0f;

        p.UpdateCurrentTile(currTile); //Set the players current tile
        nextTile.OnFinalLand(p);

        Assert.AreEqual(p.GetGPA(), 2.0f); //Ensure GPA is unaffected
		Assert.AreEqual(p.GetRemainingMoves(), 1); //Ensure remaining moves is unaffected

        GameObject.DestroyImmediate(tile1);
        GameObject.DestroyImmediate(tile2);
        GameObject.DestroyImmediate(playerObj);

    }
}
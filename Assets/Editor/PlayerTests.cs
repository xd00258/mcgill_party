using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PlayerTests {

    [Test]
    public void TestPlayerInit()
    {
        GameObject tile1 = new GameObject();
        GameObject tile2 = new GameObject();
        GameTile t1 = tile1.AddComponent<NormalTile>();
        GameTile t2 = tile2.AddComponent<NormalTile>();

        GameObject playerObj = new GameObject("Player");
        playerObj.AddComponent<BoxCollider>();
        Player p = playerObj.AddComponent<Player>();

        t1.nextTile = t2.transform; //Set tile order


        p.InitPlayer(t1); //Init player will set the target tile the current tile's next tile

        Assert.AreEqual(t2, p.target_tile); //Ensure that target tile is correct
    }

    [Test]
    public void TestPlayerInitMove()
    {
        GameObject playerObj = new GameObject("Player");
        playerObj.AddComponent<BoxCollider>();
        Player p = playerObj.AddComponent<Player>();

        p.InitMovePlayer(10);

        Assert.AreEqual(10, p.moves_remaining);
    }

    [Test]
    public void TestPlayerSetGPAValid()
    {
        GameObject playerObj = new GameObject("Player");
        playerObj.AddComponent<BoxCollider>();
        Player p = playerObj.AddComponent<Player>();

        p.SetGPA(3.0f);

        Assert.AreEqual(3.0f, p.GetGPA());
    }

    [Test]
    public void TestPlayerSetGPAInvalidLow()
    {
        GameObject playerObj = new GameObject("Player");
        playerObj.AddComponent<BoxCollider>();
        Player p = playerObj.AddComponent<Player>();

        p.SetGPA(-1.0f);

        Assert.AreEqual(0.0f, p.GetGPA()); //Should be initial value of 0
    }

    [Test]
    public void TestPlayerSetGPAInvalidHigh()
    {
        GameObject playerObj = new GameObject("Player");
        playerObj.AddComponent<BoxCollider>();
        Player p = playerObj.AddComponent<Player>();

        p.SetGPA(10.0f);

        Assert.AreEqual(0.0f, p.GetGPA()); //Should be initial value of 0
    }

    [Test]
    public void TestPlayerUpdateGPAValid()
    {
        GameObject playerObj = new GameObject("Player");
        playerObj.AddComponent<BoxCollider>();
        Player p = playerObj.AddComponent<Player>();

        p.SetGPA(2.0f);

        p.UpdateGPA(1.0f); //Update will add parameter to the current gpa

        Assert.AreEqual(3.0f, p.GetGPA());
    }

    [Test]
    public void TestPlayerUpdateGPAInvalidLow()
    {
        GameObject playerObj = new GameObject("Player");
        playerObj.AddComponent<BoxCollider>();
        Player p = playerObj.AddComponent<Player>();

        p.SetGPA(2.0f);

        p.UpdateGPA(-3.0f); //Will clamp the value at minimum (0)

        Assert.AreEqual(0.0f, p.GetGPA());
    }

    [Test]
    public void TestPlayerUpdateGPAInvalidHigh()
    {
        GameObject playerObj = new GameObject("Player");
        playerObj.AddComponent<BoxCollider>();
        Player p = playerObj.AddComponent<Player>();

        p.SetGPA(2.0f);

        p.UpdateGPA(3.0f); //Will clamp the value at maximum (4)

        Assert.AreEqual(4.0f, p.GetGPA());
    }
}

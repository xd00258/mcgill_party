using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BoardGenTest {

    private BoardManager SetupBoard()
    {
        //First start by removing everything in the scene
        CleanupBoard();

        // Load a prefab (by giving it the path to an existing prefab).
        GameObject managers = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Board/Managers.prefab");
        GameObject tiles = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Board/GameTiles.prefab");

        //Instantiate our tiles and managers into the scene
        tiles = GameObject.Instantiate(tiles, Vector3.zero, Quaternion.identity);
        managers = GameObject.Instantiate(managers, Vector3.zero, Quaternion.identity);

        BoardManager script = managers.gameObject.GetComponentInChildren<BoardManager>();
        // Assert that the script exists on our prefab so we don't stumble upon this problem in the future.
        Assert.IsTrue(script != null, "Board Game Manager must be set on prefab.");
        return script;
    }

    private void CleanupBoard()
    {
        Transform[] obj = GameObject.FindObjectsOfType<Transform>();
        foreach(Transform t in obj){
            if(t!=null){
                GameObject.DestroyImmediate(t.gameObject);
            }
        }
    }

    [Test]
    [Timeout(180000)]
    public void TestGPARedTiles()
    {
        //Set number of red tiles to 5
        EditorData.redTiles = 5;
        BoardManager script = SetupBoard();
        script.Start();

        //Check our number of tiles
        GPAModTile[] modTiles = GameObject.FindObjectsOfType<GPAModTile>();

        int numRed = 0;
        foreach (GPAModTile g in modTiles)
        {
            if (g.color == GPAModTile.Color.RED)
            {
                numRed++;
            }
        }

        Assert.AreEqual(numRed, 5);

        //Redo for 0 green tiles
        EditorData.redTiles = 0;
        script.Start();
        //Check our number of tiles
        modTiles = GameObject.FindObjectsOfType<GPAModTile>();

        numRed = 0;
        foreach (GPAModTile g in modTiles)
        {
            if (g.color == GPAModTile.Color.RED)
            {
                numRed++;
            }
        }

        Assert.AreEqual(numRed, 0);

        CleanupBoard();
     }

    [Test]
    [Timeout(180000)]
    public void TestGPAGreenTiles()
    {
        //Set number of red tiles to 5
        EditorData.greenTiles = 5;
        BoardManager script = SetupBoard();
        script.Start();

        //Check our number of tiles
        GPAModTile[] modTiles = GameObject.FindObjectsOfType<GPAModTile>();

        int numGreen = 0;
        foreach (GPAModTile g in modTiles)
        {
            if (g.color == GPAModTile.Color.GREEN)
            {
                numGreen++;
            }
        }

        Assert.AreEqual(numGreen, 5);

        //Redo for 0 green tiles
        EditorData.greenTiles = 0;
        script.Start();
        //Check our number of tiles
        modTiles = GameObject.FindObjectsOfType<GPAModTile>();

        numGreen = 0;
        foreach (GPAModTile g in modTiles)
        {
            if (g.color == GPAModTile.Color.GREEN)
            {
                numGreen++;
            }
        }

        Assert.AreEqual(numGreen, 0);

        CleanupBoard();
    }

    [Test]
    [Timeout(180000)]
    public void TestDuelTiles()
    {
        //Set number of red tiles to 5
        BoardManager script = SetupBoard();

        //Check our number of tiles
        DuelTile[] duelTiles = GameObject.FindObjectsOfType<DuelTile>();

        Assert.IsTrue(duelTiles.Length>0,"Must use board prefab with duel tiles present for test");

        EditorData.duelTiles = true;
        script.Start();
        duelTiles = GameObject.FindObjectsOfType<DuelTile>();
        Assert.IsTrue(duelTiles.Length > 0);

        EditorData.duelTiles = false;
        script.Start();
        duelTiles = GameObject.FindObjectsOfType<DuelTile>();
        Assert.IsTrue(duelTiles.Length == 0);


        CleanupBoard();
    }


    [Test]
    [Timeout(180000)]
    public void TestTeleportTile()
    {
        //Set number of red tiles to 5
        BoardManager script = SetupBoard();

        //Check our number of tiles
        TeleportationTile[] teleTile = GameObject.FindObjectsOfType<TeleportationTile>();

        Assert.IsTrue(teleTile.Length > 0, "Must use board prefab with teleportation tiles present for test");

        EditorData.teleportTiles = true;
        script.Start();
        teleTile = GameObject.FindObjectsOfType<TeleportationTile>();
        Assert.IsTrue(teleTile.Length > 0);

        EditorData.teleportTiles = false;
        script.Start();
        teleTile = GameObject.FindObjectsOfType<TeleportationTile>();
        Assert.IsTrue(teleTile.Length == 0);


        CleanupBoard();
    }


    [Test]
    [Timeout(180000)]
    public void TestRandomTile()
    {
        //Set number of red tiles to 5
        BoardManager script = SetupBoard();

        //Check our number of tiles
        RandomGPAMod[] rngTile = GameObject.FindObjectsOfType<RandomGPAMod>();

        Assert.IsTrue(rngTile.Length > 0, "Must use board prefab with teleportation tiles present for test");

        EditorData.gpaTiles = true;
        script.Start();
        rngTile = GameObject.FindObjectsOfType<RandomGPAMod>();
        Assert.IsTrue(rngTile.Length > 0);

        EditorData.gpaTiles = false;
        script.Start();
        rngTile = GameObject.FindObjectsOfType<RandomGPAMod>();
        Assert.IsTrue(rngTile.Length == 0);


        CleanupBoard();
    }

}

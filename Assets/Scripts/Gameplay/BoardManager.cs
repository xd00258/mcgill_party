using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

    private int numGreenTiles;
    private int numRedTiles;
    private bool rngTilesOn=false;
    private bool teleportTilesOn=false;
    private bool duelTilesOn=false;

    public GPAModTile greenTilePrefab;
    public GPAModTile redTilePrefab;
    public NormalTile normalTilePrefab;

	// Use this for initialization
	public void Start () {
        numGreenTiles = EditorData.greenTiles;
        numRedTiles = EditorData.redTiles;
        duelTilesOn = EditorData.duelTiles;
        teleportTilesOn = EditorData.teleportTiles;
        rngTilesOn = EditorData.gpaTiles;
        Populate();
        GameManager.Instance.enabled = true;
	}

    private void Populate()
    {
        int greenLeft = numGreenTiles;
        int redLeft = numRedTiles;

        GameTile[] tiles = FindObjectsOfType<GameTile>();

        List<GPAModTile> greenTiles = new List<GPAModTile>();
        List<GPAModTile> redTiles = new List<GPAModTile>();

        //First count how many tiles already exist of type a given type, and disable any special tiles
        foreach (GameTile t in tiles)
        {
            System.Type type = t.GetType();

            if (type == typeof(GPAModTile))
            {
                GPAModTile gpaTile = ((GPAModTile)t);
                //Red
                if(gpaTile.color == GPAModTile.Color.RED)
                {
                    redTiles.Add(gpaTile);
                    redLeft--;
                }
                //Green
                else
                {
                    greenTiles.Add(gpaTile);
                    greenLeft--;
                }
            }
            //Disable special tiles
            else if(!rngTilesOn && type == typeof(RandomGPAMod))
            {
                ReplaceTileWith(t, normalTilePrefab);
            }
            else if(!duelTilesOn && type == typeof(DuelTile))
            {
                ReplaceTileWith(t, normalTilePrefab);

            }
            else if(!teleportTilesOn && type == typeof(TeleportationTile))
            {
                ReplaceTileWith(t, normalTilePrefab);
            }
        }

        //Tiles we can modify
        NormalTile[] regularTiles = FindObjectsOfType<NormalTile>();

        while(greenLeft>0)
        {
            int choice = Random.Range(0, regularTiles.Length);
            if(regularTiles[choice]!=null){
                ReplaceTileWith(regularTiles[choice], greenTilePrefab);
                greenLeft--;

            }
        }

        while(greenLeft<0)
        {
            int choice = Random.Range(0, greenTiles.Count);
            if (greenTiles[choice] != null)
            {
                ReplaceTileWith(greenTiles[choice], normalTilePrefab);
                greenLeft++;
            }
        }

        while(redLeft>0)
        {
            int choice = Random.Range(0, regularTiles.Length);
            if (regularTiles[choice] != null)
            {
                ReplaceTileWith(regularTiles[choice], redTilePrefab);
                redLeft--;

            }
        }

        while (redLeft < 0)
        {
            int choice = Random.Range(0, redTiles.Count);
            if (redTiles[choice] != null)
            {
                ReplaceTileWith(redTiles[choice], normalTilePrefab);
                redLeft++;
            }
        }

    }

    private void ReplaceTileWith(GameTile original, GameTile prefabNew)
    {
   

        GameTile g = Instantiate(prefabNew) as GameTile;
        g.transform.position = original.transform.position;
        g.transform.rotation = original.transform.rotation;
        g.nextTile = original.nextTile.transform;
        foreach (Transform t in original.transform)
        {
            DestroyImmediate(t.gameObject);
        }
        g.transform.SetParent(original.transform);

        DestroyImmediate(original.GetComponent<MeshRenderer>());
        DestroyImmediate(original);

    }

}

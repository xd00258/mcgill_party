using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkTile : GameTile 
{

    public Transform[] forkTiles;
    private bool fork_prompt = false;
    private Player p;
    private int selectedTile = 0;
    private int previousSelectedTile = -1;
    public Transform haloPrefab;
    Transform halo;


	// Use this for initialization
	void Start () 
    {
        halo = Instantiate(haloPrefab) as Transform;
        halo.gameObject.SetActive(false);
        lore_text = "The new course schedules are out! It's time to make some decisions. Which path do you take?";

    }

	
	// Update is called once per frame
	void Update () 
    {
        if (fork_prompt)
        {
            SelectTile();
            previousSelectedTile = selectedTile;
        }
        //select path
        if(Input.GetButtonDown("KeyPad1") && fork_prompt){
            selectedTile++;
            selectedTile %= forkTiles.Length;
        }
        //choose path
        if(Input.GetButtonDown("KeyPad2") && fork_prompt){
            halo.gameObject.SetActive(false);
            nextTile = forkTiles[selectedTile].transform;
            p.MoveNext();
            fork_prompt = false;
        }
	}

    private void OnGUI()
    {
        if (fork_prompt)
        {
            GUIStyle centerStyle = new GUIStyle("box");
            centerStyle.alignment = TextAnchor.MiddleCenter;
            var text = lore_text;
            var textSize = GUI.skin.label.CalcSize(new GUIContent(text));
            GUI.Label(new Rect((Screen.width - textSize.x) / 2, 5 * Screen.height / 6, textSize.x + 15, textSize.y + 6), text, centerStyle);
        }
    }

    //override OnTransit
    //method for increasing
    public override void OnTransit(Player player)
    {
        p = player;
        fork_prompt = true;
    }

    //update current selected tile
    public void SelectTile()
    {
        halo.gameObject.SetActive(true);
        Vector3 position = forkTiles[selectedTile].transform.position;
        halo.position = position;
    }

    public override void OnFinalLand(Player player)
    {
        //Do nothing for fork tile, DOESN'T EXIST
    }
}

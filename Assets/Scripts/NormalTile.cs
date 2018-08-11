using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTile : GameTile {
    // Use this for initialization

    void Start () {
        lore_text = "You plan to study, finish all your assignments and get your life together during reading week.\nMonday comes along; nothing has changed.";
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnFinalLand(Player player) {
        //Do nothing for normal tile
    }
}

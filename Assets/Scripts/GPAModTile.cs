using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPAModTile : GameTile {
    

    public enum Color { RED, GREEN }
    public Color color;
    public float gpaModPoints = 0.3f;

	// Use this for initialization
	void Start () {
		switch(color)
        {
            case Color.RED:
                lore_text = "You skip a class to work on an assignment for that class\nand recursively fall more behind in the course material.";
                break;
            case Color.GREEN:
                lore_text = "A fellow student buys 3 samosas for $2 but can't finish the last one and gives you a free samosa.\nYou are so overwhelmed with joy that you finish an assignment in one sitting and develop excellent hygiene de vie.";
                break;

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnFinalLand(Player player) {
        switch(color)
        {
            case Color.RED:
                player.UpdateGPA(gpaModPoints * -1);
                break;
            case Color.GREEN:
                player.UpdateGPA(gpaModPoints);
                break;
        }
    }
}

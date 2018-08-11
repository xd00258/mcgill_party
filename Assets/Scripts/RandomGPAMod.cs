using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGPAMod : GameTile {

	public float randomGPAModPoints;
	public Material[] material;
	Renderer rend;
	private bool isRed = true;
	public float interpolationPeriod = 0.25f;
	private float time = 0.0f;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
		rend.enabled = true;
		rend.sharedMaterial = material [0];
    }

	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;

		//change tile color every interval of time
		if (time >= interpolationPeriod) {
			time = 0.0f;

			if (isRed) {
				rend.sharedMaterial = material [1];
				isRed = false;
			} else {
				rend.sharedMaterial = material [0];
				isRed = true;
			}
		}

	}

	public override void OnFinalLand(Player player) {
		randomGPAModPoints = (float) 0.1 * Random.Range(-10,10);
		//TODO: handle lore depending on value of randomGPAModPoints
        if (randomGPAModPoints > 0)
        {
            lore_text = "A fellow student buys 3 samosas for $2 but can't finish the last one and gives you a free samosa.\nYou are so overwhelmed with joy that you finish an assignment in one sitting and develop excellent hygiene de vie.";
        }
        else 
        {
            lore_text = "You skip a class to work on an assignment for that class\nand recursively fall more behind in the course material.";
        }
		//update
		player.UpdateGPA(randomGPAModPoints);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationTile : GameTile {

    public Transform teleportTile;
	public bool isDestination;

	// Use this for initialization
	void Start () {
        lore_text = "How did you end up here? Did you skip ahead or did you fall behind?\nYou e-mail your faculty for answers.";
    }

	// Update is called once per frame
	void Update () {
		// make object rotate constantly
        transform.Rotate(Vector3.up, 36f * Time.deltaTime);
	}
		
	public override void OnFinalLand(Player player) {
		if (!isDestination) {
            player.UpdateDestinationTile (teleportTile.GetComponentInChildren<GameTile>());
			StartCoroutine(AlphaFade(player));
		}
	}

	/**
     * makes the player fade out, teleport and fade back in.
     * 
     * @param player player to move
     */
	private IEnumerator AlphaFade (Player player)
	{
		// Alpha start value.
		float alpha = 1.0f;
		float fadeSpeed = 1.0f;

		// fade out
		while (alpha > 0.0f) {
			// reduce alpha by fadeSpeed amount.
			alpha -= fadeSpeed * Time.deltaTime;
			Color color = player.GetComponentInChildren<MeshRenderer> ().material.color;

			// create a new color using original color RGB values and new alpha
			player.GetComponentInChildren<MeshRenderer> ().material.color = new Color (color.r, color.g, color.b, alpha);

			yield return null;
		}

		// teleport
		player.transform.position = player.target_tile.transform.position;

		// fade in
		while (alpha < 1.0f) {
			// increase alpha by fadeSpeed amount.
			alpha += fadeSpeed * Time.deltaTime;
			Color color = player.GetComponentInChildren<MeshRenderer> ().material.color;

			// create a new color using original color RGB values and new alpha
			player.GetComponentInChildren<MeshRenderer> ().material.color = new Color (color.r, color.g, color.b, alpha);
            player.just_teleported = true;

			yield return null;
		}

		player.UpdateCurrentTile (teleportTile.GetComponentInChildren<GameTile> ()); 
	}




}


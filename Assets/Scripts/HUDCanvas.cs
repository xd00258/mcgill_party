using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDCanvas : MonoBehaviour {

	private float maxGpa;
	private float minGpa;
	private float startGpa;
	private float player1Gpa;
	private float player2Gpa;
	private int turn;

	public Slider gpa1Slider;
	public Slider gpa2Slider;

	public Text player1Text;
	public Text player2Text;
	public Text gpa1Text;
	public Text gpa2Text;

	// Use this for initialization
	void Start () {
		
		// set initial GPA values
		maxGpa = 4.0f;
		minGpa = 0.0f;
		startGpa = 2.0f;

		// update display
		UpdateStatistics ();
		UpdateTurn ();
		UpdateSliders ();
		UpdateGpaTexts ();
	}

	/**
 	 * Calculates the value of the GPA slider
 	 * @param playerGpa the GPA of the player
 	 * @return the slider value corresponding to the GPA
 	 */
	private float CalculateGpa (float playerGpa) {
		return (playerGpa-minGpa)/(maxGpa-minGpa);
	}

	/**
 	 * Gets game statistics updates from game manager
 	 */
	private void UpdateStatistics () {
		player1Gpa = GameManager.Instance.Player1GPA;
		player2Gpa = GameManager.Instance.Player2GPA;
		turn = GameManager.Instance.Turn;
	}

	/**
 	 * Updates the GPA sliders
 	 */
	private void UpdateSliders() {
		gpa1Slider.value = CalculateGpa(player1Gpa);
		gpa2Slider.value = CalculateGpa(player2Gpa);
	}

	/**
 	 * Updates the player score displays
 	 */
	private void UpdateGpaTexts() {
		gpa1Text.text = "GPA: " + player1Gpa.ToString("0.0");
		gpa2Text.text = "GPA: " + player2Gpa.ToString("0.0");
	}

	/**
 	 * Updates the color of the player texts to emphasize active player
 	 */
	private void UpdateTurn() {
		if (turn == 1) {
			player1Text.color = Color.green;
			player2Text.color = Color.black;
		} else if (turn == 2) {
			player1Text.color = Color.black;
			player2Text.color = Color.green;
		} else {
			player1Text.color = Color.black;
			player2Text.color = Color.black;
		}
	}

	// Update is called once per frame
	void Update () {
		UpdateStatistics ();
		UpdateTurn ();
		UpdateSliders ();
		UpdateGpaTexts ();
	}
}

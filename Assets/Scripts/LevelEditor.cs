using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEditor : MonoBehaviour {

	public Slider greenSlider;
	public Slider redSlider;
	public Toggle duelToggle;
	public Toggle gpaToggle;
	public Toggle teleportToggle;
	public Button startButton;

	public void changeScene ()
	{
		EditorData.greenTiles = (int)greenSlider.value;
		EditorData.redTiles = (int)redSlider.value;
		EditorData.duelTiles = duelToggle.isOn;
		EditorData.gpaTiles = gpaToggle.isOn;
		EditorData.teleportTiles = teleportToggle.isOn;

        SceneManager.LoadScene("MainScene");
	}
}

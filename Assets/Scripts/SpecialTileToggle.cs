using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialTileToggle : MonoBehaviour {
	public Toggle thisToggle;

	public Toggle duelToggle;
	public Toggle teleportationToggle;
	public Toggle rngToggle;

	void Start () {
		thisToggle.onValueChanged.AddListener(delegate {
			ToggleValueChanged(thisToggle);
		});
	}
	
	void ToggleValueChanged(Toggle thisToggle){
		if (thisToggle.isOn){
			duelToggle.isOn = true;
			teleportationToggle.isOn = true;
			rngToggle.isOn = true;
			duelToggle.enabled = true;
			teleportationToggle.enabled = true;
			rngToggle.enabled = true;
		}
		else {
			duelToggle.isOn = false;
			teleportationToggle.isOn = false;
			rngToggle.isOn = false;
			duelToggle.enabled = false;
			teleportationToggle.enabled = false;
			rngToggle.enabled = false;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GMSlider : MonoBehaviour {

    public Text textValue;
    public Slider editorSlider;
	
	// Update is called once per frame
	void Update () {
        textValue.text = editorSlider.value.ToString();
    }
}

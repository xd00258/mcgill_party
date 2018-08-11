using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;

public class ModalPanel : MonoBehaviour {

	public Button yesButton;
	public Button noButton;
	public Text question;
	public GameObject modalPanelObject;

	public UnityAction myYesAction;
	public UnityAction myNoAction;

	private static ModalPanel modalPanel;

	public delegate void ClickAction ();
	public static event ClickAction OnClickedYes;
	public static event ClickAction OnClickedNo;

	public static ModalPanel Instance() {
		if(!modalPanel) {
			modalPanel = FindObjectOfType (typeof (ModalPanel)) as ModalPanel;
			if (!modalPanel)
				Debug.LogError ("Error");
		}
		return modalPanel;	
	}

	public void AcceptOrRefuse (string question, UnityAction yesEvent, UnityAction noEvent) {
		modalPanelObject.SetActive (true);

		yesButton.onClick.RemoveAllListeners ();
		yesButton.onClick.AddListener (yesEvent);
		yesButton.onClick.AddListener (ClosePanelYes);

		noButton.onClick.RemoveAllListeners ();
		noButton.onClick.AddListener (noEvent);
		noButton.onClick.AddListener (ClosePanelNo);

		this.question.text = question;
		yesButton.gameObject.SetActive (true);
		noButton.gameObject.SetActive (true);
	}

	void TestYesFunction() {
		Debug.Log ("Yes pressed");
		// if yes is pressed, should go to minigame
	}

	void TestNoFunction(){
		Debug.Log("No pressed");
		// if no is pressed, the challenged player loses a certain amount of GPA
	}

	void ClosePanelYes(){
		if (OnClickedYes != null)
			OnClickedYes ();
		modalPanelObject.SetActive (false);
	}

	void ClosePanelNo(){
		if (OnClickedNo != null)
			OnClickedNo ();
		modalPanelObject.SetActive (false);
	}

	// Use this for initialization
	void Start () {
		modalPanelObject.SetActive (false);
	}

	void Awake() {
		modalPanel = this;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void Activate() {
		modalPanelObject.SetActive (true);
		myYesAction = new UnityAction (TestYesFunction);
		myNoAction = new UnityAction (TestNoFunction);
		AcceptOrRefuse ("Do you accept the challenge?", myYesAction, myNoAction);	
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CamTrackCurrentPlayer : MonoBehaviour {

    
    public CinemachineVirtualCamera cam;
	// Use this for initialization
	void Start () {
        cam = this.GetComponent<CinemachineVirtualCamera>();
        cam.Follow = cam.LookAt = GameManager.Instance.Turn == 1 ? GameManager.Instance.Player1 : GameManager.Instance.Player2;
        GameManager.Instance.OnTurnStart += UpdateView;
        GameManager.Instance.OnGameEnd += UpdateView;
	}
	
	// Update is called once per frame
    void UpdateView(Player player, PlayerNum playerNumber) {
        cam.Follow = player.transform;
        cam.LookAt = player.transform;
	}
}

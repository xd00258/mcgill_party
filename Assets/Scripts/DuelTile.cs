using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelTile : GameTile {


    public enum Minigame { COIN_TOSS, RPS, TICTACTO };
    private int numgames = System.Enum.GetNames(typeof(Minigame)).Length;
    public int latest_winner; //for testing purposes

    void Start()
    {
        lore_text = " The grading scheme for a new course is out.\nLooks like only the top 10% get an A. Time to duel.";
    }

    public override void OnFinalLand(Player player)
    {
        print("Land on duel tile");
        MinigameManager.Instance.StartMinigame(player);
    }
}

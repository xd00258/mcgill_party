using UnityEngine;
using System.Collections;

abstract public class GameTile : MonoBehaviour {

    public Transform nextTile;
    public string lore_text;
    protected float waitAmount = 0.5f;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

	}

    //Implement in subclass
    public abstract void  OnFinalLand(Player player);

	//Moves player
    public virtual void OnTransit(Player player)
    {
        player.moves_remaining--;
        StartCoroutine(AdvancePlayer(waitAmount, player));
    }

    private IEnumerator AdvancePlayer(float wait,Player player)
    {
        yield return new WaitForSeconds(wait);
        player.MoveNext();
    }

    public GameTile GetNextTile()
    {
        return this.nextTile.GetComponentInChildren<GameTile>();
    }
}

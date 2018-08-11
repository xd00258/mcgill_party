using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]

public class Player : MonoBehaviour {

    [Range(0f,4.0f)]
    public float gpa;
    private float speed;
    public bool move;
    public int moves_remaining;
    public bool just_teleported;
    private GameTile current_tile;
    public GameTile target_tile;
	public int coinWinner;
	public Transform coinResult;
	private Text text;
    public GameObject WinObj;

    public delegate void TurnEvent();
    public TurnEvent OnTurnEnd;

	//// Use this for initialization
	void Start () {
        this.speed = 2;
        this.just_teleported = false;

		if (GameObject.Find ("resultText") != null) {
			coinResult = GameObject.Find ("resultText").transform;
		} else {
			Debug.Log ("No Object resultText found!");
			return;
		}
		coinResult.gameObject.SetActive (true);
		text = coinResult.gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (this.move)
        {
            MovePlayer();
        }
	}

    // Move player to target tile
    private void MovePlayer()
    {
        if (this.moves_remaining >= 0)
        {
                float step = GetSpeed() * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, this.target_tile.transform.position, step);
        }
    }

    //OnTriggerEnter or OnCollisionEnter?
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.gameObject.tag == "Tile")
        {
            GameTile t = other.gameObject.GetComponent<GameTile>();
            if (t && this.target_tile.Equals(t))
            {
                this.current_tile = t;
                StayOnTile();

                if (this.moves_remaining > 0 || t.GetType().ToString() == "ForkTile")
                {
                    this.current_tile.OnTransit(this);
                }
                else
                {
                    this.current_tile.OnFinalLand(this);
					if (current_tile is TeleportationTile) {
						Invoke ("EndTurn", 2.0f); 
					} else {
						EndTurn ();
					}

                }
            }
        }
    }

	private void EndTurn() {
		if (OnTurnEnd != null)
		{
			OnTurnEnd();
		}
	}

    public void InitPlayer(GameTile currentTile)
    {
        this.moves_remaining = 0;
        this.current_tile = currentTile;
        target_tile = currentTile.GetNextTile();
    }
    // Initiate player movement by specified number of tiles - Called by Game Manager
    public void InitMovePlayer(int num_moves)
    {
        moves_remaining = num_moves;
        just_teleported = false;
        //MoveNext();
    }

    public void SetActivePlayer()
    {

    }


    // Stop player movement
    public void StayOnTile()
    {
        this.move = false;
    }

    // Tells player to move to next tile - Called by TileClass (I think this description is wrong)
    public void MoveNext()
    {

        this.move = true;
        UpdateDestinationTile(current_tile.GetNextTile());
    }

    // Get player GPA
    public float GetGPA()
    {
        return this.gpa;
    }

    // Set GPA to specified value
    public void SetGPA(float score)
    {
        if(score>=0 && score<= GameManager.Instance.gpaGoal)
            this.gpa = score;
    }

    // Change GPA by specified amount
    public void UpdateGPA(float change)
    {
        this.gpa = Mathf.Clamp(this.gpa + change, 0.0f, GameManager.Instance.gpaGoal);
		//flip coin redemption
		if (this.gpa < 1.0) {
			coinWinner = LaunchCoinToss();
			//gpa boost by 1
			if (coinWinner == 1) {
				coinResult.gameObject.SetActive (true);

				this.gpa = this.gpa + 1;
				text.text = "In probation, you flipped a coin and got HEADS, you're back in the game with +1 GPA!";
				StartCoroutine(Pause(7));

				//lose the game
			} else {
				text.text = "In probation, you flipped a coin and got TAILS, you got kicked out of the school ";
				this.gpa = 0;
			}

		}

    }

	private IEnumerator Pause(int p)
	{
		yield return new WaitForSeconds(p); // waits 3 seconds
		text.text = "";
	}

    // Change current tile to match tile player is currently on
    public void UpdateCurrentTile(GameTile current)
    {
        this.current_tile = current;
    }

    public GameTile GetCurrentTile()
    {
        return this.current_tile;
    }

    // Change destination tile to next tile
    public void UpdateDestinationTile(GameTile next)
    {
        this.target_tile = next;
    }

    // Decrement number of remaining tile moves
    public void DecrementRemainingMoves()
    {
        this.moves_remaining--;
    }

    // Get number of remaining tile moves
    public int GetRemainingMoves()
    {
        return this.moves_remaining;
    }

    // Get object speed
    public float GetSpeed()
    {
        return this.speed;
    }
    // Set obejct speed
    public void SetSpeed(float spd)
    {
       this.speed = spd;
    }

    public void PlayerWon()
    {
        if(WinObj)
        {
            WinObj.SetActive(true);
        }
        //Activate confetti
    }

	private int LaunchCoinToss()
	{

		int coin = Random.Range(0, 2);
		int winner = 0;
		switch (coin)
		{
			case 0:
				Debug.Log ("Boosted by 1 GPA");
				winner = 1;
				break;
			case 1:
				Debug.Log("Kicked out of school");
			    winner = 2;
			    break;
		}
		  	return winner;
	}

}

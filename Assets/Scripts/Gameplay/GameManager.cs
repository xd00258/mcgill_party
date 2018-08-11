using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;

//Enum for identifying players
public enum PlayerNum
{
    Player1, Player2
}

public class GameManager : ComponentSingleton<GameManager>
{
     #region GAME_STATS

    //Current turns
    public int TurnCount
    {
        get { return m_turnCount; }
        private set { if (value > 0) m_turnCount = value; }
    }

    //Player GPAs
    public float Player1GPA
    {
        get { return m_p1 ? m_p1.GetGPA() : gpaMin; }
    }

    public float Player2GPA
    {
        get { return m_p2 ? m_p2.GetGPA() : gpaMin; }
    }

    public int Turn {
        get { return (m_playerCounter == PlayerNum.Player1 ? 1 : 2); }
    }

    public Transform Player1
    {
        get { return m_p1.transform; }
    }

    public Transform Player2
    {
        get { return m_p2.transform; }
    }

    private int m_turnCount = 1;
    #endregion

    #region PUBLIC_VARIABLES

    public DiceRoller dice;
    public int diceMin = 1;
    public int diceMax = 7;
    [Header("Player Prefabs")]
    public Player player1Prefab;
    public Player player2Prefab;

    [Space(10)]
    [Header("Start Tile")]
    [Tooltip("Tile at which the players will begin")]
    public GameTile startTile;

    [Tooltip("GPA which players are trying to achieve")]
    [Range(2.0f, 4.0f)]
    public float gpaGoal = 4.0f;

    [Tooltip("GPA under which players will lose")]
    [Range(0.0f, 0.5f)]
    public float gpaMin = 0.3f;

    [Tooltip("GPA that players start with")]
    [Range(0.5f, 1.5f)]
    public float gpaStart = 1.0f;
    #endregion

    #region PRIVATE_VARIABLES

    //references for player 1 and player2 on run time
    private Player m_p1;
    private Player m_p2;

    private Player m_currentPlayer;
	private Player other_Player;
    private PlayerNum m_playerCounter;

    private bool turn_active = false;
    private bool game_done = false;
    private bool minigame_in_progress = false;
    #endregion

    #region EVENTS

    public delegate void TurnEvent(Player player, PlayerNum playerNumber);

    public TurnEvent OnTurnStart;
    public TurnEvent OnTurnEnd;
    public TurnEvent OnGameEnd;

    #endregion

    #region UNITY_METHODS

    // Use this for initialization
    void Start()
    {
        SpawnPlayers();

        //Register minigame events
        MinigameManager.Instance.OnMinigameStart += MinigameStart;
        MinigameManager.Instance.OnMinigameEnd += MinigameEnd;
		MinigameManager.Instance.players = new List<Player> { m_p1, m_p2 };
		ModalPanel.OnClickedYes += acceptMiniGame;
		ModalPanel.OnClickedNo += refuseMiniGame;
    }

    public void Update()
    {
        if(Input.GetButtonDown("RollDice") && !turn_active && !game_done && !minigame_in_progress)
        {
            StartCoroutine(StartTurn());
        }
    }

    private void OnGUI()
    {
        if (!turn_active && !game_done && m_turnCount > 1) {
            GUIStyle centerStyle = new GUIStyle("box");
            centerStyle.alignment = TextAnchor.MiddleCenter;
            var text = "";
            if (m_currentPlayer.just_teleported){
                text = "How did you end up here? Did you skip ahead or did you fall behind?\nYou e-mail your faculty for answers.";
            }
            else{
                text = m_currentPlayer.GetCurrentTile().lore_text;
            }
            var textSize = GUI.skin.label.CalcSize(new GUIContent(text));
            GUI.Label(new Rect((Screen.width - textSize.x)/2, 5*Screen.height/6, textSize.x+15, textSize.y+6), text, centerStyle);
        }
    }

    #endregion

    #region PRIVATE_METHODS

    private bool SpawnPlayers()
    {
        TurnCount = 1;

        if (player1Prefab && player2Prefab && startTile)
        {
            //Instantiate objects
            m_p1 = Instantiate<GameObject>(player1Prefab.gameObject).GetComponent<Player>();
            m_p2 = Instantiate<GameObject>(player2Prefab.gameObject).GetComponent<Player>();

            //TODO Initialize GPA of objects
            m_p1.SetGPA(gpaStart);
            m_p2.SetGPA(gpaStart);

            //Position objects
            m_p1.transform.position = startTile.transform.position;
            m_p2.transform.position = startTile.transform.position;

            m_p1.InitPlayer(startTile);
            m_p2.InitPlayer(startTile);

            //Current player is player 1
            m_playerCounter = PlayerNum.Player1;
            m_currentPlayer = m_p1;

            return true;
        }

        return false;
    }

    private IEnumerator RollDice()
    {
        this.GetComponent<Animator>().SetTrigger("RollDice");

        yield return new WaitForSeconds(1.2f);
        //Rolls 6 sided die
        int diceRoll = Random.Range(diceMin, diceMax+1);
        //Try to reduce the chance of getting 1
        if(diceRoll==1)
        {
            diceRoll = Random.Range(diceMin, diceMax+1);
        }

        dice.gameObject.SetActive(true);
        dice.RollDice(diceRoll);

        yield return new WaitForSeconds(dice.duration + 1.0f);
		m_currentPlayer.InitMovePlayer(diceRoll);
        dice.gameObject.SetActive(false);

        yield return null;
    }
   

    private IEnumerator StartTurn()
    {

        turn_active = true;

        m_currentPlayer = m_playerCounter == PlayerNum.Player1 ? m_p1 : m_p2;
        TurnCount++;
        if (OnTurnStart != null)
        {
            OnTurnStart(m_currentPlayer,m_playerCounter);
        }
        //TODO: register EndTurn to current player
        m_currentPlayer.OnTurnEnd += this.EndTurn;

        //Start Dice Roll
        yield return RollDice();

        m_currentPlayer.SetActivePlayer();
        this.GetComponent<Animator>().SetTrigger("PlayerMove");
        m_currentPlayer.moves_remaining--;
        m_currentPlayer.MoveNext();

        yield return null;
    }

    private void EndTurn()
    {
        if (OnTurnEnd != null)
        {
            OnTurnEnd(m_currentPlayer, m_playerCounter);
        }

        m_playerCounter = m_playerCounter == PlayerNum.Player1 ? PlayerNum.Player2 : PlayerNum.Player1;

        turn_active = false;
        //TODO: unregister EndTurn from current player
        m_currentPlayer.OnTurnEnd -= this.EndTurn;

        PlayerNum winner = PlayerNum.Player1;

        if (GameDone(out winner))
        {
            game_done = true;
            EndGame(winner);
        }
        else
        { 
			if (GameObject.ReferenceEquals(m_p1.GetCurrentTile(), m_p2.GetCurrentTile())) {
				minigame_in_progress = true;
				ModalPanel.Instance ().Activate ();
			} 

            this.GetComponent<Animator>().SetTrigger("TurnWait");
            //Activate waiting camera
            //StartTurn();
        }


    }

	private void acceptMiniGame() {
		MinigameManager.Instance.StartMinigame (m_currentPlayer);
	}

	private void refuseMiniGame() {
		other_Player = m_playerCounter == PlayerNum.Player1 ? m_p1 : m_p2;

		if ((other_Player.GetGPA () - 0.6f) < 0) {
			other_Player.SetGPA (0);
		} else {
			other_Player.SetGPA (other_Player.GetGPA () - 0.6f);
		}

		minigame_in_progress = false;
		PlayerNum winner = PlayerNum.Player1;

		if (GameDone(out winner))
		{
			game_done = true;
			EndGame(winner);
		}
	}

	void checkGPA(){
		
	}


    private void EndGame(PlayerNum winningPlayer)
    {
        Player winner = winningPlayer != PlayerNum.Player1 ? m_p2 : m_p1;
        Player loser = winningPlayer == PlayerNum.Player1 ? m_p2 : m_p1;

        m_playerCounter = winningPlayer;
        //Activate End Game Camera
        this.GetComponent<Animator>().SetBool("GameDone",true);
        if (OnGameEnd != null)
        {
            OnGameEnd(winner, winningPlayer);
        }
        winner.PlayerWon();
        loser.gameObject.SetActive(false);
        //Display winner

    }

    private void Reset()
    {
        if (m_p1)
        {
            Destroy(m_p1.gameObject);
        }

        if (m_p2)
        {
            Destroy(m_p2.gameObject);
        }
    }

    private bool GameDone(out PlayerNum winningPlayer)
    {
        float p1Score = m_p1.GetGPA();
        float p2Score = m_p2.GetGPA();

        bool p1Won = p2Score < gpaMin || p1Score >= gpaGoal;
        bool p2Won = p1Score < gpaMin || p2Score >= gpaGoal;

        winningPlayer = p1Won ? PlayerNum.Player1 : PlayerNum.Player2;

        return p1Won || p2Won;
    }

    private void MinigameStart(Player startingPlayer)
    {
        this.GetComponent<Animator>().SetTrigger("RPS Active");
        minigame_in_progress = true;
    }

    private void MinigameEnd(Player winningPlayer)
    {
        float minigameWinScore = 0.5f;

        Player losingPlayer = winningPlayer == m_p1 ? m_p2 : m_p1;

        winningPlayer.UpdateGPA(minigameWinScore);
        losingPlayer.UpdateGPA(-minigameWinScore);

        minigame_in_progress = false;

		PlayerNum winner = PlayerNum.Player1;

        this.GetComponent<Animator>().SetTrigger("TurnWait");

		if (GameDone(out winner))
		{
			game_done = true;
			EndGame(winner);
		}
    }

    #endregion


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;

public class MinigameManager : ComponentSingleton<MinigameManager>
{

    public bool minigameStart;

    public delegate void MinigameEvent(Player p);

    public MinigameEvent OnMinigameStart; //p = player that initiated the game
    public MinigameEvent OnMinigameEnd; //p = player that wins the game

    public List<Player> players; //Initialized at start of GameManager

    public float minigameLength = 20f;
    public float timer = 0;

    // 1 = Rock, 2 = Paper, 3 = Scissors
    int p1Choice = 0;
    int p2Choice = 0;

    public ArmAnimator p1Arm;
    public ArmAnimator p2Arm;
    public TimerMiniGame timerArt;

    void Start()
    {

    }


    void Update()
    {
        if (minigameStart)
        {
            timer += Time.deltaTime;
            if (Input.GetButtonDown("P1_Rock"))
            {
                p1Choice = 1;
            }
            else if (Input.GetButtonDown("P1_Paper"))
            {
                p1Choice = 2;
            }
            else if (Input.GetButtonDown("P1_Scissors"))
            {
                p1Choice = 3;
            }

            if (p1Arm != null && p1Choice > 0)
            {
                p1Arm.StopAnimation();
            }

            if (Input.GetButtonDown("P2_Rock"))
            {
                p2Choice = 1;
            }
            else if (Input.GetButtonDown("P2_Paper"))
            {
                p2Choice = 2;
            }
            else if (Input.GetButtonDown("P2_Scissors"))
            {
                p2Choice = 3;
            }

            if (p2Arm != null && p2Choice > 0)
            {
                p2Arm.StopAnimation();
            }
            //Visually update UI timer as well

            timerArt.SetTIme(minigameLength - timer);

            if (timer > minigameLength || (p1Choice > 0 && p2Choice > 0))
            {
                p1Choice = p1Choice == 0 ? Random.Range(1, 4) : p1Choice;
                p2Choice = p2Choice == 0 ? Random.Range(1, 4) : p2Choice;

                minigameStart = false;
                StartCoroutine(EvaluateGame());
            }
        }
    }

    IEnumerator EvaluateGame()
    {
        //Visuals
        if (p1Arm != null)
        {
            switch (p1Choice)
            {
                case 1:
                    p1Arm.Rock();
                    break;
                case 2:
                    p1Arm.Paper();
                    break;
                case 3:
                    p1Arm.Scissors();
                    break;
            }
        }

        if (p2Arm != null)
        {
            switch (p2Choice)
            {
                case 1:
                    p2Arm.Rock();
                    break;
                case 2:
                    p2Arm.Paper();
                    break;
                case 3:
                    p2Arm.Scissors();
                    break;
            }
        }

        yield return new WaitForSeconds(2.0f);

        if (p1Choice == p2Choice)
        {
            EnableElements();
        }
        else
        {
            if ((p1Choice - p2Choice + 3) % 3 == 1)
            {

                if (p2Arm != null)
                {
                    p2Arm.HideElements();
                }
                yield return new WaitForSeconds(1.0f);
               
                OnMinigameEnd(players[0]);
                DisableElements();
            }
            else
            {
                if (p1Arm != null)
                {
                    p1Arm.HideElements();
                }
                yield return new WaitForSeconds(1.0f);

                OnMinigameEnd(players[1]);
                DisableElements();
            }
        }

        //Hide choices
        yield return null;
    }

    void changePicture(int playerNumber)
    {
        if (playerNumber == 1)
        {
            // Change p1 picture
        }
        if (playerNumber == 2)
        {
            // Change p2 picture
        }
    }
    public void StartMinigame(Player player)
    {

        EnableElements();
        //TODO: Also, need to move the camera (within GameManager? or use the cinemachine stuff? ELIE HELP!)
        OnMinigameStart(player); //Disable the "normal" game
    }

    private void EnableElements() { 
        minigameStart = true;
        p1Choice = 0;
        p2Choice = 0;
        timer = 0;

        if (p1Arm != null)
        {
            p1Arm.HideElements();
            p1Arm.StartAnimation();
        }
        if (p2Arm != null)
        {
            p2Arm.HideElements();
            p2Arm.StartAnimation();
        }
        if (timerArt != null)
        {
            timerArt.SetTIme(minigameLength);
            timerArt.gameObject.SetActive(true);
        }
    }

    private void DisableElements()
    {
        minigameStart = false;
        p1Choice = 0;
        p2Choice = 0;

        if (p1Arm != null)
        {
            p1Arm.HideElements();
        }
        if (p2Arm != null)
        {
            p2Arm.HideElements();
        }
        if (timerArt != null) {
            timerArt.SetTIme(minigameLength);
            timerArt.gameObject.SetActive(false);
        }
    
    }

}

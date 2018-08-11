using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceRoller : MonoBehaviour {

    public delegate void GameEvent();

    public GameEvent OnDiceRoll;

    public int numSwitches = 20;
    public float duration = 3;

    public TextMeshPro display;

    public void RollDice(int finalResult)
    {
        StartCoroutine(AnimateDice(finalResult));
    }

    private IEnumerator AnimateDice(int final)
    {
        int previous = 0;
        for(int i =0; i<numSwitches;i++)
        {
            int choice = previous;
            while(choice==previous)
            {
                choice = Random.Range(1, 7);
            }
            previous = choice;
            display.text = previous.ToString();
            //change display
            yield return new WaitForSeconds(duration / (float)numSwitches);
        }

        display.text = final.ToString();
    }
}

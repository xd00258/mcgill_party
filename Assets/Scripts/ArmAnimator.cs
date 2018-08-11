using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAnimator : MonoBehaviour {

    public bool animate = false;
    public float topHeight = 0;
    public float lowHeight = 0;

    private float lowSpeed = 0;
    public float quickSpeed = 0;
    public float speed = 5;


    public GameObject rock;
    public GameObject paper;
    public GameObject scissors;

	// Use this for initialization
	public void StartAnimation () {
        animate = true;
        lowSpeed = speed;
        StartCoroutine(RaiseArm());
    }

    public void Update()
    {
        if(!animate)
        {
            StopAnimation();
        }
    }
    public void StopAnimation()
    {
        animate = false;
        lowSpeed = quickSpeed;

    }

    IEnumerator RaiseArm()
    {
        while(this.transform.localPosition.y < topHeight)
        {
            if(!animate)
            {
                StartCoroutine(LowerArm());
                yield break ;
            }

            this.transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));

            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(LowerArm());
    }

    IEnumerator LowerArm()
    {
        while (this.transform.localPosition.y > lowHeight)
        {


            float amount = Mathf.Min(this.transform.localPosition.y - lowHeight, lowSpeed * Time.deltaTime);
            this.transform.Translate(new Vector3(0, -1.0f* amount, 0));
            yield return new WaitForEndOfFrame();
        }

        if(animate)
        {
            StartCoroutine(RaiseArm());
        }
    }

    public void Rock()
    {
        rock.gameObject.SetActive(true);
    }

    public void Paper()
    {
        paper.gameObject.SetActive(true);
    }
    public void Scissors()
    {
        scissors.gameObject.SetActive(true);
    }

    public void HideElements()
    {
        scissors.gameObject.SetActive(false);
        paper.gameObject.SetActive(false);
        rock.gameObject.SetActive(false);
    }
}

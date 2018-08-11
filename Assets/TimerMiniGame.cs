using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class TimerMiniGame : MonoBehaviour
{

    TextMeshPro tmpro;
    float m_time;

    public void Start()
    {
        tmpro = this.GetComponent<TextMeshPro>();
    }

    //In seconds
    public void SetTIme(float t) { 
        if(t<0){
            return;
        }

        int ms;
        int s;
        int m;
        int h;

        h = (int)(t / (60 * 60));
        m = (int)((t-h*3600) /60);
        s = (int)(t-m*60);
        ms = (int)((t - Mathf.Floor(t))*100);

        string res = "";
        if(h>0){
            res += h.ToString();
        }

        if(m>0){
            if(h>0){
                res += ":";
            }
            res += m.ToString();
        }

        if(s>0)
        {
            if(m>0){
                res += ":";
            }
            res += s.ToString();
            if(s<10 && ms>0)
            {
                res += ":" + ms.ToString();
            }
        }

        tmpro.text = res;
        
    }



}

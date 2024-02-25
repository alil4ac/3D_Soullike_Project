using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_Image : MonoBehaviour
{
    #region RedScreen
    [SerializeField]
    private Image RedScreen1;
    [SerializeField]
    private Image RedScreen2;
    bool Red1Coroutine = false;
    public void RedScreenOn()
    {
        if(Red1Coroutine == false)
        {
            RedScreen1.gameObject.SetActive(true);
            Red1Coroutine = true;
            StartCoroutine(RedScreen1On());
        }
    }
    public void RedScreenOff()
    {
        if(Red1Coroutine == true)
        {
            RedScreen1.gameObject.SetActive(false);
            Red1Coroutine = false;
            StopCoroutine(RedScreen1On());
        }
    }
    public void RedScreenOn2()
    {
        RedScreen2.gameObject.SetActive(true);
        StartCoroutine(RedScreen2On());
    }
    public IEnumerator RedScreen2On()
    {
        float opacity = 1f;
        Color Red2 = RedScreen2.color;
        while (opacity >= 0f)
        {
            opacity -= 0.1f;
            Red2.a = opacity;
            RedScreen2.color = Red2;
            yield return new WaitForSeconds(0.1f);
        }
        if(opacity <= 0)
        {
            RedScreen2.gameObject.SetActive(false);
        }
    }
    public IEnumerator RedScreen1On()
    {
        float opacity = 1f;
        Color Red1 = RedScreen1.color;

        while(Red1Coroutine == true)
        {
            while (opacity >= 0.8f)
            {
                opacity -= 0.01f;
                Red1.a = opacity;
                RedScreen1.color = Red1;
                yield return new WaitForSeconds(0.01f);
            }
            while (opacity <= 1f)
            {
                opacity += 0.01f;
                Red1.a = opacity;
                RedScreen1.color = Red1;
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(1f);
        }
        
    }
    #endregion
}

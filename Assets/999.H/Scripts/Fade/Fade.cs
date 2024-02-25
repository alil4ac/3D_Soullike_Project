using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Fade : MonoBehaviour
{
    #region Data

    [SerializeField]
    private Image[] image;

    [SerializeField]
    private TextMeshProUGUI[] text;

    private Color AlphaZero = new Color(1, 1, 1, 0);

    private Color DefaultColor = new Color(1, 1, 1, 1);

    private Color DefauleTextColor = new Color(1, 1, 1, 1);

    #endregion
    #region Method

    public void FadeIn(float Time)
    {
        foreach (Image i in image)
        {
            i.DOFade(1f, Time);
        }
        foreach (TextMeshProUGUI t in text)
        {
            t.DOFade(1f, Time);
        }
    }

    public void FadeOut(float Time)
    {
        foreach (Image i in image)
        {
            i.DOFade(0f, Time);
        }
        foreach (TextMeshProUGUI t in text)
        {
            t.DOFade(0f, Time);
        }
    }
    
    #endregion

    public void UIStart()
    {
        foreach (Image i in image)
        {
            i.color = AlphaZero;
        }
        foreach (TextMeshProUGUI t in text)
        {
            t.color = DefauleTextColor;
        }
    }
}

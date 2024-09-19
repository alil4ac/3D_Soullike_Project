using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SystemButtons : MonoBehaviour
{
    [SerializeField]
    private Image HighLighted;

    [SerializeField]
    private TextMeshProUGUI SideText;

    public int Index;


    private Color AlphaZero = new Color(1, 1, 1, 0);

    private Color FullAlpha = new Color(1, 1, 1, 1);

    private Color DefaultTextColor = new Color(0.8588235f, 0.8667861f, 0.9137255f, 1f);

    private Color DefaultAlphaZero = new Color(0.8588235f, 0.8667861f, 0.9137255f, 0f);

    public void UIStart()
    {
        SideText.color = DefaultAlphaZero;
        HighLighted.color = AlphaZero;
    }


    public void PointerEnter()
    {
        SideText.color = DefaultTextColor;
        HighLighted.color = FullAlpha;
    }

    public void PointerDown()
    {
        SideText.color = DefaultAlphaZero;
        HighLighted.color = AlphaZero;
        UIManager.Instance.SelectSystemButton(Index);
    }

    public void PointerExit()
    {
        SideText.color = DefaultAlphaZero;
        HighLighted.color = AlphaZero;
    }
}

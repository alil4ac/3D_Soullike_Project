using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EquipButton : MonoBehaviour
{
    [SerializeField]
    private Image Highlight;

    public int Index;

    private Color AlphaZero = new Color(1, 1, 1, 0);

    private Color FullAlpha = new Color(1, 1, 1, 1);

    private void Start()
    {
        Highlight.color = AlphaZero;
    }

    public void PointEnter()
    {
        Highlight.color = FullAlpha;
    }

    public void PointDown()
    {
        Highlight.color = AlphaZero;
        UIManager.Instance.ChangeInvenTab(Index);
        GameSoundManager.Instance.PlayClick();
    }

    public void PointExit()
    {
        Highlight.color = AlphaZero;
    }
}

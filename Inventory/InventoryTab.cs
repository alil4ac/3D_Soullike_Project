using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryTab : MonoBehaviour
{
    [SerializeField]
    private Image BackgroundImage;

    [SerializeField]
    private Image Selected;

    private bool IsSelected = false;

    private void Start()
    {
        if (IsSelected) { return; }
        Selected.color = new Color(1, 1, 1, 0);
    }

    public void PointerEnter()
    {
        if (IsSelected) { return; }
        Selected.color = new Color(1, 1, 1, 1);
    }

    public void PointerDown(int i)
    {
        IsSelected = true;
        Selected.color = new Color(1, 1, 1, 1);
        UIManager.Instance.ChangeInvenTab(i);
        GameSoundManager.Instance.PlayClick();
    }

    public void PointerExit()
    {
        if (IsSelected) { return; }
        Selected.color = new Color(1, 1, 1, 0);
    }

    public void ChangeTab()
    {
        IsSelected = false;
        Selected.color = new Color(1, 1, 1, 0);
    }

    public void SelectedTab()
    {
        IsSelected = true;
        Selected.color = new Color(1, 1, 1, 1);
    }
}

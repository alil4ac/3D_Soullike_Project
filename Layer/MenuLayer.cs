using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MenuLayer : MonoBehaviour
{
    [SerializeField]
    private Fade MenuFade;

    [SerializeField]
    private MenuButton[] _MenuButton;

    public void OpenMenu()
    {
        MenuFade.FadeIn(0.5f);
        UIManager.Instance.IsActiveMenuLayer = true;
        foreach(MenuButton btn in _MenuButton)
        {
            btn.ActiveButton();
        }
    }

    public void CloseMenu()
    {
        foreach(MenuButton btn in _MenuButton)
        {
            btn.DisableButton();
        }
        MenuFade.FadeOut(0.5f);

        Invoke("Exit", 0.5f);
    }

    private void Exit()
    {
        UIManager.Instance.IsActiveMenuLayer = false;
        this.gameObject.SetActive(false);
    }

    public void UIStart()
    {
        MenuFade.UIStart();

        this.gameObject.SetActive(false);
    }
}

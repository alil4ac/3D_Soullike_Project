using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemLayer : MonoBehaviour
{
    [SerializeField]
    private SystemButtons[] Buttons;

    [SerializeField]
    private Fade SystemFade;


    public void UIStart()
    {
        for(int i = 0; i < Buttons.Length;)
        {
            if(Buttons[i] != null)
            {
                Buttons[i].UIStart();

                i++;
            }
        }

        SystemFade.UIStart();
        UIManager.Instance.IsActiveSystemLayer = false;
        this.gameObject.SetActive(false);
    }

    public void OpenSystemLayer()
    {
        SystemFade.FadeIn(0.5f);

        UIManager.Instance.IsActiveSystemLayer = true;
    }

    public void CloseSystemLayer()
    {
        SystemFade.FadeOut(0.5f);

        Invoke(nameof(Exit), 0.5f);
    }

    private void Exit()
    {
        UIManager.Instance.IsActiveSystemLayer = false;
        this.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItem : MonoBehaviour
{
    [SerializeField]
    private Fade FadePickItem;

    public void UIStart()
    {
        FadePickItem.UIStart();
        this.gameObject.SetActive(false);
    }

    public void ActivePickItem()
    {
        FadePickItem.FadeIn(0.5f);
        UIManager.Instance.IsActivePickItem = true;
    }

    public void DisablePickItem()
    {
        FadePickItem.FadeOut(0.5f);
        Invoke(nameof(Exit), 0.5f);
    }

    private void Exit()
    {
        UIManager.Instance.IsActivePickItem = false;
        this.gameObject.SetActive(false);
    }
}

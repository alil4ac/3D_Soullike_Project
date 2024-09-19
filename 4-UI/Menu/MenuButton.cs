using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField]
    private int ButtonIndex;

    [SerializeField]
    private Image HighLight;

    private void Awake()
    {
        DisableButton();
    }

    public void ActiveButton()
    {
        HighLight.gameObject.SetActive(true);

        HighLight.color = new Color(1, 1, 1, 0);
    }

    public void DisableButton()
    {
        HighLight.color = new Color(1, 1, 1, 0);

        HighLight.gameObject.SetActive(false);
    }

    public void PointEnter()
    {
        HighLight.color = new Color(1, 1, 1, 1);
    }

    public void PointExit()
    {
        HighLight.color = new Color(1, 1, 1, 0);
    }

    public void PointDown()
    {
        UIManager.Instance.SelectedMenu(ButtonIndex);
        GameSoundManager.Instance.PlayClick2();
    }
}

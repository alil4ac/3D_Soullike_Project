using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class ItemSlot : MonoBehaviour
{
    [SerializeField]
    private Image Background;

    [SerializeField]
    private Image Icon;

    [SerializeField]
    private Image Helighted;

    [SerializeField]
    private Image Selected;

    public int Index;


    private void Awake()
    {
        DisableIcon();
    }

    public void ActiveIcon(Sprite Resource)
    {
        Icon.gameObject.SetActive(true);
        Icon.sprite = Resource;
        Helighted.gameObject.SetActive(true);
        Helighted.color = new Color(1, 1, 1, 0);
        Selected.color = new Color(1, 1, 1, 0);
    }

    public void DisableIcon()
    {
        Icon.sprite = null;
        Icon.gameObject.SetActive(false);
        Helighted.color = new Color(1,1,1,0);
        Helighted.gameObject.SetActive(false);
        Selected.color = new Color(1, 1, 1, 0);
    }

    public void PointerEnter(int i)
    {
        Helighted.color = new Color(1, 1, 1, 1);

        UIManager.Instance.SetInventoryItemText(Index);

    }

    public void PointerExit()
    {
        Helighted.color = new Color(1, 1, 1, 0);

        UIManager.Instance.ClearOptionText();
    }

    public void PointerDown(int i)
    {
        Selected.color = new Color(1, 1, 1, 1);
        UIManager.Instance.SelectItem(i);

        CharacterManager.Instance.ChangeEquip(i);
    }

    public void EquipItem()
    {
        Selected.color = new Color(1, 1, 1, 1);
    }

    public void ChangeSelect()
    {
        Selected.color = new Color(1, 1, 1, 0);
    }

}

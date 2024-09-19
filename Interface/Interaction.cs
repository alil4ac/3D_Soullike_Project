using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Interaction : MonoBehaviour
{
    [SerializeField]
    private Image ItemIcon;

    [SerializeField]
    private TextMeshProUGUI ItemName;

    [SerializeField]
    private Fade InteractionFade;

    bool isFade=false;

    public void UIStart()
    {
        InteractionFade.UIStart();
        this.gameObject.SetActive(false);
    }

    public void OpenTab(int ItemType, int ItemIndex) //아이템 획득 결과창 열기
    {
        if(ItemType == 0)
        {
            ItemIcon.sprite = ItemManager.Instance.WeaponData[ItemIndex].ItemImage;

            ItemName.text = ItemManager.Instance.WeaponData[ItemIndex].ItemName;

            InteractionFade.FadeIn(0.5f);
        }

        else if(ItemType >= (int)ItemColumn.EItemType.Helmat && ItemType <= (int)ItemColumn.EItemType.LegArmor)
        {
            switch (ItemType)
            {
                case 1:
                    ItemIcon.sprite = ItemManager.Instance.HelmatData[ItemIndex].ItemImage;

                    ItemName.text = ItemManager.Instance.HelmatData[ItemIndex].ItemName;

                    break;
                case 2:
                    ItemIcon.sprite = ItemManager.Instance.TopArmorData[ItemIndex].ItemImage;

                    ItemName.text = ItemManager.Instance.TopArmorData[ItemIndex].ItemName;

                    break;
                case 3:
                    ItemIcon.sprite = ItemManager.Instance.GauntletData[ItemIndex].ItemImage;

                    ItemName.text = ItemManager.Instance.GauntletData[ItemIndex].ItemName;

                    break;
                case 4:
                    ItemIcon.sprite = ItemManager.Instance.LegArmorData[ItemIndex].ItemImage;

                    ItemName.text = ItemManager.Instance.LegArmorData[ItemIndex].ItemName;

                    break;
            }
        }

        InteractionFade.FadeIn(0.5f);
        Invoke("IsFadeDone", 0.5f);
    }

    void IsFadeDone()
    {
        if (isFade == false)
        {
            isFade = true;
            UIManager.Instance.IsActiveInteraction=true;
        }
    }

    public void CloseTab() //아이템 획득 결과창 닫기
    {
        InteractionFade.FadeOut(0.5f);
        
        Invoke("Exit", 0.5f);
    }

    private void Exit()
    {
        if (isFade == true)
        {
            isFade = false;
        }
        UIManager.Instance.IsActiveInteraction = false;
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && GameManager.Instance.IsPaused == false&&isFade==true)
        {
            CloseTab();
        }
    }
}

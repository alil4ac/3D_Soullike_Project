using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuickSlot : MonoBehaviour
{
    [SerializeField]
    private Image WeaponSlot;

    [SerializeField]
    private Image SkillSlot;

    [SerializeField]
    private Image PotionSlot;


    private Color AlphaZero = new Color(1, 1, 1, 0);

    public void UIStart()
    {
        WeaponSlot.sprite = UIManager.Instance.ItemImageResource[0][CharacterManager.Instance.Equip.Inventory[0][CharacterManager.Instance.Equip.EquipIndex[0]]];

        SkillSlot.sprite = null;

        SkillSlot.color = AlphaZero;

        PotionSlot.sprite = null;

        PotionSlot.color = AlphaZero;

    }

    public void ChangeWeapon()
    {
        if (WeaponSlot.gameObject.activeInHierarchy)
        {
            WeaponSlot.gameObject.SetActive(true);
        }
        if(WeaponSlot.color.a != 1f)
        {
            WeaponSlot.color = Color.white;
        }

        WeaponSlot.sprite = UIManager.Instance.ItemImageResource[0][CharacterManager.Instance.Equip.Inventory[0][CharacterManager.Instance.Equip.EquipIndex[0]]];
    }

}

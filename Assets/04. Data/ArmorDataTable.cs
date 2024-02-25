using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "ArmorDataTable", menuName = ("ArmorDataTable"), order = int.MaxValue)]
public class ArmorDataTable : ScriptableObject
{
    [SerializeField]
    private Sprite _ItemImage;

    public Sprite ItemImage { get { return _ItemImage; } }

    [SerializeField]
    private string _ItemName;

    public string ItemName { get { return _ItemName; } }

    [SerializeField]
    private string _ItemType;

    public string ItemType { get { return _ItemType; } }

    [SerializeField]
    private float _ArmorPoint;

    public float ArmorPoint { get { return _ArmorPoint; } }

    //public float HitReduce(float Player_Def)
    //{
    //    float defCorrect = 1000;
    //    float hitReduce = (Player_Def /(Player_Def + defCorrect)*100);
    //    return hitReduce;
    //}

    //public float HitDamageValue(float Mob_DMG, float HitReduce)
    //{
    //    float hitDamage = Mob_DMG*(1-(HitReduce/100));
    //    return hitDamage;
    //}
}

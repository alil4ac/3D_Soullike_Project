using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpButton : MonoBehaviour
{
    public int ButtionIndex;

    public void PointerDown()
    {
        UIManager.Instance.SetLevelStackUp(ButtionIndex);
    }
}

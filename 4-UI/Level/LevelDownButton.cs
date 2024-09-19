using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDownButton : MonoBehaviour
{
    public int ButtionIndex;

    public void PointerDown()
    {
        UIManager.Instance.SetLevelStackDown(ButtionIndex);
    }
}

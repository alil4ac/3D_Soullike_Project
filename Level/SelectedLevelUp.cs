using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedLevelUp : MonoBehaviour
{
    public void PointerDown()
    {
        UIManager.Instance.SelectedLevelUp();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : SingleTone<LayerManager>
{
    #region LayerMask

    public int L_Player = LayerMask.GetMask("Player");

    #endregion

    #region Method

    public bool ColLayerAndLayerMask(int collayer, int layerMask)
    {
        string LayerName = LayerMask.LayerToName(collayer);

        int colLayerMask = LayerMask.GetMask(LayerName);

        if ((colLayerMask & layerMask) != 0) { return true; }

        else { return false; }
    }

    #endregion

    private void Awake()
    {
        if(LayerManager.Instance != null && LayerManager.Instance != this)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}

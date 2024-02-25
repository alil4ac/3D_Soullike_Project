using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHolders : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> PlayerEffects;

    private GameObject obj;

    private void Awake()
    {
        
    }

    public GameObject SerachPlayerEffects(string type)
    {
        obj = null;
        foreach(GameObject eff in PlayerEffects)
        {
            if(eff.name == type)
            {
                obj = eff;
                break;
            }
        }
        if (obj == null)
        {
            Debug.LogError("No Serach Effect Found List");
            return null;
        }
        return obj;
    }


}

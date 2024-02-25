using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour
{
    public void Scenec()
    {
        SceneLoadManager.Instance.LoadSceneToLoading(1);
    }
}

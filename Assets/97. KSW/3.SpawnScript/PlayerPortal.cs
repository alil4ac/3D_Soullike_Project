using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPortal : MonoBehaviour
{
    //private bool isNearPortal = false;

    //private int SceneIndex = 1;

    //void Update()
    //{
    //    if (isNearPortal && Input.GetKeyDown(KeyCode.E))
    //    {
    //        SceneLoadManager.Instance.LoadSceneToLoading(SceneIndex++);
    //        isNearPortal = false;
    //    }
    //}

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("portal"))
    //    {
    //        isNearPortal = true;
    //    }
    //}

    //void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("portal"))
    //    {
    //        isNearPortal = false;
    //    }
    //}
    //void LoadNextScene()
    //{
    //    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

    //    if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
    //    {
    //        int nextSceneIndex = currentSceneIndex + 1;
    //        string nextSceneName = SceneUtility.GetScenePathByBuildIndex(nextSceneIndex);
    //        nextSceneName = nextSceneName.Substring(nextSceneName.LastIndexOf("/") + 1);
    //        nextSceneName = nextSceneName.Substring(0, nextSceneName.Length - 6);

    //        LoadingSceneController.LoadScene(nextSceneName);
    //    }
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoadManager : SingleTone<SceneLoadManager>
{
    public float progess;

    public Vector3[] SpawnPos;
    public Vector3[] SpawnRot;

    public bool IsDone = false;

    public bool FadeDone = false;

    private int NextScene;

    public int NextSceneIndex { get { return NextScene; } }

    private void Awake()
    {
        if (SceneLoadManager.Instance != null && SceneLoadManager.Instance != this)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void LoadSceneToLoading(int SceneNumber)
    {
        NextScene = SceneNumber;
        SceneManager.LoadScene(5);
        GameManager.Instance.IsPaused = true;
        GameSoundManager.Instance.SetLoadingBGM();
    }

    public void StartLoad()
    {
        StartCoroutine(_LoadSceneToLoading(NextScene));
    }

    private IEnumerator _LoadSceneToLoading(int SceneNumber)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(SceneNumber);
        
        async.allowSceneActivation = true;
        progess = async.progress;
        IsDone = async.isDone;

        yield return async;

        if (async.isDone == true)
        {
            if(GameManager.Instance.IsRestart == true)
            {
                GameManager.Instance.IsRestart = false;
                GameManager.Instance.RestartScene();
            }

            IsDone = false;
            FadeDone = false;

            if(SceneNumber == 0)
            {
                GameManager.Instance.Return_Title();
            }
            else
            {
                SetPosition(SceneNumber);
                GameSoundManager.Instance.SetStageBGM(SceneNumber);
                if (UIManager.Instance != null && UIManager.Instance.IsActiveFadeLayer)
                {
                    UIManager.Instance.InGameFadeOut();
                }
            }

            progess = 0f;
        }
    }

    private void SetPosition(int SceneIndex)
    {
        if(CharacterManager.Instance.Controller != null)
        {
            CharacterManager.Instance.Controller.gameObject.transform.position = SpawnPos[SceneIndex];
            CharacterManager.Instance.Controller.gameObject.transform.rotation = Quaternion.Euler(SpawnRot[SceneIndex]);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleTone<GameManager>
{
    #region Variables
    [SerializeField]
    private bool _isPaused = false;

    public bool IsPaused { get { return _isPaused; } set { _isPaused = value; } }

    #endregion

    public bool IsBossFelled = false;


    public bool IsRestart = false;

    private List<Transform> _TargetTransform = new List<Transform>();

    public List<Transform> TargetTransform
    {
        get { return _TargetTransform; }

        set { _TargetTransform = value; }
    }

    public void ClearTargetTranform(Transform[] transform)
    {
        CharacterManager.Instance.ReleaseDeadTarget(transform);

        for (int i = 0; i < transform.Length; i++)
        {
            _TargetTransform.Remove(transform[i]);
        }
    }

    public void ClearTargetList() { _TargetTransform.Clear(); }


    public void RestartScene()
    {
        CharacterManager.Instance.RestartScene();
    }

    public void Return_Title()
    {
        UIManager.Instance.Return_Title();

        CameraManager.Instance.Return_Title();

        CharacterManager.Instance.Return_Title();
    }

    private void Awake()
    {
        if (GameManager.Instance != null && GameManager.Instance != this)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

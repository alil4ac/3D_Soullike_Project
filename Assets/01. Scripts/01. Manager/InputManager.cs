using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingleTone<InputManager>
{
    #region InputValue

    private float _InputX;

    public float InputX
    {
        get
        {
            if(GameManager.Instance.IsPaused == false)
            {
                _InputX = Input.GetAxisRaw("Horizontal");
            }
            else if(GameManager.Instance.IsPaused == true)
            {
                _InputX = 0f;
            }
            else { return 0; }

            return _InputX;
        }
    }

    private float _InputZ;

    public float InputZ
    {
        get
        {
            if (GameManager.Instance.IsPaused == false)
            {
                _InputZ = Input.GetAxisRaw("Vertical");
            }
            else if(GameManager.Instance.IsPaused == true)
            {
                _InputZ = 0f;
            }
            else { return 0; }

            return _InputX;
        }
    }

    #endregion

    private void Awake()
    {
        if (InputManager.Instance != null && InputManager.Instance != this)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}

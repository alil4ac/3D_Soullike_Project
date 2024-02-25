using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screens : MonoBehaviour
{
    int screenwidth = Screen.width;
    int screenheight = Screen.height;
    public bool FullScreen;
    public void SetScreen(int index)
    {
        if(index == 0)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            FullScreen = true;
        }

        if(index == 1)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            FullScreen = false;
        }
    }
    private void Start()
    {
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        FullScreen = true;
    }
    public void ScreenClick(int index)
    {
        if(index == 0)
        {
            Screen.SetResolution(1920, 1080, FullScreen);
        }
        if(index == 1)
        {
            Screen.SetResolution(1600, 900, FullScreen);
        }
        if(index == 3)
        {
            Screen.SetResolution(1280, 720, FullScreen);
        }
    }
}

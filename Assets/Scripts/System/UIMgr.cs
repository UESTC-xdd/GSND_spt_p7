using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : Singleton<UIMgr>
{
    public GameObject[] Controls;

    public UI_BG BG;
    public UI_MovieBG MovieBG;
    public UI_CenterPoint CenterPoint;
    public DialogueController DialogC;

    private void Start()
    {
        GameManager.Instance.OnEnterModeAction -= OnEnterMode;
        GameManager.Instance.OnEnterModeAction += OnEnterMode;
    }

    private void OnEnterMode(GameMode toMode)
    {
        if(toMode == GameMode.MOVIE)
        {
            EnterMovieMode();
        }
        else
        {
            LeaveMovieMode();
        }
    }

    //public void UpdateProgressBar(float value,bool enabled)
    //{
    //    if(enabled)
    //    {
    //        ProgressBar.gameObject.SetActive(true);
    //    }
    //    else
    //    {
    //        ProgressBar.gameObject.SetActive(false);
    //    }
    //    ProgressBar.UpdateValue(value);
    //}

    public void EnterMovieMode()
    {
        MovieBG.ShowBG(1);
        foreach (var control in Controls)
        {
            control.SetActive(false);
        }
    }

    public void LeaveMovieMode()
    {
        MovieBG.HideBG(1);
        foreach (var control in Controls)
        {
            control.SetActive(true);
        }
    }
}

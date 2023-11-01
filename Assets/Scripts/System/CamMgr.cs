using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Rendering;

public class CamMgr : Singleton<CamMgr>
{
    public static Dictionary<string, CinemachineVirtualCamera> CamDic = new Dictionary<string, CinemachineVirtualCamera>();

    public string FirstCamName = "FirstCam";

    public CinemachineVirtualCamera CurActiveCam;

    protected override void Awake()
    {
        base.Awake();
        UpdateCamRef();
    }

    private void Start()
    {
        LevelMgr.Instance.OnSceneLoadedAction -= OnSceneLoaded;
        LevelMgr.Instance.OnSceneLoadedAction += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0)
    {
        UpdateCamRef();
    }

    /// <summary>
    /// �����������
    /// </summary>
    private void UpdateCamRef()
    {
        CamDic.Clear();
        CinemachineVirtualCamera[] Cams = FindObjectsOfType<CinemachineVirtualCamera>();
        foreach (CinemachineVirtualCamera cam in Cams)
        {
            CamDic.Add(cam.gameObject.name, cam);
            if(!string.Equals(cam.gameObject.name, FirstCamName))
            {
                cam.gameObject.SetActive(false);
            }
            else
            {
                CurActiveCam = cam;
            }
        }
    }

    public void SwitchToCam(string CamName)
    {
        if(CamDic.ContainsKey(CamName))
        {
            CinemachineVirtualCamera toCam=CamDic[CamName];
            if(CurActiveCam != null)
            {
                CurActiveCam.gameObject.SetActive(false);
            }
            toCam.gameObject.SetActive(true);
            CurActiveCam = toCam;
        }
        else
        {
            Debug.LogError("�����ڸ��������");
        }
    }
}

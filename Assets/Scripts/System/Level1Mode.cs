using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Level1Mode : LevelSingleton<Level1Mode>
{
    public float RewindTime;

    [Header("Reference")]
    public PlayableDirector m_Director;

    private GameObject CurHideObj;
    private GameObject[] CurNeedToHideBeforeRewindObjs;
    private GameObject[] CurNeedToHideAfterRewindObjs;

    public void OnStartDialog(TimelineAsset timelineAsset,GameObject hidObj, GameObject[] NeedToHideBeforeRewindObjs, GameObject[] NeedToHideAfterRewindObjs)
    {
        CurHideObj = hidObj;
        CurNeedToHideBeforeRewindObjs= NeedToHideBeforeRewindObjs;
        CurNeedToHideAfterRewindObjs = NeedToHideAfterRewindObjs;
        m_Director.playableAsset = timelineAsset;
        StartCoroutine(StartDialogCou());
    }

    public IEnumerator StartDialogCou()
    {
        UIMgr.Instance.BG.FadeIn(2, Color.black);
        yield return new WaitUntil(() => UIMgr.Instance.BG.IsDone);

        foreach (var obj in CurNeedToHideBeforeRewindObjs)
        {
            obj.SetActive(false);
        }

        m_Director.Play();
    }

    public void OnStopDialog()
    {
        UIMgr.Instance.BG.FadeOut(0);
        StartCoroutine(RewindCou());
    }

    public IEnumerator RewindCou()
    {
        yield return new WaitForSeconds(RewindTime);
        UIMgr.Instance.BG.FadeIn(3, Color.black);
        yield return new WaitUntil(() => UIMgr.Instance.BG.IsDone);

        foreach (var gameObj in CurNeedToHideAfterRewindObjs)
        {
            gameObj.SetActive(false);
        }
        foreach (var obj in CurNeedToHideBeforeRewindObjs)
        {
            obj.SetActive(true);
        }

        UIMgr.Instance.BG.FadeOut(0);
    }
}

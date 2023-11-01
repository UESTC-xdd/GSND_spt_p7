using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Level2Mode : MonoBehaviour
{
    [Header("Sixth Task")]
    public Outline PhoneOutline;
    public AudioSource PhoneSource;
    public AudioClip PhoneClip;
    public IInteractable PhoneInteractable;
    public DialogueLine[] PhoneDialog;

    [Header("End")]
    public PlayableDirector Director;

    private void Start()
    {
        SetUpSixthScene();
    }

    public void SetUpSixthScene()
    {
        PhoneInteractable.Interactable = true;
        PhoneOutline.enabled = true;
        PhoneSource.clip = PhoneClip;
        PhoneSource.Play();
    }

    public void PickUpPhone()
    {
        PhoneSource.Stop();
        UIMgr.Instance.DialogC.StartDialogue(PhoneDialog, DialogType.Telephone);
        UIMgr.Instance.DialogC.OnFinishDialog -= SixthTaskFinished;
        UIMgr.Instance.DialogC.OnFinishDialog += SixthTaskFinished;
    }


    public void SixthTaskFinished()
    {
        Debug.Log("Sixth task finished");
        UIMgr.Instance.DialogC.OnFinishDialog -= SixthTaskFinished;
        UIMgr.Instance.CenterPoint.gameObject.SetActive(false);
        StartCoroutine(EndCou());
    }

    private IEnumerator EndCou()
    {
        UIMgr.Instance.BG.FadeIn(2, Color.black);
        yield return new WaitUntil(() => UIMgr.Instance.BG.IsDone);
        UIMgr.Instance.BG.FadeOut(2);
        Director.Play();
        Director.stopped -= OnDirectorStop;
        Director.stopped += OnDirectorStop;
    }

    private void OnDirectorStop(PlayableDirector obj)
    {
        GameManager.Instance.QuitGame();
    }
}

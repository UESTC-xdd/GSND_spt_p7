using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InspectInteractable : IInteractable
{
    public Transform RealTrans;
    public bool IsInspecting { get; set; }

    public DialogueLine DialogLine;
    public DialogType CurDialogType;

    public UnityEvent OnBeginInteract;
    public UnityEvent OnEndInteract;

    public override void OnInteract()
    {
        base.OnInteract();
        Interactable = false;
        GameManager.Instance.PlayerInteractor.InspectObj(RealTrans);
        UIMgr.Instance.DialogC.StartDialogue(DialogLine, CurDialogType);

        OnBeginInteract?.Invoke();

        EventMgr.OnInteract -= OnEBtn;
        EventMgr.OnInteract += OnEBtn;
    }

    private void OnEBtn()
    {
        OnEndInteract?.Invoke();
        UIMgr.Instance.DialogC.StopDialog();
        GameManager.Instance.PlayerInteractor.ReturnInspectObj();
        EventMgr.OnInteract -= OnEBtn;
        Interactable = true;
    }
}

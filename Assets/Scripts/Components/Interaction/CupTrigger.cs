using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class CupTrigger : ITrigger
{
    public bool TriggerAble;
    public bool HasRewindedTime;
    public Outline OutlineComp;
    public TimelineAsset RewindAsset;

    public GameObject[] NeedToHideWhenRewind;
    public GameObject[] NeedToHideObjsAfterRewind;

    protected override void OnPlayerEnterTrigger()
    {
        base.OnPlayerEnterTrigger();
        if(TriggerAble && !HasRewindedTime)
        {
            OutlineComp.enabled = true;

        }
    }

    protected override void OnPlayerExitTrigger()
    {
        base.OnPlayerExitTrigger();
        if(!HasRewindedTime)
        {
            OutlineComp.enabled = false;
        }
    }

    private void Update()
    {
        if(TriggerAble && IsPlayerInTrigger)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if (!HasRewindedTime)
                {
                    Level1Mode.Instance.OnStartDialog(RewindAsset, gameObject, NeedToHideWhenRewind, NeedToHideObjsAfterRewind);
                    HasRewindedTime = true;
                }
            }
        }
    }
}

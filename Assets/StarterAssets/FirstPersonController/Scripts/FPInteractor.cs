using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FPInteractor : MonoBehaviour
{
    public Transform InspectPos;
    public IInteractable CurInteractable;
    public LayerMask InteractorLayer;

    private Transform CurInspectObjTrans;
    private Vector3 CurInspectObjOriginPos;
    private Quaternion CurInspectObjOriginRot;

    private void Update()
    {
        DetectInteractableObj();
    }

    private void DetectInteractableObj()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)),
            out RaycastHit hit, float.MaxValue, InteractorLayer,QueryTriggerInteraction.Ignore))
        {
            //Debug.Log(hit.transform.name);
            if (hit.transform.TryGetComponent(out IInteractable interactable))
            {
                //In Interaction Range
                if(interactable.Interactable && interactable.IsPlayerInRange)
                {
                    if (CurInteractable == null)
                    {
                        CurInteractable = interactable;
                        CurInteractable.CanInteract = true;
                        UIMgr.Instance.CenterPoint.SetHandEnabled(true);
                    }
                    else
                    {
                        if (CurInteractable == interactable)
                        {
                            CurInteractable.CanInteract = true;
                            UIMgr.Instance.CenterPoint.SetHandEnabled(true);
                        }
                        else
                        {
                            CurInteractable.CanInteract = false;
                            CurInteractable = interactable;
                            CurInteractable.CanInteract = true;
                            UIMgr.Instance.CenterPoint.SetHandEnabled(true);
                        }
                    }
                }
                else
                {
                    ClearCurInteractable();
                }
            }
            else
            {
                ClearCurInteractable();
            }
        }
    }

    public void ClearCurInteractable()
    {
        if (CurInteractable != null)
        {
            CurInteractable.CanInteract = false;
            CurInteractable = null;
            UIMgr.Instance.CenterPoint.SetHandEnabled(false);
        }
    }

    public void OnInteract()
    {
        EventMgr.OnInteract?.Invoke();
        if (CurInteractable != null && CurInteractable.CanInteract)
        {
            CurInteractable.OnInteract();
        }
    }

    public void InspectObj(Transform curInspectObjTrans)
    {
        CurInspectObjTrans = curInspectObjTrans;
        CurInspectObjOriginPos = curInspectObjTrans.position;
        CurInspectObjOriginRot = curInspectObjTrans.rotation;

        curInspectObjTrans.SetParent(InspectPos);
        curInspectObjTrans.DOLocalMove(Vector3.zero, 0.5f);
        curInspectObjTrans.DOLocalRotateQuaternion(Quaternion.identity, 0.5f);
    }

    public void ReturnInspectObj()
    {
        if(CurInspectObjTrans!=null)
        {
            CurInspectObjTrans.SetParent(null);
            CurInspectObjTrans.DOMove(CurInspectObjOriginPos, 0.5f);
            CurInspectObjTrans.DORotateQuaternion(CurInspectObjOriginRot, 0.5f);

            CurInspectObjTrans = null;
        }
    }
}

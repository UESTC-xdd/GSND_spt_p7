using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Mode : LevelSingleton<Level1Mode>
{
    [Header("First Task")]
    public Outline CalenderOutline;

    [Header("Second Task")]
    public Outline ToDoListOutline;

    [Header("Third Task")]
    public Outline[] AllCleanOutline;
    public List<IInteractable> AllCleanOutInteractables = new List<IInteractable>();

    [Header("Fourth Task")]
    public GameObject EmptyPlate;
    public Outline EmptyPlateOutline;
    public IInteractable EmptyPlateInteractable;
    public float WaitBakeTime;
    public GameObject FoodPlate; 
    public AudioClip BakeClip;

    [Header("Fifth Task")]
    public Outline BedOutline;
    public BoxCollider BedTrigger;

    private void Start()
    {
        SetUpFirstTask();
    }

    public void ChangeToGameMode()
    {
        GameManager.Instance.ChangeToGameMode();
    }

    public void SetUpFirstTask()
    {
        CalenderOutline.enabled = true;
    }

    public void FinishFirstTask()
    {
        Debug.Log("Fist Task Finished");
        SetUpSecondTask();
    }
        

    public void SetUpSecondTask()
    {
        ToDoListOutline.enabled = true;
    }

    public void FinishSecondTask()
    {
        Debug.Log("Second Task Finished");
        SetUpThirdTask();
    }

    public void SetUpThirdTask()
    {
        foreach (var outline in AllCleanOutline)
        {
            outline.enabled = true;
        }

        foreach (var interactable in AllCleanOutInteractables)
        {
            interactable.Interactable = true;
        }
    }

    public void FinishedOneClean(IInteractable thisInteractable)
    {
        if(AllCleanOutInteractables.Contains(thisInteractable))
        {
            AllCleanOutInteractables.Remove(thisInteractable);
            if(AllCleanOutInteractables.Count<=0)
            {
                FinishThirdTask();
            }
        }
        thisInteractable.gameObject.SetActive(false);
    }

    public void FinishThirdTask()
    {
        Debug.Log("Third Task Finished");
        SetUpFourthTask();
    }

    public void SetUpFourthTask()
    {
        EmptyPlateOutline.enabled = true;
        EmptyPlateInteractable.Interactable = true;
    }

    public void BakeTheFood()
    {
        UIMgr.Instance.BG.FadeIn(1, Color.black);
        AudioMgr.Instance.PlayOneShot2DSE(BakeClip);
        StartCoroutine(WaitForBakeCou());
    }

    private IEnumerator WaitForBakeCou()
    {
        yield return new WaitForSeconds(WaitBakeTime);
        EmptyPlate.SetActive(false);
        FoodPlate.SetActive(true);
        UIMgr.Instance.BG.FadeOut(1);
        yield return new WaitUntil(() => UIMgr.Instance.BG.IsDone);
        FinishFourthTask();
    }

    public void FinishFourthTask()
    {
        Debug.Log("Fourth Task Finished");
        SetUpFifthTask();
    }

    public void SetUpFifthTask()
    {
        BedOutline.enabled = true;
        BedTrigger.enabled = true;
    }

    public void FinishFifthTask()
    {
        BedOutline.enabled = false;
        Debug.Log("Fifth Task Finished");
        LevelMgr.Instance.LoadNextLevel();
    }
}

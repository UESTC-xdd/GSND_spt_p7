using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UI_MovieBG : MonoBehaviour
{
    public RectTransform BGUp;
    public RectTransform BGDown;
    public Transform inventoryReminder;

    [SerializeField]
    private bool IsShowingBG;

    public bool IsDone = true;


    private void Start()
    {
       // ShowBG(2f);
    }
    public void ShowBG(float time)
    {
        if(!IsShowingBG)
        {
            //Debug.Log("ПЊ");
            gameObject.SetActive(true);

            BGUp.anchoredPosition = new Vector2(0, 64.5f);
            BGDown.anchoredPosition = new Vector2(0, -64.5f);

            BGUp.DOAnchorPosY(-64.5f, time);
            BGDown.DOAnchorPosY(64.5f, time);

            IsShowingBG = true;

            if(inventoryReminder)
                inventoryReminder.gameObject.SetActive(false);
        }
    }

    public void HideBG(float time)
    {
        if(IsShowingBG)
        {
            //Debug.Log("Ви");
            StartCoroutine(HideBGCou(time));
        }
    }

    private IEnumerator HideBGCou(float time)
    {
        Tween tween1 = BGUp.DOAnchorPosY(64.5f, time);
        Tween tween2 = BGDown.DOAnchorPosY(-64.5f, time);
        yield return tween1.WaitForCompletion();
        yield return tween2.WaitForCompletion();
        IsShowingBG = false;
        gameObject.SetActive(false);

        if (inventoryReminder)
            inventoryReminder.gameObject.SetActive(true);
    }
}

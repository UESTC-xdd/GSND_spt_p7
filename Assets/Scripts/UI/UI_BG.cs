using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_BG : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup m_Group;

    [SerializeField]
    private Image m_Image;

    public bool IsDone = true;

    private void Awake()
    {
        if(m_Group == null)
        {
            m_Group = GetComponent<CanvasGroup>();
        }

        if(m_Image == null)
        {
            m_Image = GetComponent<Image>();
        }

        gameObject.SetActive(false);
    }

    public void FadeIn(float time,Color bgColor)
    {
        m_Image.color = bgColor;
        gameObject.SetActive(true);
        StartCoroutine(FadeInCou(time));
    }

    IEnumerator FadeInCou(float time)
    {
        IsDone = false;
        m_Group.alpha = 0;
        Tween inTween = m_Group.DOFade(1, time);
        yield return inTween.WaitForCompletion();
        IsDone = true;
    }

    public void FadeOut(float time)
    {
        StartCoroutine(FadeOutCou(time));
    }

    IEnumerator FadeOutCou(float time)
    {
        IsDone = false;
        m_Group.alpha = 1;
        Tween inTween = m_Group.DOFade(0, time);
        yield return inTween.WaitForCompletion();
        gameObject.SetActive(false);
        IsDone = true;
    }
}

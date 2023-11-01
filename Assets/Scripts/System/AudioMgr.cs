using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioMgr : Singleton<AudioMgr>
{
    [Header("BGM")]
    public AudioClip MainMenuBGM;
    public AudioClip SE_Left;
    public AudioClip SE_Right;
    public AudioClip[] LoopBGM;

    [Header("Source")]
    public AudioSource BGMSource;
    public AudioSource SESource_2D;

    //����ѭ��BGM
    private int LoopBGMIndex = 0;
    private bool IsPlayingLoopBGM;
    private AudioClip[] CurPlayingClips;

    /// <summary>
    /// ֹͣ��ǰBGM������ָ��BGM��BGM���ظ����ţ�
    /// </summary>
    /// <param name="clip"></param>
    public void PlayBGM(AudioClip clip,bool isFade)
    {
        BGMSource.loop = true;
        if (BGMSource.isPlaying)
        {
            if(isFade)
            {
                CrossFadeClip(BGMSource, clip);
            }
            else
            {
                BGMSource.Stop();
                BGMSource.clip = clip;
                BGMSource.Play();
            }
        }
        else
        {
            BGMSource.clip = clip;
            BGMSource.Play();
        }
    }

    /// <summary>
    /// ����ѭ��BGM
    /// </summary>
    /// <param name="clips"></param>
    public void PlayLoopBGM(AudioClip[] clips)
    {
        if(CurPlayingClips ==null || CurPlayingClips!=clips)
        {
            LoopBGMIndex = 0;
            BGMSource.loop = false;
            BGMSource.clip = clips[LoopBGMIndex];
            BGMSource.Play();
            IsPlayingLoopBGM = true;
            CurPlayingClips = clips;
        }
        else if(CurPlayingClips != null && CurPlayingClips == clips)
        {
            BGMSource.Play();
            IsPlayingLoopBGM = true;
        }
    }

    /// <summary>
    /// ֹͣBGM����
    /// </summary>
    public void StopBGM()
    {
        if (IsPlayingLoopBGM)
            IsPlayingLoopBGM = false;

        BGMSource.Stop();
    }

    private void UsedForLoopBGM()
    {
        if(!BGMSource.isPlaying)
        {
            if(LoopBGMIndex<LoopBGM.Length-1)
            {
                LoopBGMIndex++;
            }
            else
            {
                LoopBGMIndex = 0;
            }
            BGMSource.clip = LoopBGM[LoopBGMIndex];
            BGMSource.Play();
        }
    }

    private void Update()
    {
        //ѭ��BGM
        if(IsPlayingLoopBGM)
        {
            UsedForLoopBGM();
        }
    }

    /// <summary>
    /// ��������BGM
    /// </summary>
    public void FadeStartBGM(AudioClip tarClip)
    {
        BGMSource.volume = 0;
        BGMSource.clip = tarClip;
        BGMSource.Play();
        BGMSource.DOFade(1, 1);
    }

    /// <summary>
    /// ����ֹͣBGM
    /// </summary>
    public void FadeStopBGM()
    {
        StartCoroutine(FadeStopCo(BGMSource));
    }

    private IEnumerator FadeStopCo(AudioSource source)
    {
        Tween downTween = source.DOFade(0, 1);
        yield return downTween.WaitForCompletion();
        source.Stop();
    }

    /// <summary>
    /// 0-100����������
    /// </summary>
    /// <param name="volume"></param>
    public void SetTotalVolume(int volume)
    {
        if(volume>=0 && volume<=100)
        {
            float targetVol = volume / 100f;
            AudioListener.volume = targetVol;
        }
    }

    /// <summary>
    /// ���ڽ�����ɵ���һ��Clip
    /// </summary>
    public void CrossFadeClip(AudioSource source,AudioClip tarClip)
    {
        StartCoroutine(CrossFadeClipCo(source, tarClip));
    }

    private IEnumerator CrossFadeClipCo(AudioSource source, AudioClip tarClip)
    {
        Tween downTween = source.DOFade(0, 1);
        yield return downTween.WaitForCompletion();
        source.Stop();
        source.clip = tarClip;
        source.Play();
        source.DOFade(1, 1);
    }

    /// <summary>
    /// ����һ��2D��Ч
    /// </summary>
    public void PlayOneShot2DSE(AudioClip clip)
    {
        SESource_2D.PlayOneShot(clip, 1);
    }

    public void Stop2DSE()
    {
        SESource_2D.Stop();
    }

    /// <summary>
    /// ָ��λ�ò���һ��3D��Ч
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="pos"></param>
    public void PlayOneShot3DSE(AudioClip clip, Vector2 pos)
    {
        AudioCue newCue = PoolManager.Instance.GetFromPool("AudioCue", new Vector3(pos.x, pos.y, 0)).GetComponent<AudioCue>();
        newCue.PlayOneShot3DSE(clip);
    }

    /// <summary>
    /// ָ��λ�ò���һ��3D��Ч
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="pos"></param>
    public void PlayOneShot3DSE(AudioClip clip,Vector3 pos)
    {
        AudioCue newCue = PoolManager.Instance.GetFromPool("AudioCue", pos).GetComponent<AudioCue>();
        newCue.PlayOneShot3DSE(clip);
    }

    /// <summary>
    /// ����һ�����Clip
    /// </summary>
    /// <param name="source"></param>
    /// <param name="clips"></param>
    public void PlayRandomClip(AudioSource source, AudioClip[] clips)
    {
        source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }


}

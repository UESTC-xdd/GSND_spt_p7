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

    //用于循环BGM
    private int LoopBGMIndex = 0;
    private bool IsPlayingLoopBGM;
    private AudioClip[] CurPlayingClips;

    /// <summary>
    /// 停止当前BGM，播放指定BGM（BGM会重复播放）
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
    /// 播放循环BGM
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
    /// 停止BGM播放
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
        //循环BGM
        if(IsPlayingLoopBGM)
        {
            UsedForLoopBGM();
        }
    }

    /// <summary>
    /// 渐变启动BGM
    /// </summary>
    public void FadeStartBGM(AudioClip tarClip)
    {
        BGMSource.volume = 0;
        BGMSource.clip = tarClip;
        BGMSource.Play();
        BGMSource.DOFade(1, 1);
    }

    /// <summary>
    /// 渐变停止BGM
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
    /// 0-100设置总音量
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
    /// 用于渐变过渡到下一个Clip
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
    /// 播放一次2D音效
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
    /// 指定位置播放一次3D音效
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="pos"></param>
    public void PlayOneShot3DSE(AudioClip clip, Vector2 pos)
    {
        AudioCue newCue = PoolManager.Instance.GetFromPool("AudioCue", new Vector3(pos.x, pos.y, 0)).GetComponent<AudioCue>();
        newCue.PlayOneShot3DSE(clip);
    }

    /// <summary>
    /// 指定位置播放一次3D音效
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="pos"></param>
    public void PlayOneShot3DSE(AudioClip clip,Vector3 pos)
    {
        AudioCue newCue = PoolManager.Instance.GetFromPool("AudioCue", pos).GetComponent<AudioCue>();
        newCue.PlayOneShot3DSE(clip);
    }

    /// <summary>
    /// 播放一个随机Clip
    /// </summary>
    /// <param name="source"></param>
    /// <param name="clips"></param>
    public void PlayRandomClip(AudioSource source, AudioClip[] clips)
    {
        source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }


}

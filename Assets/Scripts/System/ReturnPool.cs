using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPool : MonoBehaviour
{
    public float existTime;
    public string objPoolTag;

    public enum ObjType
    {   
        ParticleSystem,
        Audio,
        WaitExistTime,
        ManualControl
    }

    public ObjType type;

    private ParticleSystem _particle;
    private AudioSource _source;
    private bool _notFirstSpawn;

    private void Awake()
    {
        switch (type)
        {
            case ObjType.ParticleSystem:
                {
                    if (!TryGetComponent(out _particle))
                    {
                        Debug.LogError("The type of this object pool is Particle System. Make sure that there is a corresponding component on the certain object.");
                    }
                    break;
                }
            case ObjType.Audio:
                {
                    if(!TryGetComponent(out _source))
                    {
                        Debug.LogError("The type of this object pool is Audio System. Make sure that there is a corresponding component on the certain object.");
                    }
                    break;
                }
            case ObjType.WaitExistTime:
                break;
            case ObjType.ManualControl:
                break;
            default:
                break;
        }
    }

    private void OnEnable()
    {
        if(!_notFirstSpawn)
        {
            _notFirstSpawn = true;
            return;
        }

        switch (type)
        {
            case ObjType.ParticleSystem:
                {
                    _particle.Play();
                    StartCoroutine(ReturnPoolParticle());
                    break;
                }
            case ObjType.Audio:
                {
                    break;
                }
            case ObjType.WaitExistTime:
                {
                    StartCoroutine(ReturnPoolExistTime());
                    break;
                }
            default:
                break;
        }
    }

    public void SetCountReturn(float time)
    {
        existTime = time;
        StartCoroutine(ReturnPoolExistTime());
    }

    public IEnumerator ReturnPoolAudio()
    {
        yield return new WaitUntil(() => !_source.isPlaying);
        PoolManager.Instance.ReturnPool(gameObject, objPoolTag);
    }

    IEnumerator ReturnPoolParticle()
    {
        yield return new WaitUntil(() => _particle.isStopped);
        PoolManager.Instance.ReturnPool(gameObject, objPoolTag);
    }

    IEnumerator ReturnPoolExistTime()
    {
        yield return new WaitForSeconds(existTime);
        PoolManager.Instance.ReturnPool(gameObject, objPoolTag);
    }

    /// <summary>
    /// 回对象池
    /// </summary>
    public void ReturnSelf()
    {
        StopAllCoroutines();
        PoolManager.Instance.ReturnPool(gameObject, objPoolTag);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCue : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_AudioSource;
    [SerializeField]
    private ReturnPool m_ReturnPool;

    private void Awake()
    {
        if(m_AudioSource == null)
        {
            m_AudioSource = GetComponent<AudioSource>();
        }

        if(m_ReturnPool == null)
        { 
            m_ReturnPool = GetComponent<ReturnPool>();
        }
    }

    public void PlayOneShot3DSE(AudioClip clip)
    {
        m_AudioSource.clip = clip;
        m_AudioSource.Play();
        StartCoroutine(m_ReturnPool.ReturnPoolAudio());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSingleton<T> : MonoBehaviour where T: LevelSingleton<T>
{
    private static T m_Instance;
    public static T Instance
    {
        get
        {
            return m_Instance;
        }
    }

    public static bool IsValid => m_Instance != null;

    protected virtual void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = (T)this;
        }
        else
        {
            if (m_Instance != (T)this)
            {
                Destroy(gameObject);
                return;
            }
        }
    }
}

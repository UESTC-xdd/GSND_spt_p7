using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{
    void RegisterSavable()
    {
        SaveMgr.Instance.Register(this);
    }

    /// <summary>
    /// ���ɴ洢��������
    /// </summary>
    /// <returns></returns>
    void GenerateSaveData();

    /// <summary>
    /// �뵱ǰ�洢����ͬ��
    /// </summary>
    void RestoreSaveData(SaveData data);
}

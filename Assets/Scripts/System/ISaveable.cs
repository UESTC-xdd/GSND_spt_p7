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
    /// 生成存储数据类型
    /// </summary>
    /// <returns></returns>
    void GenerateSaveData();

    /// <summary>
    /// 与当前存储数据同步
    /// </summary>
    void RestoreSaveData(SaveData data);
}

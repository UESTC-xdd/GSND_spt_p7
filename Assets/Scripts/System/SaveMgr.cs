using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Newtonsoft.Json;
using System.IO;

public class SaveMgr : Singleton<SaveMgr>
{
    public SaveData CurSaveData = new SaveData();

    private static string SavePath;

    private List<ISaveable> m_CurSaveables=new List<ISaveable>();

    protected override void Awake()
    {
        base.Awake();
        SavePath = Application.persistentDataPath + "/SAVE/";
    }

    public void Register(ISaveable saveable)
    {
        m_CurSaveables.Add(saveable);
    }

    public void Save(int index)
    {
        string path = SavePath + "data" + index + ".sav";

        //ͬ������
        foreach (ISaveable saveable in m_CurSaveables)
        {
            saveable.GenerateSaveData();
        }

        //string jsonData=JsonConvert.SerializeObject(CurSaveData, Formatting.Indented);

        //if(!Directory.Exists(SavePath))
        //{
        //    Directory.CreateDirectory(SavePath);
        //}

        //if (File.Exists(path))
        //{
        //    File.Delete(path);
        //}

        //File.WriteAllText(path, jsonData);
    }

    public void Load(int index)
    {
        string path = SavePath + "data" + index + ".sav";
        if (File.Exists(path))
        {
            string jsonData=File.ReadAllText(path);
            //CurSaveData = JsonConvert.DeserializeObject<SaveData>(jsonData);
            //foreach (ISaveable saveable in m_CurSaveables)
            //{
            //    saveable.RestoreSaveData(CurSaveData);
            //}
        }
        else
        {
            Debug.LogWarning("�����ڸô浵�ļ�");
            Save(index);
            Load(index);
            return;
        }
    }

    [ContextMenu("��")]
    public void TestSave()
    {
        Save(0);
    }

    [ContextMenu("ȡ")]
    public void TestLoad()
    {
        Load(0);
    }

    private void OnApplicationQuit()
    {
        Debug.Log("�˳�");
    }

    private void OnApplicationPause(bool pause)
    {
        Debug.Log("��ͣ");
    }
}

public class SaveData
{
    public int Index = 0;
    public List<string> Inventory = new List<string>();
}

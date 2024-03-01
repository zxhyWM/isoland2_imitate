using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

using System.IO;
using Newtonsoft.Json;

public class SaveLoadManager : Singleton<SaveLoadManager>
{
    private string _jsonFolder;

    private List<ISaveable> saveableList = new List<ISaveable>();
    private Dictionary<string, GameSaveData> saveDataDict = new Dictionary<string, GameSaveData>();

    protected override void Awake()
    {
        base.Awake();
        _jsonFolder = Application.persistentDataPath + "/SAVE/";
    }

    private void OnEnable()
    {
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }

    private void OnDisable()
    {
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
    }

    private void OnStartNewGameEvent(int obj)
    {
        var resultPath = _jsonFolder + "data.sav";
        if (File.Exists(resultPath))
        {
            File.Delete(resultPath);
            
        }
    }

    public void Register(ISaveable saveable)
    {
        saveableList.Add(saveable);
    }

    public void Save()
    {
        saveDataDict.Clear();
        foreach (var saveable in saveableList)
        {
            saveDataDict.Add(saveable.GetType().Name,saveable.GenerateSaveData());
        }
        
        var resultPath = _jsonFolder + "data.sav";
        var jsonData = JsonConvert.SerializeObject(saveDataDict, Formatting.Indented);
        if (!File.Exists(resultPath))
        {
            Directory.CreateDirectory(_jsonFolder);
        }

        File.WriteAllText(resultPath,jsonData);
    }

    public void Load()
    {
        var resultPath = _jsonFolder + "data.sav";
        if(!File.Exists(resultPath))    return;

        var stringData = File.ReadAllText(resultPath);
        var jsonData = JsonConvert.DeserializeObject<Dictionary<string,GameSaveData>>(stringData);
        foreach (var saveable in saveableList)
        {
            saveable.RestoreGameData(jsonData[saveable.GetType().Name]);
            
        }
    }
}

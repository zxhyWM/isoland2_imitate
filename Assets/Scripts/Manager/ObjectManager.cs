using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectManager : MonoBehaviour,ISaveable
{
    private Dictionary<ItemName, bool> itemAvaliableDic = new Dictionary<ItemName, bool>();

    private Dictionary<string, bool> interactiveStateDict = new Dictionary<string, bool>();
    
    //注册事件
    public void OnEnable()
    {
        EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadEvent += OnAfterSceneLoadEvent;
        EventHandler.UpdateUIEvent += OnUpdateUIEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }

    private void OnDisable()
    {
        EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadEvent -= OnAfterSceneLoadEvent;
        EventHandler.UpdateUIEvent -= OnUpdateUIEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
    }

    private void Start()
    {
        ISaveable saveable = this;
        saveable.SaveableRegister();
    }

    private void OnStartNewGameEvent(int obj)
    {
        itemAvaliableDic.Clear();
        interactiveStateDict.Clear();
    }

    private void OnBeforeSceneUnloadEvent()
    {
        foreach (var item in FindObjectsOfType<Item>())
        {
            if (!itemAvaliableDic.ContainsKey(item.itemName))
            {
                itemAvaliableDic.Add(item.itemName,true);
            }
        }

        foreach (var item in FindObjectsOfType<Interactive>())
        {
            if (interactiveStateDict.ContainsKey(item.name))
                interactiveStateDict[item.name] = item.isDone;
            else
                interactiveStateDict.Add(item.name,item.isDone);
        }
    }

    private void OnAfterSceneLoadEvent() 
    {
        foreach (var item in FindObjectsOfType<Item>())
        {
            if(!itemAvaliableDic.ContainsKey(item.itemName))
                itemAvaliableDic.Add(item.itemName,true);
            else
                item.gameObject.SetActive(itemAvaliableDic[item.itemName]);
        }
        
        foreach (var item in FindObjectsOfType<Interactive>())
        {
            if (interactiveStateDict.ContainsKey(item.name))
                item.isDone = interactiveStateDict[item.name];
            else
                interactiveStateDict.Add(item.name,item.isDone);
        }
    }

    private void OnUpdateUIEvent(ItemDetails itemDetails,int a)
    {
        if (itemDetails!=null)
        {
            itemAvaliableDic[itemDetails.itemName] = false;
        }
    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.itemAvaliableDic = this.itemAvaliableDic;
        saveData.interactiveStateDict = this.interactiveStateDict;

        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        this.itemAvaliableDic = saveData.itemAvaliableDic;
        this.interactiveStateDict = saveData.interactiveStateDict;
    }
}

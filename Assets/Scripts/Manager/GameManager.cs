using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour,ISaveable
{
    private Dictionary<string, bool> miniGameStateDict = new Dictionary<string, bool>();

    private GameController currentGame;
    private int gameWeek;
    private ISaveable _saveableImplementation;

    private void OnEnable()
    {
        EventHandler.AfterSceneLoadEvent += OnAfterSceneLoadEvent;
        EventHandler.GamePassEvent += OnGamePassEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }

    

    private void OnDisable()
    {
        EventHandler.AfterSceneLoadEvent -= OnAfterSceneLoadEvent;
        EventHandler.GamePassEvent -= OnGamePassEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
    }

    private void OnStartNewGameEvent(int gameWeek)
    {
        this.gameWeek = gameWeek;
        miniGameStateDict.Clear();
    }


    void Start()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        EventHandler.CallGameStateChangeEvent(GameState.GamePlay);
        
        //保存数据
        ISaveable saveable = this;
        saveable.SaveableRegister();
    }
    
    private void OnAfterSceneLoadEvent()
    {
        foreach (var miniGame in FindObjectsOfType<MiniGame>())
        {
            if (miniGameStateDict.TryGetValue(miniGame.gameName, out bool isPass))
            {
                miniGame.isPass = isPass;
                miniGame.UpdateMiniGameState();
            }
        }

        currentGame = FindObjectOfType<GameController>();
        currentGame?.SetGameWeekData(gameWeek);
    }
    
    private void OnGamePassEvent(string gameName)
    {
        miniGameStateDict[gameName] = true;
    }


    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.gameWeek = this.gameWeek;
        saveData.miniGameStateDict = this.miniGameStateDict;
        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        this.gameWeek = saveData.gameWeek;
        this.miniGameStateDict = saveData.miniGameStateDict;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    //跳转逻辑
    //两个变量：当前场景、下一场景
    //[SceneName]是mtool自动枚举所有挂载到build settings里的场景集合，单例模式
    [SceneName] public string currentScene,nextScene;
    
    //点击事件
    public void TeleportToScene()
    {
        TransitionManager.Instance.Transition(currentScene,nextScene);
    }
    
    
}

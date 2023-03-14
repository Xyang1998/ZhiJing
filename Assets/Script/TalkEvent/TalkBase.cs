using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using Fungus.EditorUtils;
using UnityEngine;

[Serializable]
public class TalkBase : NPC
{
    public string Name;
    public int Favorability = 0;
    public Flowchart flowchart;
    public string talkmessage;
    public string DailogPath; //对话文本
    public int StartID; //对话开始ID

    public void Awake()
    {
        Load();
        SystemMediator.GetSystemMediator().GetEventSystem().saveaction += Save;
    }
    public virtual void StartTalk()
    {
        flowchart.ExecuteBlock("Start");
    }

    public virtual bool PlayerItemsCheck(List<int> list) //检查玩家是否有物品
    {
        return PlayerController.GetPlayerController().ItemsCheck(list);
    }

    public virtual void PlayerItemsUse(List<int> list) //检查玩家是否有并使用物品
    {
        PlayerController.GetPlayerController().ItemsUse(list);
    }

    public void Load() //读取，下次对话ID，位置，好感度等
    {
        
    }

    public void Save() //保存
    {
        
    }
    
    
}

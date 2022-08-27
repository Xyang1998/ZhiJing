using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using Fungus.EditorUtils;
using UnityEngine;

public class TalkBase : MonoBehaviour
{
    public Flowchart flowchart;
    public string talkmessage;
    public virtual void StartTalk()
    {
        flowchart.ExecuteBlock("Start");
    }

    public virtual bool PlayerItemsCheck(List<int> list)
    {
        return PlayerController.GetPlayerController().ItemsCheck(list);
    }

    public virtual void PlayerItemsUse(List<int> list)
    {
        PlayerController.GetPlayerController().ItemsUse(list);
    }
    
}

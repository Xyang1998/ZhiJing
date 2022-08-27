using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
[CommandInfo("FungusEX","IntelligenceCheck","判断智慧值修改flowchart中bool变量")]
public class IntelligenceCheck : Command
{
    public Flowchart flowchart;
    [Tooltip("智慧值大于等于value时将var设为true")]
    public float value;
    [Tooltip("flowchart中if变量")]
    public string var;

    public override void OnEnter()
    {
        if (PlayerController.GetPlayerController().IntelligenceChcek(value))
        {
            if (var != null)
            {
                flowchart.SetBooleanVariable(var,true);
            }
        }
        Continue();
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("FungusEX","RenYiCheck","判断仁义值修改flowchart中bool变量")]
public class RenYiCheck : Command
{
    public Flowchart flowchart;
    [Tooltip("仁义值大于等于value时将var设为true")]
    public float value;
    [Tooltip("flowchart中if变量")]
    public string var;

    public override void OnEnter()
    {
      
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Fungus;
[CommandInfo("FungusEX","IntelligenceCheck","判断养晦值修改flowchart中bool变量")]
public class YangHui : Command
{
    public Flowchart flowchart;
    [Tooltip("养晦值大于等于value时将var设为true")]
    public float value;
    [Tooltip("flowchart中if变量")]
    public string var;

    public override void OnEnter()
    {
       
    }
}

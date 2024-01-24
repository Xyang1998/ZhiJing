using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("FungusEX","ConditionCheck","判断物品修改flowchart中bool变量")]
public class ConditionCheck : Command
{
   
   public override void OnEnter()
   {
      base.OnEnter();
      Continue();
   }
}

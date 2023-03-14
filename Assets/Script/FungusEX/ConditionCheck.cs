using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("FungusEX","ConditionCheck","判断物品修改flowchart中bool变量")]
public class ConditionCheck : Command
{
   public Flowchart flowchart;
   [Tooltip("物品列表")]
   public List<int> list;
   [Tooltip("flowchart中if变量")]
   public string var;

   public override void OnEnter()
   {
      if (PlayerController.GetPlayerController().ItemsCheck(list))
      {
         if (var != null)
         {
            flowchart.SetBooleanVariable(var,true);
         }
      }
      Continue();
   }
}

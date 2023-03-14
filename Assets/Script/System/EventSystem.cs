using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EventSystem : ISystem
{
   public UnityAction startaction; //游戏开始时调用，用于读取初始对话等等?
   public UnityAction saveaction;//保存游戏时调用,绑定每个需要保存物体的Save函数

   public override void Init()
   {
      startaction = new UnityAction(StartAction);
      saveaction = new UnityAction(SaveAction);
   }

   private void StartAction()
   {
      
   }

   private void SaveAction()
   {
      
   }

}

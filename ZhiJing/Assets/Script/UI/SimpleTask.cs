using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimpleTask : MonoBehaviour,IPointerClickHandler

{
   public int taskID;
   public Text Title;
   public Text description;
   private BaseTask _baseTask;

   public void SetTask(BaseTask task)
   {
      _baseTask = task;
      taskID = task.ID;
      Title.text = _baseTask.title;
      description.text = _baseTask.description;
   }

   public void OnPointerClick(PointerEventData pointerEventData)
   {
      SystemMediator.GetSystemMediator().GetUISystem().detailedTask.GetComponent<DetailedTask>().SetTask(_baseTask);
   }
   
}

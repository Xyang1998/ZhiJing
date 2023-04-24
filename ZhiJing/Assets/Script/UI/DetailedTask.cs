using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailedTask : MonoBehaviour
{
    public BaseTask task;
    public Text detailedText;

    public void SetTask(BaseTask task_)
    {
        task = task_;
        detailedText.text = GetText();

    }

    public string GetText()
    {
        string text = "";
        text += "<b>" + task.title + "</b>"+"\n";
        text += task.description + "\n";
        text+="<b>任务目标</b>"+"\n";
        foreach (var objective in task.taskObjectives.collectiveObjectives)
        {
            
        }

        foreach (var objective in task.taskObjectives.talkObjectives)
        {
            text += "与" + TaskSystem.GetTaskSystem().GetNPCNameByID(objective.NPCID) +
                    String.Format("交谈 [{0}/{1}]", objective.curAmount, objective.amount)+"\n";
        }
        text+="<b>任务奖励</b>"+"\n";
        return text;

    }

    public void ClearText()
    {
        detailedText.text="<b>选择一个任务查看详细</b>"+"\n";
    }

}

using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("FungusEX","GivePlayerTask","接受任务")]
public class GivePlayerTask : Command
{
    private string TaskID;
    private BaseTask task;

    public void Init(string id)
    {
        TaskID = id;
        task = Resources.Load<BaseTask>("Tasks/Task_"+TaskID);
    }
    public override void OnEnter()
    {
        base.OnEnter();
        if (!task)
        {
            Debug.Log("没有任务？");
        }
        StartCoroutine(TaskSystem.GetTaskSystem().AcceptTask(task));
        Continue();
    }
}

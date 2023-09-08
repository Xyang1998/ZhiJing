using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Object = System.Object;


[System.Serializable]
[CreateAssetMenu(fileName = "quest", menuName = "Task/NewTask")]
public class BaseTask : ScriptableObject
{
    public int ID;
    
    public string title;
    
    public string description;

    [Tooltip("任务接受条件")]
    public QuestAcceptCondition acceptCondition;
    
    [Tooltip("任务奖励")]
    public QuestReward questReward;

    [Tooltip("任务目标")]
    public TaskObjectives taskObjectives;

    public bool IsComplete()
    {
        FieldInfo[] fieldInfos = taskObjectives.GetType().GetFields();
        foreach (var item in fieldInfos)
        {
            Objective[] objectives = (Objective[])item.GetValue(taskObjectives);
            foreach (Objective objective in objectives)
            {
                if (!objective.isComplete)
                {
                    return false;
                }
            }
            
        }
        return true;
    }

    public void Init()//记得创建或读取任务后调用
    {
        Bind();
    }

    private void Bind() 
    {
        TaskSystem taskSystem = SystemMediator.Instance.taskSystem;
        foreach (CollectiveObjective collectiveObjective in taskObjectives.collectiveObjectives)
        {
            taskSystem.addItemAction += collectiveObjective.AddItemAmount;
            taskSystem.removeItemAction += collectiveObjective.RemoveItemAmount;
            //collectiveObjective.UpdateItemAmount

        }
        foreach (TalkObjective talkObjective in taskObjectives.talkObjectives)
        {
            taskSystem.talkToAction += talkObjective.PlayerTalkTo;
            //collectiveObjective.UpdateItemAmount

        }
    }

    public void UnBind() //任务完成后调用
    {
        TaskSystem taskSystem = SystemMediator.Instance.taskSystem;
        foreach (CollectiveObjective collectiveObjective in taskObjectives.collectiveObjectives)
        {
            taskSystem.addItemAction -= collectiveObjective.AddItemAmount;
            taskSystem.removeItemAction -= collectiveObjective.RemoveItemAmount;
            //collectiveObjective.UpdateItemAmount

        }
        foreach (TalkObjective talkObjective in taskObjectives.talkObjectives)
        {
            taskSystem.talkToAction -= talkObjective.PlayerTalkTo;
            //collectiveObjective.UpdateItemAmount

        }
    }


}

[Serializable]
public class Objective
{
    [Tooltip("当前数量")]
    public int curAmount; 
    
    [Tooltip("任务完成所需数量")]
    public int amount=1; 

    public bool isComplete //是否完成目标
    {
        get
        {
            if (curAmount >= amount) return true;
            return false;
        }
    }
}

[Serializable]
public class CollectiveObjective :Objective //收集类的目标
{
    public int ItemID; //需要收集物品的ID

    public void AddItemAmount(int id,int num)//获得物品时调用
    {
        if (id == ItemID)
        {
            curAmount += num;
        }
        
    }

    public void RemoveItemAmount(int id, int num) //丢弃物品时调用
    {
        if (id == ItemID)
        {
            curAmount -= num;
        }
    }

    public void UpdateItemAmount() //检查背包中物品，何时使用？(接任务)
    {
        //continue
    }
}

[Serializable]
public class TalkObjective : Objective //交谈类目标
{
    public int NPCID; //需要交谈的NPC
    public int DialID; //对话ID(真的需要每次对话都判断吗？)

    public void PlayerTalkTo(int npcid,int dialid) //对话时调用
    {
        if (npcid == NPCID && dialid == DialID)
        {
            curAmount += 1;
        }
    }
}

[Serializable]
public class OthersObjective : Objective
{
    public int EventID;

    public void update(int id )
    {
        if (id == EventID) curAmount += 1;
    }
}


[Serializable]
public class TaskObjectives //任务目标
{
    public CollectiveObjective[] collectiveObjectives;
    public TalkObjective[] talkObjectives;
    public OthersObjective[] othersObjectives;
}
public enum PlayerValue
{
    Intelligence,
    RenYi,
    YangHui
}

public enum NPCValue
{
    a,
    b
}

public enum Condition
{
    [Tooltip(">=")]
    More,
    [Tooltip("<=")]
    Less,
    [Tooltip("==")]
    Equal
}

[Serializable]
public struct PlayerValueCondition
{
    public PlayerValue value;

    public Condition condition;
    
    public float reqvalue;


}

[Serializable]
public struct NPCValueCondition
{
    public int NPCID;
    
    public NPCValue value;

    public Condition condition;
    
    public float reqvalue;


}

[Serializable]
public struct ItemCondition
{
    public int ItemID;
    public int num;
}

[Serializable]
public class QuestAcceptCondition
{
    public bool HasCondition;//判断是否需要条件，为False时无视条件接受任务
    
    public List<int> preTaskList; //前置任务

    [SerializeField]
    public PlayerValueCondition[] playerValueConditions;

    [SerializeField]
    public NPCValueCondition[] NpcValueConditions;

    [SerializeField] 
    public ItemCondition[] itemConditions;

}

[Serializable]
public struct PlayerValueReward
{
    public PlayerValue playerValue;
    public float rewardValue;
}
[Serializable]
public struct NPCValueReward
{
    public int NPCID;
    public NPCValue npcValue;
    public float rewardValue;
}

[Serializable]
public struct ItemReward
{
    public int ItemID;
    public int num;
}

[Serializable]
public struct NPCStartIDChange
{
    public int NPCID;
    public int StartID;
}
[Serializable]
public class QuestReward
{
    public List<int> nextTaskList; //任务链

    public PlayerValueReward[] playerValueRewards;

    public NPCValueReward[] npcValueRewards;

    public ItemReward[] itemRewards;

    public NPCStartIDChange[] npcStartIDChanges;
    

}
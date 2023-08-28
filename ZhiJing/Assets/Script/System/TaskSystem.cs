using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Fungus;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class TaskSystem : ISystem //任务管理
{
    private static TaskSystem _taskSystem;

    private Dictionary<int,BaseTask> _tasks=new Dictionary<int, BaseTask>(); //存放当前任务

    private Dictionary<int, BaseTask> _finishTasks=new Dictionary<int, BaseTask>(); //已完成任务

    private Dictionary<int, TalkBase> _talkBases = new Dictionary<int, TalkBase>(); //保存当前所有可交互NPC

    private Dictionary<string, GameObject> _characters = new Dictionary<string, GameObject>(); //保存Say所需Character

    public UnityAction<int, int> addItemAction=new UnityAction<int, int>((a,b)=>{}); //获得物品时调用
    
    public UnityAction<int, int> removeItemAction=new UnityAction<int, int>((a,b)=>{}); //丢失物品时调用

    public UnityAction<int, int> talkToAction=new UnityAction<int, int>((a,b)=>{}); //对话时调用


    public override void Init() //读取当前任务表
    {
        Load();
        SystemMediator.Instance.eventSystem.saveaction += Save;
    }

    public string GetNPCNameByID(int id)
    {
        return _talkBases[id].Name;
    }

    public override void Tick()
    {

    }

    public Dictionary<int, BaseTask> GetCurrentTasks()
    {
        return _tasks;
    }

    public static TaskSystem GetTaskSystem()
    {
        if (!_taskSystem)
        {
            _taskSystem = GameObject.FindObjectOfType<TaskSystem>().GetComponent<TaskSystem>();

        }

        return _taskSystem;
    }

    public void Load()
    {

    }

    public void Save()
    {

    }

    public bool CheckTaskConditions(BaseTask task)
    {
        QuestAcceptCondition questAcceptCondition = task.acceptCondition;
        if (!questAcceptCondition.HasCondition)
        {
            return true;
        }

        foreach (int id in questAcceptCondition.preTaskList)
        {
            if (!_finishTasks.ContainsKey(id))
            {
                return false;
            }
        }

        if (!CheckPlayerValues(questAcceptCondition))
        {
            return false;
        }

        if (!CheckNPCsValues(questAcceptCondition))
        {
            return false;
        }


        return true;
    }

    public bool CheckPlayerValues(QuestAcceptCondition questAcceptCondition)
    {
        PlayerValueCondition[] playerValueConditions = questAcceptCondition.playerValueConditions;
        PlayerController playerController= _systemMediator.playerController;
        foreach (PlayerValueCondition playerValueCondition in playerValueConditions)
        {
            if (!playerController.PlayerValueChcek(playerValueCondition.value, playerValueCondition.condition,
                    playerValueCondition.reqvalue))
            {
                return false;
            }
        }

        return true;

    }

    public bool CheckNPCsValues(QuestAcceptCondition questAcceptCondition)
    {
        //continue
        return true;
    }

    public IEnumerator AcceptTask(BaseTask task)
    {
        if (!_tasks.ContainsKey(task.ID))
        {
            if (CheckTaskConditions(task))
            {
                _tasks.Add(task.ID, task);
                task.Init();
                _systemMediator.uisystem.AddTask(task);
            }
            else
            {

            }
        }

        yield return null;
    }

    public void AddItemAction(int id,int num)
    {
        addItemAction.Invoke(id,num);
        StartCoroutine(CheckTasks());
    }

    public void RemoveItemAction(int id, int num)
    {
        removeItemAction.Invoke(id,num);
        StartCoroutine(CheckTasks());
    }

    public void TalkToAction(int npcid, int dialid)
    {
        talkToAction.Invoke(npcid,dialid);
        StartCoroutine(CheckTasks());
    }

    public void AddNPC(TalkBase npc)
    {
        if (!_talkBases.ContainsKey(npc.ID))
        {
            _talkBases.Add(npc.ID,npc);
        }
    }

    private IEnumerator CheckTasks()
    {
        List<int> finfishtasks = new List<int>();
        foreach (BaseTask task in _tasks.Values)
        {
            if (task.IsComplete())
            {
                finfishtasks.Add(task.ID);
            }
        }

        foreach (int taskid in finfishtasks)
        {
            FinishTask(taskid);
        }

        yield return null;
    }

    private void FinishTask(int taskid)
    {
        if (_tasks.ContainsKey(taskid))
        {
            Debug.Log("完成任务"+taskid);
            BaseTask task = _tasks[taskid];
            task.UnBind();
            _tasks.Remove(task.ID);
            _finishTasks.Add(task.ID,task);
            _systemMediator.uisystem.FinishTask(task.ID);
        }
    }

    public GameObject GetCharacter(string name)
    {
        if (_characters.ContainsKey(name))
        {
            return _characters[name];
        }
        else
        {
            GameObject character=Instantiate(Resources.Load<GameObject>("Prefabs/Character"));
            Character c = character.GetComponent<Character>();
            c.SetStandardText(name);
            _characters.Add(name,character);
            return character;
        }
    }
    

}

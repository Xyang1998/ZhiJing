using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISystem : ISystem
{
    public GameObject interacting;
    private Text interactingtext;
    public float messageheight=300;
    private UnityAction TickUA = new UnityAction(() => {});
    public GameObject menu;
    public GameObject taskUI;
    public GameObject taskContent;
    private Dictionary<int, SimpleTask> _simpleTasks = new Dictionary<int, SimpleTask>();
    public GameObject detailedTask;
    private bool islock = false;

    public List<Displayable> displayables; //按键控制显示的ui界面（同一时间只显示一个）
    private Displayable preDisplay;
    public override void Init()
    {
        interactingtext = interacting.transform.GetComponentInChildren<Text>();
        displayables = new List<Displayable>();
    }

    public override void Tick()
    {
        TickUA.Invoke();
        
    }



    public void Showinteracting(string text) //靠近物体时显示”交谈“等信息
    {
        interacting.SetActive(true);
        Vector3 PlayerScreenPos = Camera.main.WorldToScreenPoint(SystemMediator.Instance.playerController.GetPlayerPos());
        Debug.Log(PlayerScreenPos);
        interacting.GetComponent<RectTransform>().position = new Vector2(PlayerScreenPos.x, PlayerScreenPos.y + messageheight);
        interactingtext.text = text;
        TickUA += Showinginteracting;
    }

    public void Showinginteracting()
    {
        Vector3 PlayerScreenPos = Camera.main.WorldToScreenPoint(SystemMediator.Instance.playerController.GetPlayerPos());
        interacting.transform.position = new Vector3(PlayerScreenPos.x, PlayerScreenPos.y + messageheight, 0);
    }

    public void Hideinteracting()
    {
        interacting.SetActive(false);
        TickUA -= Showinginteracting;
    }

    
    public void SaveGame()
    {
        SystemMediator.Instance.playerController.SavePlayerPos();
        //PlayerController.GetPlayerController().GetPlayer().GetComponent<PlayerState>().Save();
        _systemMediator.eventSystem.Save(); //执行所有Save
    }

    public void AddTask(BaseTask task)
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/UI/SimpleTask"));
        SimpleTask simpleTask = go.GetComponent<SimpleTask>();
        simpleTask.SetTask(task);
        _simpleTasks.Add(simpleTask.taskID,simpleTask);
        go.transform.SetParent(taskContent.transform);
    }

    public void Lock()
    {
        islock = true;
    }

    public void UnLock()
    {
        islock = false;
    }

    public void FinishTask(int id)
    {
        Destroy(_simpleTasks[id].gameObject);
        _simpleTasks.Remove(id);
    }

    public void ShowUI(Displayable displayable)
    {
        if (!islock)
        {
            if (preDisplay)
            {
                preDisplay.Hide();
                if (preDisplay != displayable)
                {
                    displayable.Show();
                    preDisplay = displayable;
                }
                else
                {
                    preDisplay = null;
                }
            }
            else
            {
                displayable.Show();
                preDisplay = displayable;
            }
            
            
        }
    }


}

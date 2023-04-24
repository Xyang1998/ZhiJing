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
    public bool islock = false;
    public override void Init()
    {
        interactingtext = interacting.transform.GetComponentInChildren<Text>();
    }

    public override void Tick()
    {
        HandleMenu();
        TickUA.Invoke();
        
    }

    public void Showinteracting(string text) //靠近物体时显示”交谈“等信息
    {
        interacting.SetActive(true);
        Vector3 PlayerScreenPos = Camera.main.WorldToScreenPoint(PlayerController.GetPlayerController().GetPlayerPos());
        Debug.Log(PlayerScreenPos);
        interacting.GetComponent<RectTransform>().position = new Vector2(PlayerScreenPos.x, PlayerScreenPos.y + messageheight);
        interactingtext.text = text;
        TickUA += Showinginteracting;
    }

    public void Showinginteracting()
    {
        Vector3 PlayerScreenPos = Camera.main.WorldToScreenPoint(PlayerController.GetPlayerController().GetPlayerPos());
        interacting.transform.position = new Vector3(PlayerScreenPos.x, PlayerScreenPos.y + messageheight, 0);
    }

    public void Hideinteracting()
    {
        interacting.SetActive(false);
        TickUA -= Showinginteracting;
    }

    public void HandleMenu()
    {
        if (!islock)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("按下E");
                if (menu.activeSelf)
                {
                    menu.SetActive(false);
                }
                else menu.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("按下E");
                if (taskUI.activeSelf)
                {
                    taskUI.SetActive(false);
                }
                else
                {
                    taskUI.SetActive(true);
                    detailedTask.GetComponent<DetailedTask>().ClearText();
                }
            }
        }

    }
    public void SaveGame()
    {
        PlayerController.GetPlayerController().SavePlayerPos();
        //PlayerController.GetPlayerController().GetPlayer().GetComponent<PlayerState>().Save();
        _systemMediator.GetEventSystem().Save(); //执行所有Save
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


}

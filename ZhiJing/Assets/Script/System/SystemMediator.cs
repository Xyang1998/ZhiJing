using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SystemMediator : MonoBehaviour
{
    public GameObject eventSystemObj;
    public GameObject uiSystemobj;
    private EventSystem eventSystem;
    private UISystem uisystem;
    private PlayerController playerController;
    private TaskSystem taskSystem;
    private static SystemMediator _systemMediator;

    public static SystemMediator GetSystemMediator()
    {
        if (!_systemMediator)
        {
            _systemMediator = GameObject.FindObjectOfType<SystemMediator>().GetComponent<SystemMediator>();
            
        }

        return _systemMediator;
    }

    public UISystem GetUISystem()
    {
        return uisystem;
    }

    public EventSystem GetEventSystem()
    {
        return eventSystem;
        
    }

    public PlayerController GetPlayerController()
    {
        return playerController;
    }
    
    

    private void Awake()
    {
        //getSystem
        GetSystem();

        //setMediator
        setMediator();
        
        //Init
        Init();
        _systemMediator = this; //
    }

    private void Update()
    {
        playerController.Tick();
        uisystem.Tick();
        taskSystem.Tick();
    }

    private void GetSystem()
    {
        eventSystem = eventSystemObj.GetComponent<EventSystem>();
        uisystem = uiSystemobj.GetComponent<UISystem>();
        playerController=PlayerController.GetPlayerController();
        taskSystem = GetComponent<TaskSystem>();
    }

    private void setMediator()
    {
        playerController.setMediator(this);
        eventSystem.setMediator(this);
        uisystem.setMediator(this);
        taskSystem.setMediator(this);
    }

    private void Init()
    {
        eventSystem.Init();
        playerController.Init();
        uisystem.Init();
        taskSystem.Init();
        HandleNewGame();
    }

    public void Showinteracting(string talkmessage)
    {
        uisystem.Showinteracting(talkmessage);
    }

    public void Hideinteracting()
    {
        uisystem.Hideinteracting();
    }

    public bool NewGameCheck()
    {
        return playerController.NewGameCheck();
    }

    public void HandleNewGame()
    {
        if (NewGameCheck())
        {
            playerController.NewGame(); //初始化位置及各属性
        }
        else
        {
            playerController.LoadGame();
        }
    }


}

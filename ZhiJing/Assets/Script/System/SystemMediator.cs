using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SystemMediator : SingletonMono<SystemMediator>
{
    
    public EventSystem eventSystem
    {
        get;
        private set;
        
    }

    public UISystem uisystem
    {
        get;
        private set;
    }

    public PlayerController playerController
    {
        get;
        private set;
    }

    public TaskSystem taskSystem
    {
        get;
        private set;
    }

    public InventoryManager inventoryManager
    {
        get;
        private set;
    }
    

    private void Awake()
    {
        //getSystem
        GetSystem();

        //setMediator
        setMediator();
        
        //Init
        Init();
        

    }

    private void Update()
    {
        playerController.Tick();
        uisystem.Tick();
        taskSystem.Tick();
    }

    private void GetSystem()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        eventSystem = FindObjectOfType<EventSystem>();
        uisystem = FindObjectOfType<UISystem>();
        playerController=FindObjectOfType<PlayerController>();
        taskSystem = GetComponent<TaskSystem>();
    }

    private void setMediator()
    {
        playerController.setMediator(this);
        eventSystem.setMediator(this);
        uisystem.setMediator(this);
        taskSystem.setMediator(this);
        if (inventoryManager)
        {
            inventoryManager.setMediator(this);
        }
    }

    private void Init()
    {
        eventSystem.Init();
        playerController.Init();
        uisystem.Init();
        taskSystem.Init();
        if (inventoryManager)
        {
            inventoryManager.Init();
        }

        HandleNewGame();
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

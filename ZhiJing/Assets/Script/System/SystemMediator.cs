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
        uisystem.Tick();
    }

    private void GetSystem()
    {
        eventSystem = eventSystemObj.GetComponent<EventSystem>();
        uisystem = uiSystemobj.GetComponent<UISystem>();
    }

    private void setMediator()
    {
        PlayerController.GetPlayerController().setMediator(this);
        eventSystem.setMediator(this);
        uisystem.setMediator(this);
    }

    private void Init()
    {
        uisystem.Init();
        
    }

    public void Showinteracting(string talkmessage)
    {
        uisystem.Showinteracting(talkmessage);
    }

    public void Hideinteracting()
    {
        uisystem.Hideinteracting();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EventSystem : ISystem
{
    private UnityAction UA;
    private UnityAction TickUA;
    private IEnumerator IE;
    public void AddAction(UnityAction myaction)
    {
        UA += myaction;
    }
    public void DelAction(UnityAction myaction)
    {
        UA -= myaction;
    }
    public void InvokeAction()
    {
        UA.Invoke();
    }
    public void AddTAction(UnityAction myaction)
    {
        TickUA += myaction;
    }
    public void DelTAction(UnityAction myaction)
    {
        TickUA -= myaction;
    }

}

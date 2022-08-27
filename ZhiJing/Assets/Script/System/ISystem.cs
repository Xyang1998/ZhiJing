using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISystem : MonoBehaviour
{
    public SystemMediator _systemMediator;

    public void setMediator(SystemMediator systemMediator)
    {
        _systemMediator = systemMediator;
    }

    public virtual void Init()
    {
        
    }

    public virtual void Tick()
    {
        
    }
   
}

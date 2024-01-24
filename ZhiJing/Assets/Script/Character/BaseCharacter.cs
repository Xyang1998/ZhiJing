using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    
    public int ID;
    public string Name;

    public void Start()
    {

        SystemMediator.Instance.taskSystem.AddNPC(this);
    }

    public virtual void Moveto()
    {
        
    }

    public virtual void Turn(float direction)
    {
        if (direction > 0)
        {
            transform.rotation=Quaternion.Euler(0,0,0);
        }
        else if(direction<0)
        {
            transform.rotation=Quaternion.Euler(0,180,0);
        }
    }

    public virtual void Dead()
    {
        
    }
}

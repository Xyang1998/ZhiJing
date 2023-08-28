using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;



public class PlayerController : ISystem
{
    
    private Rigidbody2D PlayerRigidDody;
    public float MoveSpeed = 5;
    public Player player; //控制的角色物体


    private float InputX;
    private static bool canMove = true;
    private static bool canTalk = false;
    
    private TalkBase CurTalker;
    public override void Init()
    {
        GetPlayerComp();
    }
    // Update is called once per frame
    public override void Tick()
    {
        if (canTalk)
        {
            TalkTo();
        }

        if (canMove)
        {

            Run();
        }
    }

    void GetPlayerComp()
    {
        PlayerRigidDody = GetComponent<Rigidbody2D>();
    }



    public void IntelligenceChange(float x)
    {
        player.playerState.IntelligenceChange(x);
    }
    public void RenYiChange(float x)
    {
        player.playerState.RenYiChange(x);
    }
    public void YangHuiChange(float x)
    {
        player.playerState.YangHuiChange(x);
    }
    public void PlayerTalkEnd()
    {
       UnLock();
       _systemMediator.uisystem.UnLock();
    }
    public bool ItemsCheck(List<int> list)
    {
        return false;
    }
    public void ItemsUse(List<int> list)
    {
        
    }
    public bool PlayerValueChcek(PlayerValue playerValue,Condition condition,float value)
    {
        var method = typeof(PlayerState).GetMethod("GetPlayer" + playerValue.ToString());
        if (method == null) return false;
        switch (condition)
        {
            case Condition.More:
                if ((float)method.Invoke(player.playerState, null) >= value)
                {
                    return true;
                }

                return false;
            case Condition.Less:
                if ((float)method.Invoke(player.playerState, null) <= value)
                {
                    return true;
                }

                return false;
            case Condition.Equal:
                if (Mathf.Abs((float)method.Invoke(player.playerState, null) - value)<=0.1)
                {
                    return true;
                }

                return false;
            default:
                return false;
                
                
        }
    }
    public bool RenYiChcek(Condition condition,float value)
    {
        return false;
    }
    public bool YangHuiChcek(Condition condition,float value)
    {
        return false;
    }

    public Vector3 GetPlayerPos()
    {
        return transform.position;
    }

    public void SetPlayerPos(Vector3 Pos)
    {
        transform.position = Pos;
    }

    public void SavePlayerPos()
    {
        
    }

    public Vector2 LoadPlayerPos()
    {
        return new Vector2(0,0);
    }

    public bool NewGameCheck()
    {
        return false;
    }

    public void NewGame()
    {
        
        SetPlayerPos(new Vector3(0,0,0));
    }

    public void LoadGame()
    {
        //SetPlayerPos(playerState.LoadPos());
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("IN:"+other.name);
        CurTalker = other.GetComponent<TalkBase>();
        canTalk = true;
        _systemMediator.uisystem.Showinteracting(CurTalker.talkmessage);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OUT:"+other.name);
        CurTalker = other.GetComponent<TalkBase>();
        canTalk = false;
        _systemMediator.uisystem.Hideinteracting();
    }
    public void Run()//移动 
    {
        InputX = Input.GetAxis("Horizontal");
        Vector2 input = new Vector2(InputX, 0);
        player.Turn(input.x);
        PlayerRigidDody.velocity = input * MoveSpeed;
        //动画部分
    }
    
    public bool TalkCheck()
    {
        if (canTalk)
        {
            if (CurTalker)
            {
                return true;
            }
        }
        return false;
    }

    public void TalkTo()
    {
        if (Input.GetKeyDown(KeyCode.Space) && TalkCheck())
        {
            Lock();
            _systemMediator.uisystem.Lock();
            CurTalker.StartTalk();
            //PlayerController.GetPlayerController()._systemMediator.Hideinteracting();
        }

    }

    public static void Lock()
    {
        canMove = false;
        canTalk = false;
        
    }

    public static void UnLock()
    {
        canMove = true;
        canTalk = true;
    }
    



}

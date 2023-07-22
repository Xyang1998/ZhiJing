using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;



public class PlayerController : ISystem
{
    private Player player;
    private static  PlayerController playercontroller;

    private PlayerState _playerState;
    // Start is called before the first frame update
    public override void Init()
    {
        GetPlayerComp();
    }
    // Update is called once per frame
    public override void Tick()
    {
        player.TalkTo();
    }
    private void Update()
    {
        player.Run();
        player.OpenMyBag();//打开背包 使用fixedupdate会出现卡顿
        
    }
    void GetPlayerComp()
    {
        player=Player.GetPlayer();
        _playerState = player.GetComponent<PlayerState>();
    }

    public Player GetPlayer()
    {
        return player;
    }
    public static PlayerController GetPlayerController()
    {
        if (!playercontroller)
        {
            playercontroller = FindObjectOfType<PlayerController>();
            
        }
        return playercontroller;
    }
    public void IntelligenceChange(float x)
    {
        _playerState.IntelligenceChange(x);
    }
    public void RenYiChange(float x)
    {
        _playerState.RenYiChange(x);
    }
    public void YangHuiChange(float x)
    {
       _playerState.YangHuiChange(x);
    }
    public void PlayerTalkEnd()
    {
        Player.UnLock();
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
                if ((float)method.Invoke(_playerState, null) >= value)
                {
                    return true;
                }

                return false;
            case Condition.Less:
                if ((float)method.Invoke(_playerState, null) <= value)
                {
                    return true;
                }

                return false;
            case Condition.Equal:
                if (Mathf.Abs((float)method.Invoke(_playerState, null) - value)<=0.1)
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
        return player.transform.position;
    }

    public void SetPlayerPos(Vector3 Pos)
    {
        player.transform.position = Pos;
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

}

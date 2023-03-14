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
    private void FixedUpdate()
    {
        player.Run();
        
    }
    void GetPlayerComp()
    {
        player=Player.GetPlayer();
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
        
    }
    public void RenYiChange(float x)
    {
        
    }
    public void YangHuiChange(float x)
    {
       
    }
    public void PlayerTalkEnd()
    {
        player.TalkEnd();
    }
    public bool ItemsCheck(List<int> list)
    {
        return false;
    }
    public void ItemsUse(List<int> list)
    {
        
    }
    public bool IntelligenceChcek(float value)
    {
        return false;
    }
    public bool RenYiChcek(float value)
    {
        return false;
    }
    public bool YangHuiChcek(float value)
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

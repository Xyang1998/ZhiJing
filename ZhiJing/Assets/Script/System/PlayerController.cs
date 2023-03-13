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
    private PlayerState playerstate;
    
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
        playerstate = GameObject.Find("Player").GetComponent<PlayerState>();
        playerstate.ChangeIntelligence(x);
    }
    public void RenYiChange(float x)
    {
        playerstate = GameObject.Find("Player").GetComponent<PlayerState>();
        playerstate.ChangeRenYi(x);
    }
    public void YangHuiChange(float x)
    {
        playerstate = GameObject.Find("Player").GetComponent<PlayerState>();
        playerstate.ChangeYangHui(x);
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
        playerstate = GameObject.Find("Player").GetComponent<PlayerState>();
        if (playerstate.Showstate().Intelligence >= value)
        {
            return true;
            
        }
        return false;
    }
    public bool RenYiChcek(float value)
    {
        playerstate = GameObject.Find("Player").GetComponent<PlayerState>();
        if (playerstate.Showstate().RenYi >= value)
        {
            return true;
            
        }
        return false;
    }
    public bool YangHuiChcek(float value)
    {   
        playerstate = GameObject.Find("Player").GetComponent<PlayerState>();
        if (playerstate.Showstate().YangHui >= value)
        {
            return true;
            
        }
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

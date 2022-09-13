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
    public PlayerState playerState;
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
        playerState.IntelligenceChange(x);
    }
    public void RenYiChange(float x)
    {
        playerState.RenYiChange(x);
    }
    public void YangHuiChange(float x)
    {
        playerState.YangHuiChange(x);
    }
    public void PlayerTalkEnd()
    {
        player.TalkEnd();
    }
    public bool ItemsCheck(List<int> list)
    {
        return playerState.ItemsCheck(list);
    }
    public void ItemsUse(List<int> list)
    {
        playerState.ItemsUse(list);
    }
    public bool IntelligenceChcek(float value)
    {
        return  playerState.IntelligenceChcek(value);
    }
    public bool RenYiChcek(float value)
    {
        return  playerState.RenYiChcek(value);
    }
    public bool YangHuiChcek(float value)
    {
        return  playerState.YangHuiChcek(value);
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
        playerState.SavePos(player.transform.position);
    }

    public Vector2 LoadPlayerPos()
    {
        return playerState.LoadPos();
    }

    public bool NewGameCheck()
    {
        return playerState.IsANewGame();
    }

    public void NewGame()
    {
        playerState.NewGame();
        SetPlayerPos(new Vector3(0,0,0));
    }

    public void LoadGame()
    {
        SetPlayerPos(playerState.LoadPos());
    }

}

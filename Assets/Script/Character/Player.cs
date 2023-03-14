using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;

public class Player : BaseCharacter
{
    public float MoveSpeed = 5;
    private float InputX;
    private static Player player;
    private bool canmove = true;
    private bool cantalk = false;
    private TalkBase CurTalker;
    private Rigidbody2D PlayerRigidDody;

    private void Awake()
    {
        PlayerRigidDody = GetComponent<Rigidbody2D>();

    }

    public static Player GetPlayer()
    {
        if (!player)
        {
            player = GameObject.FindObjectOfType<Player>();
        }
        return player;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("IN:"+other.name);
        CurTalker = other.GetComponent<TalkBase>();
        cantalk = true;
        PlayerController.GetPlayerController()._systemMediator.Showinteracting(CurTalker.talkmessage);
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OUT:"+other.name);
        CurTalker = other.GetComponent<TalkBase>();
        cantalk = false;
        PlayerController.GetPlayerController()._systemMediator.Hideinteracting();
    }

    public bool TalkCheck()
    {
        if (cantalk)
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
            canmove = false;
            cantalk = false;
            CurTalker.StartTalk();
            PlayerController.GetPlayerController()._systemMediator.Hideinteracting();
        }

    }
    public void Run()//移动
    {
        if (canmove)
        {
            InputX = Input.GetAxis("Horizontal");
            Vector2 input = new Vector2(InputX, 0);
            Turn(input.x);
            PlayerRigidDody.velocity = input * MoveSpeed;
            //动画部分
        }
    } 


    public void TalkEnd()//对话结束
    {
        cantalk = true;
        canmove = true;
    } 
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
[CreateAssetMenu]
public class PlayerState : ScriptableObject
{
    private bool IsNewGame = true;
    public int CurChapter = 0;
    [SerializeField] private float Intelligence = 100;
    [SerializeField] private Vector2 CurPosition = new Vector2(0,0);
    [SerializeField] private float RenYi = 100;
    [SerializeField] private float YangHui = 100;
    public Dictionary<int, string> Items = new Dictionary<int, string>(); //ID,名称
    public void IntelligenceChange(float x)
    {
        Intelligence += x;
    }
    public void RenYiChange(float x)
    {
        RenYi += x;
    }
    public void YangHuiChange(float x)
    {
        YangHui += x;
    }
    public void ResetValue()
    {
        Intelligence = 100;
        RenYi = 100;
        YangHui = 100;
        Items.Clear();
    }
    public bool IntelligenceChcek(float value)
    {
        if (Intelligence >= value) {return true;}
        return false;
    }
    public bool RenYiChcek(float value)
    {
        if (RenYi >= value) {return true;}
        return false;
    }
    public bool YangHuiChcek(float value)
    {
        if (YangHui >= value) {return true;}
        return false;
    }
    public bool ItemsCheck(List<int> list)
    {
        foreach (var item in list)
        {
            if (!Items.ContainsKey(item))
            {
                return false;
            }
        }

        return true;
    }
    public void ItemsUse(List<int> list)
    {
        foreach (var item in list)
        {
            if (Items.ContainsKey(item))
            {
                Items.Remove(item);
            }
        }
    }

    public void SavePos(Vector2 savepos)
    {
        CurPosition = savepos;
    }

    public Vector2 LoadPos()
    {
        return CurPosition;
    }

    public void NewGame()
    {
        CurChapter = 0;
        Intelligence = 100;
        CurPosition = new Vector2(0,0);
        RenYi = 100;
        YangHui = 100;
        Items.Clear();
        IsNewGame = false;
    }

    public void StartNewGame()
    {
        IsNewGame = true;
    }

    public bool IsANewGame()
    {
        return IsNewGame;
    }

    
}

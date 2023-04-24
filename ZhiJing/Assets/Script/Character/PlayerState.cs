using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[System.Serializable]
public struct State
{
    public State(float I,float R,float Y)
    {
        Intelligence = I;
        RenYi=R;
        YangHui=Y;
    }
    public float Intelligence;
    public float RenYi;
    public float YangHui;
}
public class PlayerState : MonoBehaviour
{
    private State state;
    private string FilePath = Application.streamingAssetsPath + "/State.json";
    public TaskTest TaskTest;

    private void Start()
    {
        TaskTest = Resources.Load<TaskTest>("TaskTest");
        Debug.Log(TaskTest.a);
        Load();
        SystemMediator.GetSystemMediator().GetEventSystem().saveaction += Save;
    }

    public void Save()
    {
        Debug.Log("保存成功");
        string savestr = JsonUtility.ToJson(state);
        File.WriteAllText(FilePath,savestr);
        string taskstr = JsonUtility.ToJson(TaskTest);
        File.WriteAllText(Application.streamingAssetsPath+"/test.json",taskstr);
    }

    public void Load()
    {
        string json = File.ReadAllText(FilePath);
        state = JsonUtility.FromJson<State>(json);
        json = File.ReadAllText(Application.streamingAssetsPath + "/test.json");
        //TaskTest = JsonUtility.FromJson<TaskTest>(json);
        //Debug.Log(TaskTest.a);
        Debug.Log(state.Intelligence);
        Debug.Log(state.RenYi);
        Debug.Log(state.YangHui);

    }
    public void IntelligenceChange(float x)
    {
        state.Intelligence = state.Intelligence+x>=0?state.Intelligence+x:0;
    }
    public void RenYiChange(float x)
    {
        state.RenYi = state.RenYi+x>=0?state.RenYi+x:0;
    }
    public void YangHuiChange(float x)
    {
        state.YangHui = state.YangHui+x>0?state.YangHui+x:0;
    }

    public float GetPlayerIntelligence()
    {
        return state.Intelligence;
    }
    public float GetPlayerRenYi()
    {
        return state.RenYi;
    }
    public float GetPlayerYangHui()
    {
        return state.YangHui;
    }
    
}

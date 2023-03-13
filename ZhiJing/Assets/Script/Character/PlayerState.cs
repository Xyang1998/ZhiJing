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
    private State state=new State(0,0,0);
    private string FilePath = Application.streamingAssetsPath + "/State.json";
    public TaskTest TaskTest;

    private void Start()
    {
        TaskTest = Resources.Load<TaskTest>("TaskTest");
        Debug.Log(TaskTest.a);
        Load();
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
        TaskTest = JsonUtility.FromJson<TaskTest>(json);
        Debug.Log(TaskTest.a);
        Debug.Log(state.Intelligence);
        Debug.Log(state.RenYi);
        Debug.Log(state.YangHui);

    }
    public void ChangeIntelligence(float x)
    {
        state.Intelligence += x;
    }
    public void ChangeRenYi(float x)
    {
        state.RenYi += x;
    }
    public void ChangeYangHui(float x)
    {
        state.YangHui += x;
    }

    public State Showstate()
    {
        return state;
    }
    
}

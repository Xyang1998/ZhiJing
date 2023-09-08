using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
[System.Serializable]
public struct State
{
    
    private float intelligence;
    /// <summary>
    /// 
    /// </summary>
    public float Intelligence
    {
        get => intelligence;
        set
        {
            intelligence = intelligence + value >= 0 ? intelligence + value : 0;
        }
    }
    
    private float renYi;

    /// <summary>
    /// 仁义值
    /// </summary>
    public float RenYi
    {
        get => renYi;
        set
        {
            renYi = renYi + value >= 0 ? renYi + value : 0;
        }
    }
    
    private float yangHui;

    public float YangHui
    {
        get => yangHui;
        set
        {
            yangHui = yangHui + value >= 0 ? yangHui + value : 0;
            
        }
    }
    public State(float I,float R,float Y)
    {
        intelligence = I;
        renYi=R;
        yangHui=Y;
       
    }



}
public class PlayerState 
{
    private State state;
    private Type stateType;
    private string FilePath = Application.streamingAssetsPath + "/State.json";
    public TaskTest TaskTest;
    

    private void Start()
    {
        TaskTest = Resources.Load<TaskTest>("TaskTest");
        SystemMediator.Instance.eventSystem.saveaction += Save;
    }

    public PlayerState()
    {
        state = new State(0, 0, 0);
        stateType = typeof(State);
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

    /// <summary>
    /// 用于检查玩家属性是否满足某一条件
    /// </summary>
    /// <param name="valueName">需要检查的属性名</param>
    /// <param name="right">将该属性如何对比？ example:">=1"</param>
    /// <returns></returns>
    public bool ValueCheck(string valueName,string right)
    {
        float value = GetValue(valueName);
        bool result = (bool)(new DataTable().Compute($"{value}" + right, ""));
        return result;
    }
    
    private float GetValue(string valueName)
    {
        PropertyInfo propertyInfo = stateType.GetProperty(valueName);
        if (propertyInfo != null)
        {
            return (float)propertyInfo.GetValue(state);
        }
        else
        {
            return 0;
        }
    }

 




    
}

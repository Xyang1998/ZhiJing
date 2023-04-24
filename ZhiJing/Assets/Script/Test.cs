using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
[Serializable]
public class Test1
{
    public List<int> _list = new List<int>();
}
[Serializable]
public class Test2
{
    public Test1[] test_1;
}

[Serializable]
public class A
{
    public float a;

    public float GET()
    {
        return a;
    }
}
public class Test : MonoBehaviour
{
    public Test2 _test2;
    public A a;
    public Data data;
    public Dial dial;

    // Start is called before the first frame update
    void Start()
    {
        data.Dials = new Dial[2];
        Dial dial1 = new Dial(0, "a", "asd", "1");
        Dial dial2=new Dial(1,"a","asd","2");
        data.Dials[0] = dial1;
        data.Dials[1] = dial2;
        Debug.Log(data.Dials.Length);
        string js = JsonUtility.ToJson(data);
        Debug.Log(js);
        string fileUrl = Application.streamingAssetsPath + "/test.json";
        StreamWriter sw = new StreamWriter(fileUrl);
        //保存数据
        sw.WriteLine(js);
        //关闭文档
        sw.Close();





    }

}

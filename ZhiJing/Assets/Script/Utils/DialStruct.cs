using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Cysharp.Threading.Tasks;
using ExcelDataReader;
using UnityEngine;

[Serializable]
public struct Dial
{
    public int ID;
    public string type; 
    public string speaker;
    public string dial;
    public string nextID;

    public Dial(int _id,string _type,string _speaker,string _dial,string _nextid)
    {
        ID = _id;
        type = _type;
        speaker = _speaker;
        dial = _dial;
        nextID = _nextid;
    }

}

[Serializable]
public class Data
{
    public Dial[] Dials;
}

[Serializable]
public struct MenuData
{
    public string blockID; //附着Block ID
    public string nextBlockID; //跳转Block ID
    public string text; //选项文本
    public MenuData(string id1,string id2,string text_)
    {
        blockID = id1;
        nextBlockID = id2;
        text = text_;

    }
}

public class ExcelLoader<T>
{
   public List<T> list
   {
      get;
      private set;
   }

   public ExcelLoader()
   {
      list = new List<T>();
   }
   public void LoadFromPath(string path)
   {
      list.Clear();
      if (File.Exists(path))
      {
         FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.Read);
         IExcelDataReader excelDataReader = ExcelReaderFactory.CreateBinaryReader(fileStream);
         Type type = typeof(T);
         FieldInfo[] fieldInfos = type.GetFields();
         Debug.Log(fieldInfos.Length);
         //跳过第一行的定义
         excelDataReader.Read();
         while (excelDataReader.Read())
         {
            System.Object t = Activator.CreateInstance<T>();
            for (int i = 0; i < fieldInfos.Length; i++)
            {
               try
               {
                  Debug.Log(excelDataReader.GetValue(i).ToString());
                  if (i == 0)
                  {
                     fieldInfos[i].SetValue(t, int.Parse(excelDataReader.GetValue(i).ToString()));
                  }
                  else
                  {
                     if (fieldInfos[i].FieldType == typeof(int))
                     {
                        fieldInfos[i].SetValue(t, int.Parse(excelDataReader.GetValue(i).ToString()));
                     }
                     else
                     {
                        fieldInfos[i].SetValue(t, excelDataReader.GetValue(i).ToString());
                     }
                  }
               }
               catch (Exception e)
               {
                  Debug.Log(e);
               }
            }
            

            list.Add((T)t);
         }
      }
   }

   
   
   
   
}



using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public class SaveObjectData : MonoBehaviour
{
    public string ID;
    public string Name;
    public Vector3 Position;
    public int StartID;
    public int Favorability;

    // Save the object data to a JSON file
    public void Save()
    {
        ObjectData data = new ObjectData();
        data.ID = this.ID;
        data.Name = this.Name;
        data.Position = this.Position;
        data.StartID = this.StartID;
        data.Favorability = this.Favorability;

        string jsonData = JsonConvert.SerializeObject(data);
        string filePath = Application.dataPath + "/ID_Name.json";
        File.WriteAllText(filePath, jsonData, Encoding.UTF8);
    }

    // Load the object data from a JSON file
    public void Load()
    {
        string filePath = Application.dataPath + "/ID_Name.json";
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath, Encoding.UTF8);
            ObjectData data = JsonConvert.DeserializeObject<ObjectData>(jsonData);

            this.ID = data.ID;
            this.Name = data.Name;
            this.Position = data.Position;
            this.StartID = data.StartID;
            this.Favorability = data.Favorability;
        }
    }

    // Define the object data structure
    private class ObjectData
    {
        public string ID;
        public string Name;
        public Vector3 Position;
        public int StartID;
        public int Favorability;
    }
}

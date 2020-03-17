using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

[Serializable]
public class RobotCreatedComponent
{
    public string prefabName;
    public Vector3 pos;
    public Vector3 rot;
    public Vector3 scale;
}

[Serializable]
public class RobotCreated
{
    public List<RobotCreatedComponent> RobotComponents = new List<RobotCreatedComponent>();
}

[Serializable]
public class RobotsCreated
{
    public List<RobotCreated> RobotCreated = new List<RobotCreated>();
}

public class RobotCreateTool{

    private static RobotCreateTool _inst;
    public static RobotCreateTool Instance
    {
        get
        {
            if (_inst == null)
                _inst = new RobotCreateTool();
            return _inst;
        }
    }
    public RobotsCreated RobotsCreated = null;
    private string RobotsCreatedJsonFilePath = Application.dataPath + "/Resources/Json/RobotsCreate.json";
    
    public void SaveJsonRobotCreated(RobotCreated robot)
    {
        if (RobotsCreated == null)
            RobotsCreated = LoadJsonRobotCreated();
        if (RobotsCreated == null)
            RobotsCreated = new RobotsCreated();
        RobotsCreated.RobotCreated.Add(robot);
        string json = JsonUtility.ToJson(RobotsCreated);
        //StreamWriter sw = new StreamWriter()
        File.WriteAllText(RobotsCreatedJsonFilePath, json, Encoding.UTF8);
    }

    public RobotsCreated LoadJsonRobotCreated()
    {
        if (!File.Exists(RobotsCreatedJsonFilePath))
            return null;
        StreamReader sr = new StreamReader(RobotsCreatedJsonFilePath);
        if (sr == null)
            return null;
        string json = sr.ReadToEnd();
        sr.Close();
        if (json.Length > 0)
            return JsonUtility.FromJson<RobotsCreated>(json);
        return null;
    }

    public RobotsCreated GetRobotsCreated()
    {
        if (RobotsCreated != null) return RobotsCreated;
        RobotsCreated = LoadJsonRobotCreated();
        if (RobotsCreated != null) return RobotsCreated;
        return new RobotsCreated();
    }
}

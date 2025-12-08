using System;
using System.Collections.Generic;

using UnityEngine;

using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class AnalyticsController : MonoBehaviour
{
    public static AnalyticsController Self;
    List<AnlyData> anlyDataList = new List<AnlyData>();
    int id = 0;
    string url = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSd4p0Evp41rKZfHQEzhtiavd6ayy_Fbuh50OHKyJru8S25elw/formResponse";
    private void Awake()
    {
        if (Self == null)
        {
            Self = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        StartAnly("GameLaunch", 1);
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartAnly("SceneLoad_" + scene.name, 1);
    }
    void OnSceneUnloaded(Scene scene)
    {
        CompleteAnly("SceneLoad_" + scene.name);
        if(scene.buildIndex != 0){
            CompleteAllAnlyData();
            Send();
            ClearAllData();
        }
    }
    public int StartAnly(string name, float value)
    {
        AnlyData anlyData = new AnlyData(id, name, value);
        if (anlyDataList.Exists(x => x.name == name && !x.isComplete))
        {
            return -1;
        }
        anlyDataList.Add(anlyData);
        id++;
        return anlyData.id;
    }
    public void CompleteAnly(int id)
    {
        AnlyData anlyData = anlyDataList.Find(x => x.id == id);
        if (anlyData != null)
        {
            anlyData.isComplete = true;
            anlyData.endTime = DateTime.Now;
        }
    }
    public void CompleteAnly(string name)
    {
        AnlyData anlyData = anlyDataList.Find(x => x.name == name && !x.isComplete);
        if (anlyData != null)
        {
            anlyData.isComplete = true;
            anlyData.endTime = DateTime.Now;
        }
    }
    public void UpdateAnlyValue(int id, float value)
    {
        AnlyData anlyData = anlyDataList.Find(x => x.id == id);
        if (anlyData != null)
        {
            anlyData.value = value;
        }
    }
    public void UpdateAnlyValue(string name)
    {
        AnlyData anlyData = anlyDataList.Find(x => x.name == name && !x.isComplete);
        if (anlyData != null)
        {
            anlyData.value++;
            anlyData.endTime = DateTime.Now;
        }
    }
    public AnlyList GetAllAnlyData()
    {
        return new AnlyList(anlyDataList.ToArray());
    }
    public int NewAnly(string name, float value)
    {
        AnlyData anlyData = new AnlyData(id, name, value, true);
        anlyData.endTime = anlyData.startTime;
        anlyDataList.Add(anlyData);
        id++;
        return anlyData.id;
    }
    void CompleteAllAnlyData()
    {
        foreach (AnlyData item in anlyDataList)
        {
            if (!item.isComplete)
            {
                item.isComplete = true;
                item.endTime = DateTime.Now;
            }
        }
        anlyDataList.Sort((x, y) => x.name.CompareTo(y.name));
    }
    void OnApplicationQuit()
    {
        CompleteAllAnlyData();
        Send();
    }
    public void Send()
    {
        AnlyList allData = GetAllAnlyData();
        foreach (AnlyData item in allData.anlyList)
        {
            WWWForm form = new();
            form.AddField("entry.1518256880",SystemInfo.deviceUniqueIdentifier);
            form.AddField("entry.1388893675", item.name);
            form.AddField("entry.952154051", item.value.ToString());
            if (item.endTime == DateTime.MinValue || item.startTime == item.endTime)
            {
                form.AddField("entry.1952479796", "0");
            }
            else
            {
                TimeSpan timeSpan = item.endTime - item.startTime;
                form.AddField("entry.1952479796", timeSpan.ToString("hh\\:mm\\:ss"));
            }
            form.AddField("entry.778222421", item.isComplete ? "true" : "false");
            UnityWebRequest www = UnityWebRequest.Post(url, form);
            www.SendWebRequest();
        }
    }
    void ClearAllData()
    {
        anlyDataList.Clear();
        id = 0;
    }
}

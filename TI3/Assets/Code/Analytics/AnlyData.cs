

using System;

public class AnlyData 
{
    public int id { get; set;}
    public string name { get; set;}
    public float value { get; set; }
    public DateTime startTime { get; set; }
    public DateTime endTime { get; set; }
    public bool isComplete { get; set; } = false;
    public AnlyData(int id, string name, float value) : this(id, name, value, false){}
    public AnlyData(int id, string name, float value, bool isComplete)
    {
        this.name = name;
        this.value = value;
        this.isComplete = isComplete;
        startTime = DateTime.Now;
        this.id = id;
    }
}

public class AnlyList
{
    public AnlyData[] anlyList;
    public AnlyList(AnlyData[] anlyList)
    {
        this.anlyList = anlyList;
    }  
}
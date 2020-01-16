using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class Task : MonoBehaviour
{
    /// <summary>
    /// Contains data about existing task
    /// </summary>

    [SerializeField] private TaskList taskList;

    [SerializeField] private Text nameText;
    [SerializeField] private Text dateText;
    [SerializeField] private Image tagImage;
    [SerializeField] private Text tagText; 

    private int year;
    private int month;
    private int day;

    private Tag tagRef;

    private int id;

    public string Name {
        get { return nameText.text; }
        set { nameText.text = value; }
    }

    public int Date { get { return (year * 10000) + (month * 100) + day; } }

    public string TagName { get { return tagRef.name; } }

    public int ID { set { id = value; } }

    public void SetDate(int day, int month, int year)
    {
        this.day = day;
        this.month = month;
        this.year = year;

        dateText.text = MonthSelect.months[month-1].ToString().Substring(0, 3) + " " + day;
    }
    public int[] GetDate()
    {
        return new int[] { day, month, year };
    }

    public void SetTag(Tag tag)
    {
        tagRef = tag;
        RefreshTag();
    }

    public void RefreshTag()
    {
        tagText.text = tagRef.name;
        tagImage.color = tagRef.color;
    }

    public void Done()
    {
        taskList.RemoveTask(id);
    }

    public void Edit()
    {
        taskList.Edit(id);
    }

    public SaveData GetSaveData()
    {
        SaveData saveData = new SaveData();
        saveData.name = Name;
        saveData.day = day;
        saveData.month = month;
        saveData.year = year;
        saveData.tagName = TagName;
        return saveData;
    }
}

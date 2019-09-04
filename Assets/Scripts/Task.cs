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
    private string tagName;

    private int id;

    public string Name {
        get { return nameText.text; }
        set { nameText.text = value; }
    }

    public int Date { get { return (year * 10000) + (month * 100) + day; } }

    public string Tag { get { return tagName; } }

    public int Id { set { id = value; } }

    public void SetDate(int day, int month, int year)
    {
        this.day = day;
        this.month = month;
        this.year = year;

        dateText.text = MonthSelect.months[month-1].ToString().Substring(0, 3) + " " + day;
    }

    public void SetTag(string tagName)
    {
        this.tagName = tagName;

        tagText.text = tagName;
        if (TagSelect.tagLookup.ContainsKey(tagName))
        {
            tagImage.color = TagSelect.tagLookup[tagName].color;
        }
        else
        {
            tagImage.color = TagSelect.tagLookup["Other"].color;
        }
        
    }

    public void Done()
    {
        taskList.RemoveTask(id);
    }

    public SaveData GetSaveData()
    {
        SaveData saveData = new SaveData();
        saveData.name = Name;
        saveData.day = day;
        saveData.month = month;
        saveData.year = year;
        saveData.tagName = tagName;
        return saveData;
    }
}

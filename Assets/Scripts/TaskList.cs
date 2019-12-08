using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class TaskList : MonoBehaviour
{
    /// <summary>
    /// handles the creation and completion of tasks
    /// </summary>
    
    [SerializeField] private Options options;
    [SerializeField] private ScreenController screenController;

    [SerializeField] private RectTransform content;
    [SerializeField] private GameObject taskTemplate;
    [SerializeField] private TagSelect tagSelect;

    private int currentId = 0;
    private Dictionary<int, GameObject> tasksUIs;
    private Dictionary<int, Task> tasks; 
    private List<int> orderedDates;

    private const string GameSaveFileName = "/TasksData";
    private const string FileExtension = ".dat";

    private int editID = -1; //-1 when not editing

    private void Awake()
    {
        tasksUIs = new Dictionary<int, GameObject>();
        tasks = new Dictionary<int, Task>();

        orderedDates = new List<int>();
        taskTemplate.SetActive(false);
    }

    private void Start()
    {
        LoadData();
    }

    public void RefreshTags()
    {
        foreach (Task task in tasks.Values)
        {
            task.RefreshTag();
        }
    }

    public void StopEdit()
    {
        editID = -1;
    }

    public void AddTask(string name, int day, int month, int year, string tagName)
    {
        //Creating new task
        if(editID < 0)
        {
            //create the new Task object
            GameObject taskUI = Instantiate(taskTemplate, content);
            //Task task = taskUI.GetComponent<Task>();
            Task task = taskUI.GetComponentInChildren<Task>();

            //apply variables
            task.Name = name;
            task.SetDate(day, month, year);
            task.SetTag(tagSelect.GetTag(tagName));
            task.ID = currentId;
            tasks.Add(currentId, task);

            //activate and Insert into date appropriate place
            taskUI.SetActive(true);
            taskUI.transform.SetSiblingIndex(PlaceTask(task.Date));
            tasksUIs.Add(currentId, taskUI);
            currentId++;
        }
        //Update an existing task
        else
        {
            GameObject taskUI = tasksUIs[editID];
            Task task = tasks[editID];

            //apply variables
            task.Name = name;
            task.SetDate(day, month, year);
            task.SetTag(tagSelect.GetTag(tagName));

            //activate and Insert into date appropriate place
            taskUI.SetActive(true);
            orderedDates.Remove(task.Date);
            taskUI.transform.SetSiblingIndex(PlaceTask(task.Date));

            StopEdit();
        }
    }

    public void Edit(int id)
    {
        editID = id;
        Task task = tasks[id];
        int[] date = task.GetDate();

        options.LoadTask(task.Name, date[0], date[1], date[2], task.TagName);
        screenController.SetScreen(ScreenUI.Create);
    }

    public int PlaceTask(int date)
    {
        int i = 0;
        if(orderedDates.Count == 0)
        {
            orderedDates.Add(date);
            return i;
        }
        for(i=0; i < orderedDates.Count; i++)
        {
            if(date < orderedDates[i])
            {
                orderedDates.Insert(i, date);
                return i;
            }
        }
        //append to end
        orderedDates.Add(date);
        return i;
    }

    public void RemoveTask(int id)
    {
        if (tasksUIs.ContainsKey(id))
        {
            GameObject taskUI = tasksUIs[id];
            Debug.Log(taskUI.name);
            orderedDates.Remove(tasks[id].Date);
            tasks.Remove(id);
            Destroy(taskUI);
            tasksUIs.Remove(id);
            SaveData();
        }
    }



    public void SaveData()
    {
        List<SaveData> saveData = new List<SaveData>();
       
        foreach(Task task in tasks.Values)
        {
            saveData.Add(task.GetSaveData());
        }

        Save(saveData.ToArray());
    }

    public void LoadData()
    {
        SaveData[] saveData = Load();
        if(saveData != null)
        {
            foreach(SaveData save in saveData)
            {
                AddTask(save.name, save.day, save.month, save.year, save.tagName);
            }
        }
    }

    private void Save(SaveData[] saveData)
    {
        string dataPath = Application.persistentDataPath + GameSaveFileName + FileExtension;
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Open(dataPath, FileMode.OpenOrCreate);

        try
        {
            binaryFormatter.Serialize(fileStream, saveData);
        }
        catch (System.Exception e)
        {
            Debug.Log("Serialization Failed: " + e.Message);
        }
        finally
        {
            //always close the fileStream
            fileStream.Close();
        }
    }

    private SaveData[] Load()
    {
        SaveData[] saveData = null;
        string dataPath = Application.persistentDataPath + GameSaveFileName + FileExtension;

        //make sure the file actually exists
        if (File.Exists(dataPath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(dataPath, FileMode.Open);
            try
            {
                saveData = (SaveData[])binaryFormatter.Deserialize(fileStream);
            }
            catch (System.Exception e)
            {
                Debug.Log("Game Deserialization Failed: " + e.Message);
            }
            finally
            {
                //always close the fileStream
                fileStream.Close();
            }
        }

        return saveData;
    }
}

[System.Serializable]
public class SaveData
{
    public string name;
    public int year;
    public int month;
    public int day;
    public string tagName;
}

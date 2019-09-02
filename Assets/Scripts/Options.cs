using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    /// <summary>
    /// handles the UI for creating a new Task
    /// </summary>

    [SerializeField] private TaskList taskList;
    [SerializeField] private ScreenController screenControl;

    [Space(10)]
    [SerializeField] private InputField nameInput;
    [SerializeField] private MonthSelect monthSelect;
    [SerializeField] private DaySelect daySelect;
    [SerializeField] private TagSelect tagSelect;

    /// <summary>
    /// Called by the DONE button to create a task
    /// </summary>
    public void Done()
    {
        //check if the input is valid
        if (Validate(nameInput.text, tagSelect.SelectedTag))
        {
            //add the task to the list
            taskList.AddTask(nameInput.text, 
                daySelect.SelectedDay, monthSelect.SelectedMonth, monthSelect.SelectedYear,
                tagSelect.SelectedTag.name);
            screenControl.ShowTaskScreen();

            taskList.SaveData();
        }
    }

    public bool Validate(string name, Tag tag)
    {
        if(name.Length == 0)
        {
            return false;
        }
        return true;
    }
}

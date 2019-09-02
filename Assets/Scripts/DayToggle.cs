using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayToggle : MonoBehaviour
{
    private static DaySelect daySelect;

    private Text textUI;
    private Toggle toggleUI;

    private void Awake()
    {
        if (daySelect == null)
        {
            daySelect = FindObjectOfType<DaySelect>();
        }
        
        toggleUI = GetComponent<Toggle>();
        textUI = GetComponentInChildren<Text>();

        toggleUI.onValueChanged.RemoveAllListeners();
        toggleUI.onValueChanged.AddListener(Toggled);
    }

    public void Toggled(bool tog)
    {
        if (tog)
        {
            daySelect.ToggleSelect(textUI, toggleUI);
        }
        
    }
}

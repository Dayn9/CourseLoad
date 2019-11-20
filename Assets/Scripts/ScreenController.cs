using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScreenUI { Task, Create, Settings, Info }

public class ScreenController : MonoBehaviour
{
    /// <summary>
    /// Handles the enabling and disabling of various screens and pop-ups
    /// </summary>

    [SerializeField] private GameObject taskScreen;
    [SerializeField] private GameObject createScreen;
    [SerializeField] private GameObject settingsScreen;

    public Action OnScreenChange;
    
    public ScreenUI CurrentScreen { get; private set; }

    private void Awake()
    {
        //activate all screens for setup
        taskScreen.SetActive(true);
        createScreen.SetActive(true);
        settingsScreen.SetActive(true);
    }

    private void Start()
    {
        SetScreen(ScreenUI.Task);
    }

    public void SetScreen(ScreenUI screen)
    {
        CurrentScreen = screen;

        taskScreen.SetActive(CurrentScreen == ScreenUI.Task);
        createScreen.SetActive(CurrentScreen == ScreenUI.Create);
        settingsScreen.SetActive(CurrentScreen == ScreenUI.Settings);

        //invoke when not null
        OnScreenChange?.Invoke();
    }

    //functions for button calls
    public void ShowSettingsScreen() { SetScreen(ScreenUI.Settings); }
    public void ShowCreateScreen() { SetScreen(ScreenUI.Create); }
}

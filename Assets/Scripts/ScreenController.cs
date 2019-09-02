using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    /// <summary>
    /// Handles the enabling and disabling of various screens and pop-ups
    /// </summary>

    [SerializeField] private GameObject taskScreen;
    [SerializeField] private GameObject createScreen;

    private void Awake()
    {
        taskScreen.SetActive(true);
        createScreen.SetActive(true);
    }

    private void Start()
    {
        ShowTaskScreen();
    }

    public void ShowTaskScreen()
    {
        taskScreen.SetActive(true);
        createScreen.SetActive(false);
    }

    public void ShowCreateScreen()
    {
        taskScreen.SetActive(false);
        createScreen.SetActive(true);
    }
}

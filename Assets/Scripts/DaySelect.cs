using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DaySelect : MonoBehaviour
{
    [SerializeField] private RectTransform[] rows;
    private List<Text[]> dayTexts;
    private List<Toggle[]> dayToggles;

    private int selectedMonth;
    private int selectedDay;


    private ToggleGroup toggleGroup;
    private int viewingMonth;

    public int SelectedDay {
        get { return selectedDay; }
        set { selectedDay = value; }
    }

    private void Awake()
    {
        dayTexts = new List<Text[]>();
        dayToggles = new List<Toggle[]>();

        toggleGroup = GetComponent<ToggleGroup>();

        foreach (RectTransform rect in rows)
        {
            //add all child rect the the 
            dayTexts.Add(rect.GetComponentsInChildren<Text>());
            Toggle[] toggles = rect.GetComponentsInChildren<Toggle>();
            dayToggles.Add(toggles);
            foreach (Toggle toggle in toggles)
            {
                toggle.group = toggleGroup;
            }
        }

        selectedDay = DateTime.Now.Day;
        selectedMonth = DateTime.Now.Month;
    }

    public void SetDays(int month, int year)
    {
        viewingMonth = month;

        int firstDayIndex = Zellercongruence(1, month, year);
        int lastDay = DateTime.DaysInMonth(year, month);

        int week = 0;
        int day = 1;
        //loop over every (row / week) 
        for(week = 0; week < dayTexts.Count; week++)
        {
            //loop over every day in the week
            for (int i = 0; i < dayTexts[week].Length; i++)
            {
                //disable selected
                dayToggles[week][i].interactable = false;
                dayToggles[week][i].enabled = true;
                dayToggles[week][i].isOn = false;
                //set blank in first week before first day
                if (week == 0 && i < firstDayIndex)
                {
                    dayTexts[week][i].text = "   ";
                    dayToggles[week][i].enabled = false;
                }
                //set day while still not at the last day
                else if (day <= lastDay)
                {
                    dayTexts[week][i].text = (day < 10 ? " " : "") + day;
                    dayToggles[week][i].interactable = true;
                    

                    //select the toggle if it was previously selected
                    if (selectedMonth == month && selectedDay == day)
                    {
                        dayToggles[week][i].Select();
                        dayToggles[week][i].isOn = true;
                    }

                    day++;
                }
                else
                {
                    dayTexts[week][i].text = "   ";
                    dayToggles[week][i].enabled = false;
                }
            }
        }       
    }

    public void ToggleSelect(Text displayText, Toggle displayToggle)
    {
        selectedMonth = viewingMonth;
        int.TryParse(displayText.text, out selectedDay);
    }

    private int Zellercongruence(int day, int month, int year)
    {
        if (month == 1)
        {
            month = 13;
            year--;
        }
        if (month == 2)
        {
            month = 14;
            year--;
        }
        int q = day;
        int m = month;
        int k = year % 100;
        int j = year / 100;
        int h = q + 13 * (m + 1) / 5 + k + k / 4
                                 + j / 4 + 5 * j;
        h = h % 7;

        return h;
    }
}

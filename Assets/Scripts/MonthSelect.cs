﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum Month {
    January, February, March, April, May, June, July, August, September, October, November, December
}

public class MonthSelect : MonoBehaviour
{
    [SerializeField] private Text monthText;
    [SerializeField] private DaySelect daySelect;

    public static  Month[] months = new Month[]
    {
        Month.January,
        Month.February,
        Month.March,
        Month.April,
        Month.May,
        Month.June,
        Month.July,
        Month.August,
        Month.September,
        Month.October,
        Month.November,
        Month.December
    };

    private int selectedMonth;
    private int selectedYear;

    public int SelectedMonth { get { return selectedMonth + 1; } }
    public int SelectedYear { get { return selectedYear; } }

    private void Awake()
    {
        selectedMonth = DateTime.Now.Month - 1; //adjust to zero index
        selectedYear = DateTime.Now.Year;
    }

    private void Start()
    {
        UpdateUI();
    }

    public void Next()
    {
        selectedMonth += 1;
        if(selectedMonth == 12)
        {
            selectedYear++;
            selectedMonth = 0;
        }
        UpdateUI();
    }

    public void Prev()
    {
        selectedMonth -= 1;
        if(selectedMonth == -1)
        {
            selectedYear--;
            selectedMonth = 11;
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        monthText.text = months[selectedMonth].ToString().Substring(0, 3) + " " + selectedYear;
        daySelect.SetDays(selectedMonth + 1, selectedYear);
    }
}

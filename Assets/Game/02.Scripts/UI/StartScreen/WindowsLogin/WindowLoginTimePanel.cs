using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WindowLoginTimePanel : MonoBehaviour
{
    public TMP_Text timeText;
    public TMP_Text dateText;
    private DateTime dateTime;

    [SerializeField]
    string hourText;
    [SerializeField]
    string minuteText;

    void Start()
    {
        // TODO: ETimeEvent
        // EventManager.StartListening(ETimeEvent.ChangeTime, SetDateTime);
        // SetDateTime(new object[] { Constant.DEFAULTDATE });
    }

    public void SetDateTime(object[] ps)
    {
        if (!(ps[0] is System.DateTime))
        {
            return;
        }

        dateTime = (System.DateTime)ps[0];

        if (dateTime.Hour < 10)
        {
            hourText = "0" + dateTime.Hour.ToString();
        }
        else
        {
            hourText = dateTime.Hour.ToString();
        }

        if (dateTime.Minute < 10)
        {
            minuteText = "0" + dateTime.Minute.ToString();
        }
        else
        {
            minuteText = dateTime.Minute.ToString();
        }

        SetText();
    }

    private void SetText()
    {
        timeText.text = hourText + ":" + minuteText;
        dateText.text = dateTime.Month + "�� " + dateTime.Day + "�� " + GetDayText(dateTime.DayOfWeek);
    }

    private string GetDayText(DayOfWeek week)
    {
        switch(week)
        {
            case DayOfWeek.Monday:
                return "������";
            case DayOfWeek.Tuesday:
                return "ȭ����";
            case DayOfWeek.Wednesday:
                return "������";
            case DayOfWeek.Thursday:
                return "�����";
            case DayOfWeek.Friday:
                return "�ݿ���";
            case DayOfWeek.Saturday:
                return "�����";
            case DayOfWeek.Sunday:
                return "�Ͽ���";
        }
        return null;
    }
}

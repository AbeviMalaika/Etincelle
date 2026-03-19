using System;
using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;

public class TimeDisplay : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public bool display;
    public enum TypeTemps { heure, date, dateClassique }
    public TypeTemps typeTemps;

    void Start()
    {
        InvokeRepeating("UpdateTimeRoutine", 0.2f, 0.2f);
    }

    void UpdateTimeRoutine()
    {
        DateTime now = DateTime.Now;
        string formattedDate;

        if (display)
        {
            if (typeTemps == TypeTemps.heure)
            {
                formattedDate = now.ToString("HH:mm");

                timeText.text = formattedDate;
            }

            if (typeTemps == TypeTemps.date)
            {
                CultureInfo culture = new CultureInfo("fr-CA");

                string rawDate = now.ToString("dddd d MMMM", culture);

                formattedDate = char.ToUpper(rawDate[0]) + rawDate.Substring(1);

                timeText.text = formattedDate;
            }

            if (typeTemps == TypeTemps.dateClassique)
            {
                formattedDate = now.ToString("yyyy-MM-dd");

                timeText.text = formattedDate;
            }
        }
    }
}

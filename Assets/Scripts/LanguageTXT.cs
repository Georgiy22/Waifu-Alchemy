using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanguageTXT : MonoBehaviour
{
    public string _en;
    public string _ru;

    public void OnStart()
    {
        if (Language.Instance.CurrentLan == "ru")
        {
            GetComponent<TextMeshProUGUI>().text = _ru;
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = _en;
        }
    }
}
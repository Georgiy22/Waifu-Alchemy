using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Language : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern string GetLang();

    public string CurrentLan;

    public static Language Instance;

    public void LangAwake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
#if !UNITY_EDITOR
            CurrentLan = GetLang();
#endif
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

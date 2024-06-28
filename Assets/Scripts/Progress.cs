using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class PlayerInfo
{
    public bool[] OpenedElements = new bool[101];
}

public class Progress : MonoBehaviour
{
    public static PlayerInfo Info = new PlayerInfo();

    [DllImport("__Internal")]
    private static extern void SaveExtern(string date);
    [DllImport("__Internal")]
    private static extern void LoadExtern();

    public static Progress Instance;

    public void OnAwake()
    {
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Load()
    {
#if !UNITY_EDITOR
            LoadExtern();
#endif
    }
    public void Save()
    {
        #if !UNITY_EDITOR
            string jsonString = JsonUtility.ToJson(Info);
            SaveExtern(jsonString);
        #endif
    }

    public void SetPlayerInfo(string value)
    {
        Info = JsonUtility.FromJson<PlayerInfo>(value);
        Backpack.Instance.UnlockLoaded();
    }
}
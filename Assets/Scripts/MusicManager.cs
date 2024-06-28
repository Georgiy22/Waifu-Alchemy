using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public Scrollbar sb;
    private bool play = true;
    private bool Nadv = true;
    private bool toggle = true;
    public static MusicManager Instance;
    public AudioClip[] clips = new AudioClip[3];
    public TMP_Dropdown MusicDD;
    public void MusicManagerAwake()
    {
        Instance = this;
    }
    public void MusicButtonsChangeLang()
    {
        if (Language.Instance.CurrentLan == "ru")
        {
            MusicDD.options.Add(new TMP_Dropdown.OptionData("Мелодия 1"));
            MusicDD.options.Add(new TMP_Dropdown.OptionData("Мелодия 2"));
            MusicDD.options.Add(new TMP_Dropdown.OptionData("Мелодия 3"));
        }
        else
        {
            MusicDD.options.Add(new TMP_Dropdown.OptionData("Melody 1"));
            MusicDD.options.Add(new TMP_Dropdown.OptionData("Melody 2"));
            MusicDD.options.Add(new TMP_Dropdown.OptionData("Melody 3"));
        }
    }
    private void MusicToggle(bool b)
    {
        GetComponent<AudioSource>().mute = !b;
    }
    public void MuteToggle()
    {
        MusicToggle(!toggle);
        play = !toggle;
        toggle = !toggle;
        if (!toggle)
        {
            sb.value = 0f;
        } else
        {
            sb.value = GetComponent<AudioSource>().volume;
        }
    }

    public void MuteToggle(bool b)
    {
        Nadv = b;
        if (toggle)
        {
            MusicToggle(b);
            play = b;
        }
    }
    public void AdMute()
    {
        MuteToggle(true);
    }
    public void VolumeToggle()
    {
        if (toggle)
        {
            GetComponent<AudioSource>().volume = sb.value;
        } else
        {
            sb.value = 0f;
        }
    }
    void OnApplicationFocus(bool hasFocus)
    {
        if ((play == !hasFocus) && (Nadv))
        {
            MuteToggle(!play);
            Nadv = true;
        }
    }
    void OnApplicationPause(bool pauseStatus)
    {
        if ((play == pauseStatus) && (Nadv))
        {
            MuteToggle(!play);
            Nadv = true;
        }
    }
    public void OnMusicChange()
    {
        GetComponent<AudioSource>().clip = clips[MusicDD.value];
        GetComponent<AudioSource>().Play();
        if (!toggle)
        {
            MusicToggle(false);
        }
    }
}

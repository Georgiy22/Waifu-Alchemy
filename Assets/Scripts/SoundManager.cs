using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioClip CollapseSound; //объединение элементов0
    public AudioClip SelectSound; //выбор элемента1
    public AudioClip DeleteSound; //удаление элемента2
    public AudioClip ThrowSound; //отпускание элемента3
    public AudioClip CreateSound; //создание элемента4
    public AudioClip NewOpenSound; //открытие нового элемента5
    public Scrollbar SoundEffectScrollbar;
    public static SoundManager Instance;
    public AudioSource SoundsForButtons;
    public void OnAwake()
    {
        Instance = this;
    }
    public void PlaySound(string s)
    {
        switch (s)
        {
            case "collapse":
                GetComponent<AudioSource>().clip = CollapseSound;
                break;
            case "select":
                GetComponent<AudioSource>().clip = SelectSound;
                break;
            case "delete":
                GetComponent<AudioSource>().clip = DeleteSound;
                break;
            case "throw":
                GetComponent<AudioSource>().clip = ThrowSound;
                break;
            case "create":
                GetComponent<AudioSource>().clip = CreateSound;
                break;
            case "newopen":
                GetComponent<AudioSource>().clip = NewOpenSound;
                break;
            default:
                GetComponent<AudioSource>().clip = SelectSound;
                break;
        }
        GetComponent<AudioSource>().Play();
    }
    public void VolumeToggle()
    {
        GetComponent<AudioSource>().volume = SoundEffectScrollbar.value;
        SoundsForButtons.volume = SoundEffectScrollbar.value;
    }
}

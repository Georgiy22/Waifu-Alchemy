using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Elements;

public class Backpack : MonoBehaviour
{
    public Canvas BackpackSpace;
    public Image BackpackImage;
    public Button ButtonBackpack;
    public TMP_Dropdown BackpackDropDown;
    public GameObject FinalImageFrame;

    private byte[] OpenedIndexes = new byte[101];
    public bool[] OpenedElementsBP = new bool[101];

    public static Backpack Instance;
    public void BackpackTrigger() 
    { 
        PlayField.Instance.CanvasField.SetActive(!PlayField.Instance.CanvasField.activeSelf);
        BackpackSpace.gameObject.SetActive(!BackpackSpace.gameObject.activeSelf); 
    }
    public int getBPDDValue() 
    {
        return (int)OpenedIndexes[BackpackDropDown.value];
    }
    public void OnAwake()
    {
        Instance = this;
    }
    public void OnStart()
    {
        Progress.Info.OpenedElements[0] = true;
        Progress.Info.OpenedElements[1] = true;
        Progress.Info.OpenedElements[2] = true;
        Progress.Info.OpenedElements[3] = true;
        AddNewElement(0);
        AddNewElement(1);
        AddNewElement(2);
        AddNewElement(3);
    }
    public void UnlockLoaded()
    {
        int[] str = new int[Progress.Info.OpenedElements.Length];
        int j = 0;
        for (int i = 4; i < Progress.Info.OpenedElements.Length; i++)
        {
            if (Progress.Info.OpenedElements[i])
            {
                str[j] = i;
                j++;
            }
        }
        if (j > 0)
        {
            for (int i = 0; i < j; i++)
            {
                char[] a = Elements.elements[str[i]].getTitle().ToCharArray();
                for (int k = i; k < j; k++)
                {
                    char[] b = Elements.elements[str[k]].getTitle().ToCharArray();
                    if (b[0] < a[0])
                    {
                        int tmp = str[i];
                        str[i] = str[k];
                        str[k] = tmp;
                        a = Elements.elements[str[i]].getTitle().ToCharArray();
                    } else
                    {
                        if ((b[0] == a[0]) && (a.Length > 1) && (b.Length > 1))
                        {
                            if (b[1] < a[1])
                            {
                                int tmp = str[i];
                                str[i] = str[k];
                                str[k] = tmp;
                                a = Elements.elements[str[i]].getTitle().ToCharArray();
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < j; i++)
            { 
                AddNewElement(str[i]);
            }
        }
    }
    private void AddNewElement(int index)
    {
        OpenedIndexes[BackpackDropDown.options.Count] = (byte)index;
        TMP_Dropdown.OptionData tmp = new TMP_Dropdown.OptionData(Elements.elements[index].getTitle(), Sprite.Create(Images.Instance.images[index], new Rect(1, 1, 254, 254), new Vector2(0.5f, 0.5f), PlayField.unit));
        BackpackDropDown.options.Add(tmp);
        OpenedElementsBP[index] = true;
    }
    public void OpenNewElement(int index)
    {
        if (!Progress.Info.OpenedElements[index])
        {
            Progress.Info.OpenedElements[index] = true;
            Progress.Instance.Save();
            FindAnyObjectByType<Yandex>().RenewLeaderboard();
            SoundManager.Instance.PlaySound("newopen");
            AddNewElement(index);
        }
    }
    public void OnFinalCheck()
    {
        FinalImageFrame.SetActive(Elements.elements[getBPDDValue()].getIsFinal());
    }
}

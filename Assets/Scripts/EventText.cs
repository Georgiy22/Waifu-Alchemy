using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventText : MonoBehaviour
{
    public static EventText Instance;
    public void OnAwake()
    {
        Instance = this;
    }
    private void setText(string s)
    {
        GetComponent<TextMeshProUGUI>().text = s;
    }
    public void writeSelectedElement(int index)
    {
        if (Language.Instance.CurrentLan == "ru")
        {
            setText("выделен элемент \"" + Elements.elements[index].getTitle() + "\"");
        }
        else
        {
            setText("\"" + Elements.elements[index].getTitle() + "\" element selected");
        }
    }
    public void writeCreatedElement(int index)
    {
        if (Language.Instance.CurrentLan == "ru")
        {
            setText("создан элемент \"" + Elements.elements[index].getTitle() + "\"");
        } else
        {
            setText("\"" + Elements.elements[index].getTitle() + "\" element created");
        }
    }
    public void writeHint(int index)
    {
        if(index == -1)
        {
            if (Language.Instance.CurrentLan == "ru")
            {
                setText("все элементы открыты!");
            }
            else
            {
                setText("all elements are open!");
            }
        }
        else
        {
            if (Language.Instance.CurrentLan == "ru")
            {
                setText("подсказка: близок к открытию элемент \"" + Elements.elements[index].getTitle() + "\"");
            }
            else
            {
                setText("hint: close to be open\"" + Elements.elements[index].getTitle() + "\" element");
            }
        }
    }
    public void clearText()
    {
        setText("");
    }
}

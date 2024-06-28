using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Images : MonoBehaviour
{
    public static Images Instance;
    public Texture2D[] images = new Texture2D[101];
    public Sprite HTP_ru;
    public Sprite HTP_en;

    public Sprite getHTP()
    {
        if (Language.Instance.CurrentLan == "ru")
        {
            return HTP_ru;
        }
        else
        {
            return HTP_en;
        }
    }
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
}

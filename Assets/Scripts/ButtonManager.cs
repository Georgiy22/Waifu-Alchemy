using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    //public Button ClearButton;
    //public Button HowToPlayButton;
    //public Button SettingsButton;
    public Canvas CanvasHTP;
    public Canvas CanvasSettings;
    public Scrollbar UnitScalerScrollbar;
    public GameObject AuthButton;
    public GameObject AuthText;
    public GameObject LikeButton;
    public GameObject StubMenu;
    public static ButtonManager Instance;
    public Image ImageHTP;
    private bool AwakeFlag = false;

    public void OnAwake()
    {
        Instance = this;
    }
    public void OnStart()
    {
        OnAuth(false);
        ImageHTP.sprite = Images.Instance.getHTP();
        AwakeFlag = true;
    }
    public void StubPlay()
    {
        if (AwakeFlag)
        {
            Progress.Instance.Load();
            StubMenu.gameObject.SetActive(false);
            PlayField.Instance.Invoke();
        }
    }
    public void OnAuth(bool b)
    {
        AuthButton.SetActive(!b);
        AuthText.SetActive(!b);
        LikeButton.SetActive(b);
    }
    public void OnClearButton()
    {
        PlayField.Instance.ClearField();
        EventText.Instance.clearText();
    }
    public void OnHTPButton()
    {
        PlayField.Instance.CanvasField.SetActive(CanvasHTP.gameObject.activeSelf);
        Backpack.Instance.BackpackSpace.gameObject.SetActive(false);
        CanvasSettings.gameObject.SetActive(false);
        CanvasHTP.gameObject.SetActive(!CanvasHTP.gameObject.activeSelf);
    }
    public void OnSettingsButton()
    {
        PlayField.Instance.CanvasField.SetActive(CanvasSettings.gameObject.activeSelf);
        Backpack.Instance.BackpackSpace.gameObject.SetActive(false);
        CanvasHTP.gameObject.SetActive(false);
        CanvasSettings.gameObject.SetActive(!CanvasSettings.gameObject.activeSelf);
    }
    public void OnUnitScalerScrollbar()
    {
        PlayField.unitScaler = UnitScalerScrollbar.value + 0.6f;
        PlayField.Instance.scaleGenerate();
        OnClearButton();
    }
}

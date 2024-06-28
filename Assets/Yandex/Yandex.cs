using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Yandex : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void AuthExtern();

    [DllImport("__Internal")]
    private static extern void RateGame();

    [DllImport("__Internal")]
    private static extern void SetToLeaderboard(int value);

    [DllImport("__Internal")]
    private static extern void ShowAdv();

    [DllImport("__Internal")]
    private static extern void SimplifyExtern();

    public static Yandex Instance;
    private bool CoroutineFlag = false;
    public void AllAwake()
    {
        Instance = this;
        FindAnyObjectByType<Language>().LangAwake();
        FindAnyObjectByType<MusicManager>().MusicManagerAwake();
        FindAnyObjectByType<ButtonManager>().OnAwake();
        FindAnyObjectByType<Progress>().OnAwake();
        FindAnyObjectByType<Backpack>().OnAwake();
        FindAnyObjectByType<PlayField>().OnAwake();
        FindAnyObjectByType<EventText>().OnAwake();
        FindAnyObjectByType<Images>().OnAwake();
        FindAnyObjectByType<SoundManager>().OnAwake();
        FindAnyObjectByType<Elements>().OnAwake();
        FindAnyObjectByType<Combinations>().OnAwake();
        //Start
        Backpack.Instance.BackpackSpace.gameObject.SetActive(true);
        ButtonManager.Instance.CanvasHTP.gameObject.SetActive(true);
        ButtonManager.Instance.CanvasSettings.gameObject.SetActive(true);
        ButtonManager.Instance.AuthButton.SetActive(true);
        ButtonManager.Instance.LikeButton.SetActive(true);
        foreach (LanguageTXT b in FindObjectsByType<LanguageTXT>(FindObjectsSortMode.None))
        {
            b.OnStart();
        }
        ButtonManager.Instance.AuthButton.SetActive(false);
        ButtonManager.Instance.LikeButton.SetActive(false);
        Backpack.Instance.BackpackSpace.gameObject.SetActive(false);
        ButtonManager.Instance.CanvasHTP.gameObject.SetActive(false);
        ButtonManager.Instance.CanvasSettings.gameObject.SetActive(false);

        MusicManager.Instance.MusicButtonsChangeLang();
        ButtonManager.Instance.OnStart();
        Backpack.Instance.OnStart();
    }
    private void Awake()
    {
#if UNITY_EDITOR
        AllAwake();
#endif
    }
    public void ShowAD()
    {
#if !UNITY_EDITOR
        MusicManager.Instance.MuteToggle(false);
        ShowAdv();
#endif
    }
    public void OnCheckAuth()
    {
        ButtonManager.Instance.OnAuth(true);
    }
    public void RateButton()
    {
        RateGame();
    }
    public void AuthButton()
    {
        AuthExtern();
    }
    public void RenewLeaderboard()
    {
        //UNITY_WEBGL____
#if !UNITY_EDITOR
        int length = 0;
        foreach(bool b in Progress.Info.OpenedElements)
        {
            if (b)
            {
                length++;
            }
        }
        SetToLeaderboard(length);
#endif
    }
    public void SimplifyBut()
    {
        if (!CoroutineFlag)
        {
            CoroutineFlag = true;
            StartCoroutine(SimpButCoroutine());
        }
    }
    IEnumerator SimpButCoroutine()
    {
#if !UNITY_EDITOR
        MusicManager.Instance.MuteToggle(false);
        SimplifyExtern();
#endif
        yield return new WaitForSeconds(1.5f);
        CoroutineFlag = false;
    }
    public void SimplifyReward()
    {
        int[] nearest5 = new int[5];
        int nearest_index = 0;
        for (int i = 0; (i < Backpack.Instance.OpenedElementsBP.Length) && nearest_index < 5; i++)
        {
            if (!Backpack.Instance.OpenedElementsBP[i])
            {
                nearest5[nearest_index] = i;
                nearest_index++;
            }
        }
        if(nearest_index > 0)
        {
            EventText.Instance.writeHint(nearest5[UnityEngine.Random.Range(0, nearest_index)]);
        }
        else
        {
            EventText.Instance.writeHint(-1);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Reflection;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Net.Mime.MediaTypeNames;
using Unity.VisualScripting;
using System;
using System.Runtime.CompilerServices;
using TMPro;
using Image = UnityEngine.UI.Image;
using static UnityEngine.UI.CanvasScaler;
using Text = UnityEngine.UI.Text;

public class PhysicalElements
{
    public int index;
    public string title;
    public GameObject body;
    public PhysicalElements(int index)
    {
        this.index = index;
        this.title = Elements.elements[index].getTitle();
        PlayField.elementCounter++;
        GameObject obj = new GameObject("Element " + PlayField.elementCounter);
        SpriteRenderer ren = obj.AddComponent<SpriteRenderer>();
        ren.sprite = Sprite.Create(Images.Instance.images[index], new Rect(1, 1, 254, 254), new Vector2(0.5f, 0.5f), PlayField.unit);
        ren.sortingOrder = -1;
        obj.AddComponent<BoxCollider2D>();
        obj.tag = "Element";
        this.body = obj;
    }
}
public class PlayField : MonoBehaviour
{
    public TMP_InputField InputField;
    public Image[] SideImages = new Image[4];

    public static int elementCounter = 0;
    public static int unit;
    public static float unitScaler = 1f;
    private Vector3 offset;
    private Transform current;
    private PhysicalElements currentPE;
    private float offsetZ = 1f;
    private float scaleH, scaleW, dispersion;
    private int prevH, prevW;
    private bool IsCollide = false;
    private int CollideResult, CollideCounter;
    private Transform[] Collidable = new Transform[2];
    private float xStep,yStep;
    private int[] SideImagesIndex = new int[4] {0,1,2,3};
    private bool CoroutineFlag = false;
    private bool StartFlag = false;

    public const int ResolutionWidth = 1600, ResolutionHeight = 900;

    private List<PhysicalElements> FieldElements = new List<PhysicalElements>();
    public static PlayField Instance;
    public GameObject CanvasField;
    
    public void OnAwake()
    {
        Instance = this;
        scaleGenerate();
        //UnityEngine.Debug.Log(1);
    }
    public void Invoke()
    {
        AddElement(0, dispersion, dispersion);
        AddElement(1, dispersion, -dispersion);
        AddElement(2, -dispersion, dispersion);
        AddElement(3, -dispersion, -dispersion);
        StartFlag = true;
    }
    public void scaleGenerate()
    {
        if (StartFlag)
        {
            foreach (Image im in SideImages)
            {
                im.transform.position = new Vector3(im.transform.position.x, im.transform.position.y, 89.5f);
            }
            Backpack.Instance.ButtonBackpack.transform.position = new Vector3(Backpack.Instance.ButtonBackpack.transform.position.x, Backpack.Instance.ButtonBackpack.transform.position.y, 89f);
        }
        prevH = Screen.height;
        prevW = Screen.width;
        float res = (float)ResolutionWidth / ResolutionHeight;
        float tmpRes = (float)Screen.width / (Screen.height * res);
        if (tmpRes < 1)
        {
            unit = (int)(100f * unitScaler / tmpRes);
            scaleH = 5f * tmpRes - (128f / unit); //!
            scaleW = 5f * tmpRes * res - (128f / unit);
            dispersion = tmpRes * res;
        }
        else
        {
            unit = (int)(100f * unitScaler);
            scaleH = 5f - (128f / unit);
            scaleW = 5f * res - (128f / unit);
            dispersion = res;
        }
    }
    public void AddElement()
    {
        if(InputField.text.Length > 0)
        {
            Int32.TryParse(InputField.text, out int tmp);
            AddElement(tmp);
        }
    }
    public void AddElement(int index)
    {
        AddElement(index, 0, 0);
    }
    public Transform AddElement(int index, float x, float y)
    {
        if (index >= 0)
        {
            PhysicalElements pe = new PhysicalElements(index);
            pe.body.transform.SetParent(CanvasField.transform);
            offsetZ++;
            pe.body.transform.position = new Vector3(x, y, 89f - 0.001f * offsetZ);
            FieldElements.Add(pe);
            if (!current)
            {
                currentPE = pe;
            }
            return pe.body.transform;
        }
        return null;
    }
    public void ClearField()
    {
        if (IsCollide)
        {
            ElementCollideCollapse();
        }
        elementCounter = 0;
        offsetZ = 1f;
        foreach(PhysicalElements pe in FieldElements)
        {
            Destroy(pe.body);
        }
        FieldElements.Clear();
    }
    private PhysicalElements GetElementByName(string s)
    {
        foreach (PhysicalElements child in FieldElements)
        {
            if (child.body.transform.name == s)
            {
                return child;
            }
        }
        return null;
    }
    public void CheckCombination()
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(new Vector2(current.position.x, current.position.y), Vector2.zero);
        if (hit.Length > 1 && (hit[0].transform.tag == "Element"))
        {
            switch (hit[1].transform.tag)
            {
                case "Element":
                    PhysicalElements hit1PE = GetElementByName(hit[1].transform.name);
                    int tmp = FindAnyObjectByType<Combinations>().checkComb(currentPE.index, hit1PE.index);
                    if (tmp >= 0)
                    {
                        if (IsCollide)
                        {
                            ElementCollideCollapse();
                        }
                        IsCollide = true;
                        Collidable[0] = hit[0].transform;
                        Collidable[1] = hit[1].transform;
                        Collidable[0].transform.tag = "Collidable";
                        Collidable[1].transform.tag = "Collidable";
                        xStep = (Collidable[0].position.x - Collidable[1].position.x) / 100f;
                        yStep = (Collidable[0].position.y - Collidable[1].position.y) / 100f;
                        CollideResult = tmp;
                        CollideCounter = 30;
                        FieldElements.Remove(currentPE);
                        FieldElements.Remove(hit1PE);
                    }
                    break;
                case "Backpack":
                    FieldElements.Remove(currentPE);
                    Destroy(current.gameObject);
                    current = null;
                    SoundManager.Instance.PlaySound("delete");
                    break;
                case "SideImage":
                    SideImages[getSideImageIndex(hit[1].transform.name)].sprite = Sprite.Create(Images.Instance.images[currentPE.index], new Rect(0, 0, 255, 255), new Vector2(0.5f, 0.5f), unit);
                    SideImagesIndex[getSideImageIndex(hit[1].transform.name)] = currentPE.index;
                    FieldElements.Remove(currentPE);
                    Destroy(current.gameObject);
                    current = null;
                    SoundManager.Instance.PlaySound("throw");
                    break;
                default:
                    break;
            }
        }
    }
    private int getSideImageIndex(string s)
    {
        int SI = -1;
        switch (s)
        {
            case "SideImage0":
                SI = 0;
                break;
            case "SideImage1":
                SI = 1;
                break;
            case "SideImage2":
                SI = 2;
                break;
            case "SideImage3":
                SI = 3;
                break;
            default:
                break;
        }
        return SI;
    }
    private void ElementCollide()
    {
        if (CollideCounter < 1)
        {
            ElementCollideCollapse();
        }
        else { 
            float offsetX, offsetY;
            offsetX = Collidable[0].position.x - xStep;
            offsetY = Collidable[0].position.y - yStep;
            Collidable[0].position = new Vector3(offsetX, offsetY, Collidable[0].position.z);
            offsetX = Collidable[1].position.x + xStep;
            offsetY = Collidable[1].position.y + yStep;
            Collidable[1].position = new Vector3(offsetX, offsetY, Collidable[1].position.z);
            CollideCounter--;
        }
    }
    private void ElementCollideCollapse()
    {
        SoundManager.Instance.PlaySound("collapse");
        Backpack.Instance.OpenNewElement(CollideResult);
        EventText.Instance.writeCreatedElement(CollideResult);
        AddElement(CollideResult, Collidable[0].position.x, Collidable[0].position.y);
        Destroy(Collidable[0].gameObject);
        Destroy(Collidable[1].gameObject);
        IsCollide = false;
    }
    void Update()
    {
        if((prevW != Screen.width) || (prevH != Screen.height))
        {
            if (!CoroutineFlag)
            {
                CoroutineFlag = true;
                StartCoroutine(ExampleCoroutine());
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            GetElement();
        }
        else if (Input.GetMouseButtonUp(0) && current)
        {
            CheckCombination();
            if(current != null)
            {
                if (current.position.x < -scaleW * 0.65)
                {
                    current.position = new Vector3(current.position.x * 0.65f, current.position.y, current.position.z);
                }
                current = null;
            }
        }
        if (current)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = position.x + offset.x;
            float y = position.y + offset.y;
            if (x < -scaleW) { x = -scaleW; }
            if (x > scaleW*0.65f) { x = scaleW*0.65f; }
            if (y < -scaleH) { y = -scaleH; }
            if (y > scaleH) { y = scaleH; }
            current.position = new Vector3(x, y, current.position.z);
            //new WaitForSeconds(0.1f);
        }
        if (IsCollide)
        {
            ElementCollide();
        }
    }
    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(0.3f);
        scaleGenerate();
        CoroutineFlag = false;
    }
    void GetElement()
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.Length > 0)
        {
            switch (hit[0].transform.tag)
            {
                case "Element":
                    offset = hit[0].transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    current = hit[0].transform;
                    offsetZ++;
                    hit[0].transform.position = new Vector3(current.position.x, current.position.y, 89f - 0.001f * offsetZ);
                    currentPE = GetElementByName(hit[0].transform.name);
                    EventText.Instance.writeSelectedElement(currentPE.index);
                    SoundManager.Instance.PlaySound("select");
                    break;
                case "Backpack":
                    if (CanvasField.activeSelf)
                    {
                        Backpack.Instance.BackpackTrigger();
                        Yandex.Instance.ShowAD();
                    }
                    break;
                case "SideImage":
                    if (CanvasField.activeSelf)
                    {
                        EventText.Instance.writeSelectedElement(SideImagesIndex[getSideImageIndex(hit[0].transform.name)]);
                        current = AddElement(SideImagesIndex[getSideImageIndex(hit[0].transform.name)], Input.mousePosition.x, Input.mousePosition.y);
                        offset = hit[0].transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        SoundManager.Instance.PlaySound("create");
                    }
                    break;
                case "BackpackImage":
                    int tmp = Backpack.Instance.getBPDDValue();
                    Backpack.Instance.BackpackTrigger();
                    EventText.Instance.writeSelectedElement(tmp);
                    current = AddElement(tmp, Input.mousePosition.x, Input.mousePosition.y);
                    offset = hit[0].transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    SoundManager.Instance.PlaySound("create");
                    break;
                default:
                    break;
            }
        }
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Plan : MonoBehaviour
{

    [SerializeField]
    public Camera mainCamera;

    [SerializeField]
    public GameObject plenCamera;

    [SerializeField]
    public GameObject toRouCamera;

    [SerializeField]
    public Roulette rotScript;

    [SerializeField]
    public GameObject mainUI;

    [SerializeField]
    public List<GameObject> nObj = new List<GameObject>();
    [SerializeField]
    public List<Image> s = new List<Image>();

    public List<string> inN = new List<string>();

    public List<GameObject> betsChips = new List<GameObject>();

    [SerializeField]
    public List<GameObject> perChips = new List<GameObject>();

    [SerializeField]
    public List<Image> pImage = new List<Image>();

    [SerializeField]
    public List<TextMeshProUGUI> pText = new List<TextMeshProUGUI>();

    public float sTime = 0;

    public int temCount = 0;
    public int temMulti = 0;

    public int temTotal = 0;

    public Ball ball;

    public List<int> temArea = new List<int>();


    [SerializeField]
    public TextMeshProUGUI baseText;
    [SerializeField]
    public TextMeshProUGUI bankRollText;
    [SerializeField]
    public TextMeshProUGUI betText;

    [SerializeField]
    public Text baseChangBgText;

    [SerializeField]
    GameObject lookTab;

    [SerializeField]
    Camera lookBall;

    [SerializeField]
    GameObject openTab;

    [SerializeField]
    GameObject closeTab;

    [SerializeField]
    GameObject totalTab;

    [SerializeField]
    TextMeshProUGUI totalTabText;

    bool onTotal;

    [SerializeField]
    GameObject titleTab;


    bool doLook = false;

    [SerializeField]
    Button playButton;

    [SerializeField]
    Button clearButton;

    [SerializeField]
    Button baseButton;



    int[] redArray = new int[18] { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
    int[] blackArray = new int[18] { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };

    private void Awake()
    {
        for (int i = 0; i < 37; i++)
        {
            temArea.Add(new int());
        }
    }


    public void allBet()
    {
        temCount = 0;
        for (int i = 0; i < 37; i++)
        {
            temArea[i] = 0;
        }

        foreach (var e in betsChips)
        {
            temCount += e.GetComponent<Chips>().count;

            for (int i = 0; i < e.GetComponent<Chips>().batArea.Count; i++)
            {
                temArea[e.GetComponent<Chips>().batArea[i]] += e.GetComponent<Chips>().multi * e.GetComponent<Chips>().count;
            }
        }
    }



    public void BaseChang()
    {
        if (Int32.Parse(baseChangBgText.text) * temCount <= Int32.Parse(bankRollText.text))
        {
            int temp = (Int32.Parse(baseText.text));
            baseText.text = baseChangBgText.text;
            if (baseText.text == "") { baseText.text = "0"; }
            if (temCount == 0 && Int32.Parse(baseText.text) > Int32.Parse(bankRollText.text)) { baseText.text = baseChangBgText.text; }
            betText.text = (Int32.Parse(baseText.text) * temCount).ToString();
            if (Int32.Parse(betText.text) > Int32.Parse(bankRollText.text))
            {
                baseText.text = temp.ToString();
                betText.text = (Int32.Parse(baseText.text) * temCount).ToString();
            }
        }
    }

    public Image[] AllS(string[] sN)
    {
        Image[] ret = new Image[sN.Length];
        for (int i = 0; i < sN.Length; i++)
        {
            ret[i] = s[Int32.Parse(sN[i])];
        }
        return ret;
    }

    public void toExit()
    {
        Application.Quit();
    }


    public void toTit()
    {
        mainCamera.orthographic = false;
        plenCamera.SetActive(false);
        mainUI.SetActive(false);
        titleTab.SetActive(true);

    }

    public void toPlen()
    {
        rotScript.speed = 0.1f;
        titleTab.SetActive(false);
        mainCamera.orthographic = true;
        plenCamera.SetActive(true);
        mainUI.SetActive(true);

        if (bankRollText.text == "0")
        {
            bankRollText.text = "10000";
            baseText.text = "1000";
        }
    }


    public void toRou()
    {
        StartCoroutine(toWait());
    }

    public void ClearTab()
    {
        betText.text = 0.ToString();

        for (int i = 0; i < betsChips.Count; i++)
        {
            Destroy(betsChips[i]);
        }
        betsChips.Clear();
        sTime = 0;
        temCount = 0;
        temMulti = 0;
        for (int i = 0; i < 37; i++)
        {
            temArea[i] = 0;
        }
        temTotal = 0;
    }
    public void LookCamera()
    {
        if (!doLook)
        {
            lookTab.transform.position = Vector3.Lerp(closeTab.transform.position, openTab.transform.position, 1);
            lookBall.rect = new Rect(0.8f, lookBall.rect.y, lookBall.rect.width, lookBall.rect.height);
            doLook = true;
        }
        else
        {
            lookTab.transform.position = Vector3.Lerp(openTab.transform.position, closeTab.transform.position, 1);
            lookBall.rect = new Rect(1, lookBall.rect.y, lookBall.rect.width, lookBall.rect.height);
            doLook = false;
        }
    }

    public IEnumerator toWait()
    {
        //關UI按鈕
        //清空Plan
        //結算分數

        //mainUI.SetActive(false);

        playButton.interactable = false;
        clearButton.interactable=false;
        baseButton.interactable = false;


        mainCamera.orthographic = false;
        toRouCamera.SetActive(true);

        yield return new WaitForSeconds(1);

        if (!doLook) LookCamera();

        int wt = 0;
        int number = 0;
        while (wt < 30)
        {
            number = ball.number;
            yield return new WaitForSeconds(0.1f);

            if (ball.number == number && ball.number != -1)
            {
                wt++;
            }
            else { wt = 0; }
        }

        yield return new WaitForSeconds(1);

        if (doLook) LookCamera();

        //結算分數
        temTotal = (temArea[number] * Int32.Parse(baseText.text)) - Int32.Parse(betText.text);

        if (temTotal >= 0) totalTabText.text = "+" + temTotal.ToString();
        else totalTabText.text = temTotal.ToString();

        totalTab.SetActive(true);
        onTotal = true;
        while (onTotal)
        {
            if (Input.anyKey) onTotal = false;
            yield return null;
        }
        totalTab.SetActive(false);


        for (int i = 0; i < 9; i++)
        {
            pText[i].text = pText[i + 1].text;
            pImage[i].color = pImage[i + 1].color;
            pImage[i + 10].color = pImage[i + 11].color;
        }

        pText[9].text = ball.number.ToString();
        pImage[19].color = new Color32(253, 190, 44, 255);

        if (ball.number == 0) pImage[9].color = new Color32(0, 153, 25, 255);
        else if (Array.Exists<int>(redArray, n => n == ball.number)) pImage[9].color = new Color32(204, 0, 0, 255);
        else pImage[9].color = new Color32(0, 0, 0, 255);


        bankRollText.text = (Int32.Parse(bankRollText.text) + temTotal).ToString();

        if (bankRollText.text == "0") { toTit(); }//結束遊戲

        if (Int32.Parse(baseText.text) > Int32.Parse(bankRollText.text))
        {
            baseText.text = bankRollText.text;
        }



        //清空
        betText.text = 0.ToString();

        for (int i = 0; i < betsChips.Count; i++)
        {
            Destroy(betsChips[i]);
        }
        betsChips.Clear();
        sTime = 0;
        temCount = 0;
        temMulti = 0;
        for (int i = 0; i < 37; i++)
        {
            temArea[i] = 0;
        }
        temTotal = 0;


        playButton.interactable = true;
        clearButton.interactable = true;
        baseButton.interactable = true;

        mainCamera.orthographic = true;
        toRouCamera.SetActive(false);
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditor;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Move : MonoBehaviour
{
    Vector3 mtempA;

    Vector3 Otemp;

    [SerializeField]
    Plan plan;
    [SerializeField]
    GameObject multiObj;
    [SerializeField]
    TextMeshProUGUI multiText;

    int multi;

    [SerializeField]
    GameObject chips;

    [SerializeField]
    int addBet = 1;

    string tempChipsName;
    List<int> tempChipsArea = new List<int>();



    private void OnMouseDown()
    {
        plan.EffPlay(0);

        Otemp = gameObject.transform.position;
        multiText.text = "";
        for (int i = 0; i < plan.perChips.Count; i++)
        {
            if (gameObject.name == plan.perChips[i].name) { continue; }
            plan.perChips[i].SetActive(false);
        }
    }

    private void OnMouseDrag()
    {
        mtempA = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = new Vector3(mtempA.x, Otemp.y, mtempA.z);
    }

    private void OnMouseUp()
    {
        gameObject.transform.position = Otemp;

        if (plan.inN.Count > 0 && Int32.Parse(plan.baseText.text) * (plan.temCount + addBet) <= Int32.Parse(plan.bankRollText.text))
        {

            plan.EffPlay(1);

            tempChipsName = plan.inN[plan.inN.Count - 1];
            for (int i = 0; i < tempChipsName.Split(',').Length; i++)
            {
                if (Int32.Parse(tempChipsName.Split(',')[i]) <= 36)
                {
                    tempChipsArea.Add(Int32.Parse(tempChipsName.Split(',')[i]));
                }
            }
            if (plan.betsChips.Find(e => e.GetComponent<Chips>().areaName == tempChipsName) == null)
            {
                plan.betsChips.Add(Instantiate(chips, plan.nObj.Find(e => e.name == plan.inN[plan.inN.Count - 1]).transform.position, Quaternion.identity));
                plan.betsChips[plan.betsChips.Count - 1].GetComponent<Chips>().areaName = tempChipsName;
                plan.betsChips[plan.betsChips.Count - 1].GetComponent<Chips>().multi = multi;
                for (int i = 0; i < tempChipsArea.Count; i++)
                {
                    plan.betsChips[plan.betsChips.Count - 1].GetComponent<Chips>().batArea.Add(tempChipsArea[i]);
                }
            }
            if (plan.betsChips.Find(e => e.GetComponent<Chips>().areaName == tempChipsName).GetComponent<Chips>().count + addBet < 999)
            {
                plan.betsChips.Find(e => e.GetComponent<Chips>().areaName == tempChipsName).GetComponent<Chips>().count += addBet;

            }
            if (plan.betsChips.Find(e => e.GetComponent<Chips>().areaName == tempChipsName).GetComponent<Chips>().count >= 500)
            {
                for (int i = 0; i < plan.betsChips.Find(e => e.GetComponent<Chips>().areaName == tempChipsName).GetComponent<Chips>().betColor.Count; i++)
                {
                    plan.betsChips.Find(e => e.GetComponent<Chips>().areaName == tempChipsName).GetComponent<Chips>().betColor[i].color = new Color(0.5f, 0, 0.5f, 1);
                }
            }
            else if (plan.betsChips.Find(e => e.GetComponent<Chips>().areaName == tempChipsName).GetComponent<Chips>().count >= 100)
            {
                for (int i = 0; i < plan.betsChips.Find(e => e.GetComponent<Chips>().areaName == tempChipsName).GetComponent<Chips>().betColor.Count; i++)
                {
                    plan.betsChips.Find(e => e.GetComponent<Chips>().areaName == tempChipsName).GetComponent<Chips>().betColor[i].color = Color.yellow;
                }
            }
            else if (plan.betsChips.Find(e => e.GetComponent<Chips>().areaName == tempChipsName).GetComponent<Chips>().count >= 50)
            {
                for (int i = 0; i < plan.betsChips.Find(e => e.GetComponent<Chips>().areaName == tempChipsName).GetComponent<Chips>().betColor.Count; i++)
                {
                    plan.betsChips.Find(e => e.GetComponent<Chips>().areaName == tempChipsName).GetComponent<Chips>().betColor[i].color = Color.blue;
                }
            }
            else if (plan.betsChips.Find(e => e.GetComponent<Chips>().areaName == tempChipsName).GetComponent<Chips>().count >= 10)
            {
                for (int i = 0; i < plan.betsChips.Find(e => e.GetComponent<Chips>().areaName == tempChipsName).GetComponent<Chips>().betColor.Count; i++)
                {
                    plan.betsChips.Find(e => e.GetComponent<Chips>().areaName == tempChipsName).GetComponent<Chips>().betColor[i].color = Color.green;
                }
            }
            plan.betsChips.Find(e => e.GetComponent<Chips>().areaName == tempChipsName).GetComponent<Chips>().countText.text = "x" + plan.betsChips.Find(e => e.GetComponent<Chips>().areaName == tempChipsName).GetComponent<Chips>().count;
        }
        else
        {
            plan.EffPlay(2);
        }

        tempChipsName = "";
        tempChipsArea.Clear();

        for (int i = 0; i < plan.perChips.Count; i++)
        {
            if (gameObject.name == plan.perChips[i].name) { continue; }
            plan.perChips[i].SetActive(true);
        }

        plan.allBet();
        plan.BaseChang();
    }


    private void OnCollisionEnter(Collision collision)
    {
        plan.sTime = 0;
        plan.inN.Add(collision.gameObject.name);
        multiObj.transform.localScale = new Vector3(0, 0, 1);
        for (int i = 0; i < plan.s.Count; i++)
        {
            plan.s[i].color = new Color(plan.s[i].color.r, plan.s[i].color.g, plan.s[i].color.b, 0);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name != "底") (multiText.text, multi) = Multi(plan.inN[plan.inN.Count - 1]);
        multiText.text = multiText.text + "\nX" + multi;
        multiObj.SetActive(true);

        if (multiObj.transform.localScale.x <= 0.8f) multiObj.transform.localScale = new Vector3(1 * plan.sTime, 1 * plan.sTime, 0);
    }

    private void OnCollisionExit(Collision collision)
    {
        plan.inN.RemoveAll(e => e == collision.gameObject.name);
        multiObj.SetActive(false);
    }


    (string, int) Multi(string name)
    {
        string[] nBet = name.Split(',');

        switch (nBet.Length)
        {
            case 1:
                return ("Straight Bet", 36);
            case 2:
                return ("Split Bet", 18);
            case 3:
                return ("Street Bet", 12);
            case 4:
                return ("Corner Bet", 9);
            case 6:
                return ("Line Bet", 6);
            case 13:
                switch (nBet[nBet.Length - 1])
                {
                    case "37":
                        return ("Column Bet", 3);
                    case "38":
                        return ("Column Bet", 3);
                    case "39":
                        return ("Column Bet", 3);
                    case "40":
                        return ("Dozen Bet", 3);
                    case "41":
                        return ("Dozen Bet", 3);
                    case "42":
                        return ("Dozen Bet", 3);
                    default: return ("", 0);
                }
            case 19:
                switch (nBet[nBet.Length - 1])
                {
                    case "43":
                        return ("Low Bet", 2);
                    case "44":
                        return ("Even Bet", 2);
                    case "45":
                        return ("Red Bet", 2);
                    case "46":
                        return ("Black Bet", 2);
                    case "47":
                        return ("Odd Bet", 2);
                    case "48":
                        return ("Hight Bet", 2);
                    default: return ("", 0);
                }
            default: return ("", 0);
        }
    }
}

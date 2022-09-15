using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Chips : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI countText;
    [SerializeField]
    public List<Image> betColor;

    public string areaName = "";

    public int multi = 0;

    public List<int> batArea = new List<int>();

    public int count = 0;

    
}

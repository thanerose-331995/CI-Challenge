using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotBars : MonoBehaviour
{
    public Image damage, hp, firerate, evasion;
    public GameObject txtd, txthp, txtfr, txte;
    public static bool change;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (change)
        {
            ChangeData();
            change = false;
        }
    }

    void ChangeData()
    {
        Image[] bars = { damage, hp, firerate, evasion };
        GameObject[] texts = { txtd, txthp, txtfr, txte };
        string[] desc = { "Damage", "Shields", "Rate of Fire", "Evasion" };
        string[] units = { "Per Hit", "HP", "per sec", "%" };
        float[] text = { Robot.weaponsLVL, Robot.shieldLVL, Robot.powerLVL, Robot.movementLVL };
        float[] data = { ((Robot.weaponsLVL * 100) / 80), ((Robot.shieldLVL * 100) / 600), ((Robot.powerLVL * 100) / 4), ((Robot.movementLVL * 100) / 40) };

        for (int i = 0; i < bars.Length; i++) //display info for each of the bars
        {
            var thisRectTransform = bars[i].transform as RectTransform;
            bars[i].rectTransform.sizeDelta = new Vector2(data[i]*5, thisRectTransform.sizeDelta.y);
            texts[i].GetComponent<Text>().text = desc[i] + ": " + text[i].ToString() + " (" + units[i] + ")";
        }
    }
}

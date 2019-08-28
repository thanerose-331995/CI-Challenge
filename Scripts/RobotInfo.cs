using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotInfo : MonoBehaviour
{
    public Image power, newPower, shields, newShields, weapons, newWeapons, movement, newMovement;
    private float pMod, sMod, wMod, mMod, modifier;
    public static bool change = false;
    public static string objName;
    private string type;

    // Start is called before the first frame update
    void Start()
    {
        change = true;
    }

    // Update is called once per frame
    void Update()
    {
        pMod = Robot.powerLVL;
        sMod = Robot.shieldLVL;
        wMod = Robot.weaponsLVL;
        mMod = Robot.movementLVL;

        if (change)
        {
            for(int j = 0; j < DataHandling.moduleList.Count; j++)
            {
                if(DataHandling.moduleList[j].moduleName == objName) //finds matching module
                {
                    type = DataHandling.moduleList[j].moduleType;
                    modifier = DataHandling.moduleList[j].modifier;
                }
            }
            

            switch (type)
            {
                case "power": //set to matching type
                    pMod = modifier;
                    break;
                case "shields":
                    sMod = modifier;
                    break;
                case "weapons":
                    wMod = modifier;
                    break;
                case "movement":
                    mMod = modifier;
                    break;
                default:
                    break;
            }

            Image[] bars = { power, newPower, shields, newShields, weapons, newWeapons, movement, newMovement }; //bars
            float[] mods = { (((Robot.powerLVL) * 100) / 4), (((pMod) * 100) / 4), (((Robot.shieldLVL) * 100) / 600), (((sMod) * 100) / 600), (((Robot.weaponsLVL) * 100) / 80), (((wMod) * 100) / 80), (((Robot.movementLVL) * 100) / 40), (((mMod) * 100) / 40) }; //values

            for (int i = 0; i < bars.Length; i++)
            {
                var thisRectTransform = bars[i].transform as RectTransform;
                bars[i].rectTransform.sizeDelta = new Vector2(mods[i], thisRectTransform.sizeDelta.y); //change y delta size
            }

            change = false;
        }
    }

    public static void ChangeBars(string name)
    {
        change = true;
        objName = name;
    }
}

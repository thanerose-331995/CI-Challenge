using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleEquip : MonoBehaviour
{
    public static string moduleName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Equip()
    {
        for (int i = 0; i < DataHandling.moduleList.Count; i++)
        {
            if (DataHandling.moduleList[i].moduleName == moduleName) //find module
            {
                switch (DataHandling.moduleList[i].moduleType)
                {
                    case "power":
                        if(Robot.powerName != DataHandling.moduleList[i].moduleName) //this is to check if theyre not already equipped
                        {
                            Debug.Log("Equipped: " + moduleName);
                            Robot.powerLVL = DataHandling.moduleList[i].modifier;
                            Debug.Log("stat: " + Robot.powerLVL);
                        }
                        break;
                    case "shields":
                        if (Robot.shieldName != DataHandling.moduleList[i].moduleName)
                        {
                            Debug.Log("Equipped: " + moduleName);
                            Robot.shieldLVL = DataHandling.moduleList[i].modifier;
                        }
                        break;
                    case "weapons":
                        if (Robot.weaponsName != DataHandling.moduleList[i].moduleName)
                        {
                            Debug.Log("Equipped: " + moduleName);
                            Robot.weaponsLVL = DataHandling.moduleList[i].modifier;
                        }
                        break;
                    case "movement":
                        if (Robot.movemnentName != DataHandling.moduleList[i].moduleName)
                        {
                            Debug.Log("Equipped: " + moduleName);
                            Robot.movementLVL = DataHandling.moduleList[i].modifier;
                        }
                        break;
                    case "powerups":
                        if (Robot.powerup != DataHandling.moduleList[i].moduleName)
                        {
                            Debug.Log("Equipped: " + moduleName);
                            Robot.powerup = DataHandling.moduleList[i].moduleName;
                        }
                        break;
                    default:
                        break;
                } 
            }
        }
        RobotBars.change = true;
        RobotInfo.change = true;
        SendData.Equip(moduleName);
    }
}

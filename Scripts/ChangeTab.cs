using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTab : MonoBehaviour
{
    public GameObject power, shield, weapons, movement, powerups, chosenTab, powerC, shieldC, weaponsC, movementC, powerUpsC;
    public static bool change = true;
    public static string chosenName;
    private List<Color32> colors = new List<Color32>();
    // Start is called before the first frame update
    void Start()
    {
        Color32[] iconColors = { powerC.GetComponent<Image>().color, shieldC.GetComponent<Image>().color, weaponsC.GetComponent<Image>().color, movementC.GetComponent<Image>().color, powerUpsC.GetComponent<Image>().color };
        for(int x = 0; x < iconColors.Length; x++)
        {
            colors.Add(iconColors[x]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (change)
        {
            GameObject[] tabs = { power, shield, weapons, movement, powerups };
            GameObject[] icons = { powerC, shieldC, weaponsC, movementC, powerUpsC };

            for (int i = 0; i < tabs.Length; i++)
            {

                if(chosenName == tabs[i].tag)
                {
                    chosenTab = tabs[i];
                }
            }

            for(int j = 0; j < tabs.Length; j++)
            {
                if(tabs[j] == chosenTab)
                {
                    tabs[j].SetActive(true);
                    icons[j].GetComponent<Image>().color = colors[j];
                }
                else
                {
                    tabs[j].SetActive(false);
                    icons[j].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }
            }
            ModuleLoad.loadModules = true;
            ModuleLoad.typeToLoad = chosenName;
            change = false;
        }
    }

    public static void ModuleTab(string name)
    {
        change = true;
        chosenName = name;
    }
}

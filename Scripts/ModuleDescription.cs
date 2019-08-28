using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModuleDescription : MonoBehaviour
{
    public GameObject prefab;
    public static bool gen = false;
    public static string name;
    private int flag = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gen)
        {
            Generate();
        }
    }

    public static void ShowInfo(string thisname)
    {
        name = thisname;
        gen = true;

    }

    void Generate()
    {
        if(flag == 1)
        {
            GameObject newObj;
            newObj = (GameObject)Instantiate(prefab, transform);
            flag = 2; //so prefab only is created once
        }
        for(int i = 0; i < DataHandling.moduleList.Count; i++)
        {
            if(DataHandling.moduleList[i].moduleName == name) //if module matches name passed then show info
            {
                GameObject.Find("moduleName").GetComponent<Text>().text = DataHandling.moduleList[i].moduleName;
                GameObject.Find("moduleType").GetComponent<Text>().text = "Type: " + DataHandling.moduleList[i].moduleType;
                GameObject.Find("description").GetComponent<Text>().text = DataHandling.moduleList[i].description;
                GameObject.Find("moduleIcon").GetComponent<RawImage>().texture = Resources.Load<Texture>(DataHandling.moduleList[i].iconName);
                GameObject.Find("mod").GetComponent<Text>().text = "Modifier: " + DataHandling.moduleList[i].modifier.ToString();
            }
        }
        gen = false;
    }
}

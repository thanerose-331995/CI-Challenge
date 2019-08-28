using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModuleLoad : MonoBehaviour
{
    public GameObject prefab;
    public static bool loadModules = false;
    public static string typeToLoad = "power";
    private int count;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (loadModules)
        {
            Populate();
            loadModules = false;
        }
    }

    void Populate()
    {
        GameObject newObj;
        count = 1;

        if(typeToLoad == "shield")
        {
            typeToLoad += "s";
        }
        
        
        if (transform.childCount < 1) //if theres no child objects
        {
            for (int i = 0; i < DataHandling.moduleList.Count; i++) //for all in module list
            {
                if(DataHandling.moduleList[i].moduleType == typeToLoad) //if they are the type to load
                {
                    newObj = (GameObject)Instantiate(prefab, transform); //instantate prefab
                    newObj.name = DataHandling.moduleList[i].moduleName; //set gameobject details
                    Texture tex = Resources.Load<Texture>(typeToLoad + (count));
                    newObj.GetComponent<RawImage>().texture = tex;

                    newObj.AddComponent<ModuleChoose>();
                    count++;
                    
                }

            }
        }


    }
}

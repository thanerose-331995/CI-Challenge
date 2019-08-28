using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMove : MonoBehaviour
{
    public Canvas build, main, progress, selected, current;
    public static string newScreen;
    public static bool change = false;
    public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        current = main;
        selected = current;
    }

    // Update is called once per frame
    void Update()
    {
        if (change)
        {
            switch (newScreen)
            {
                case "main":
                    selected = main;
                    break;
                case "build":
                    selected = build;
                    break;
                case "progress":
                    selected = progress;
                    break;
                default:
                    break;
            }
            change = false;
        }

        Canvas[] screens = { build, main, progress };

        RectTransform selectRect;
        selectRect = selected.GetComponent<RectTransform>();

        if (selectRect.position.x != 0)
        {
            if (selectRect.position.x < current.GetComponent<RectTransform>().position.x)
            {
                for (int i = 0; i < screens.Length; i++)
                {
                    RectTransform thisRect;
                    thisRect = screens[i].GetComponent<RectTransform>();
                    thisRect.position = new Vector3((thisRect.position.x + 2), thisRect.position.y, thisRect.position.z);
                }
            }
            else
            {
                for (int j = 0; j < screens.Length; j++)
                {
                    RectTransform thisRect;
                    thisRect = screens[j].GetComponent<RectTransform>();
                    thisRect.position = new Vector3((thisRect.position.x - 2), thisRect.position.y, thisRect.position.z);
                }
            }
        }
        else
        {
            current = selected;
        }

        if (selected == progress)
        {
            cube.SetActive(false);
        }
        else
        {
            cube.SetActive(true);
        }
        

    }

    public static void Change(string newChange)
    {
        change = true;
        newScreen = newChange;
    }
}

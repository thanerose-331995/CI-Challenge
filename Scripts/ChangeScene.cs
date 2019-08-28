using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public Button main, build, progress, test, selected;
    private Color32 normalC, activeC;
    private ColorBlock normal, active;

    // Start is called before the first frame update
    void Start()
    {
        normal = progress.colors;
        active = main.colors;

        selected = main;
    }

    // Update is called once per frame
    void Update()
    {
        Button[] buttons = { main, build, progress};

        for(int i = 0; i < buttons.Length; i++) //set colours
        {
            if(buttons[i] == selected)
            {
                buttons[i].colors = active;
            }
            else
            {
                buttons[i].colors = normal;
            }
        }
    }

    public void ToMain()
    {
        CanvasMove.Change("main");
        selected = main;
        MoveCube.move1 = true;
    }
    public void ToBuild()
    {
        CanvasMove.Change("build");
        selected = test;
        MoveCube.move2 = true;
    }
    public void ToProgress()
    {
        CanvasMove.Change("progress");
        selected = progress;
    }
}

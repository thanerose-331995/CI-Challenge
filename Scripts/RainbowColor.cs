using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainbowColor : MonoBehaviour
{
    private int red, green, blue;
    private int count = 1;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Color32 color;
        color = gameObject.GetComponent<Image>().color; //get the color of the image

        red = color.r; //each r/g/b value
        green = color.g;
        blue = color.b;
        
        switch (count)  //change the color depending on the stage
        {
            case 1:
                if (red != 0)
                {
                    color.r--;
                }
                else
                {
                    count++;
                }
                break;
            case 2:
                if (green != 255)
                {
                    color.g++;
                }
                else
                {
                    count++;
                }
                break;
            case 3:
                if (blue != 0)
                {
                    color.b--;
                }
                else
                {
                    count++;
                }
                break;
            case 4:
                if (red != 255)
                {
                    color.r++;
                }
                else
                {
                    count++;
                }
                break;
            case 5:
                if (green != 0)
                {
                    color.g--;
                }
                else
                {
                    count++;
                }
                break;
            case 6:
                if (blue != 255)
                {
                    color.b++;
                }
                else
                {
                    count = 1;
                }
                break;
            default:
                break;

        }
        
        gameObject.GetComponent<Image>().color = color; //set the new color
    }
}

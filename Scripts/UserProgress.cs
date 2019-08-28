using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserProgress : MonoBehaviour
{
    public Image open, agree, conciencious, extra, emo, idealO, idealA, idealC, idealEX, idealEM;
    public Text username, screenname;
    public static bool change = false;
    public bool debug;
    public static List<float> userdata = new List<float>();
    public static List<float> idealdata = new List<float>();
    public static List<float> data = new List<float>();
    // Start is called before the first frame update
    void Start()
    {
        if (debug)
        {
            float[] tempdata = { 10, 10, 10, 10, 10 }; //fake values for debugging
            Change(tempdata, "ideals");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (change)
        {
            for(int k = 0; k < 10; k++) //there are 10 bars on screen
            {
                if(k >= 5)
                {
                    data.Add((idealdata[k - 5] * 1.5f)); //set the last 5 as the data from ideals
                }
                else
                {
                    data.Add(userdata[k]); //set the first 5 as data from the user
                }
            }
            ChangeScreen(); //change screen
            ChangeData();
            change = false;
        }
    }

    void ChangeScreen()
    {
        username.text = "@" + API.username; //just a ui feature
    }

    void ChangeData()
    {
        Image[] bars = { open, agree, conciencious, extra, emo, idealO, idealA, idealC, idealEX, idealEM }; //for all of the bars
        for (int i = 0; i < bars.Length; i++)
        {
            var thisRectTransform = bars[i].transform as RectTransform;
            bars[i].rectTransform.sizeDelta = new Vector2(data[i] * 8, thisRectTransform.sizeDelta.y); //changes y (length) of each bar depending on the value of data[i]*8 (the 8 is for scaling)
        }
    }

    public static void Change(float[] tempdata, string type)
    {
        for(int j = 0; j < tempdata.Length; j++)
        {
            if(type == "user") //depending on the type of data passed (indicated by the string) tempdata is added to either userdata or idealdata
            {
                userdata.Add(tempdata[j]);
            }
            else
            {
                idealdata.Add(tempdata[j]);
            }
        }
    }
}

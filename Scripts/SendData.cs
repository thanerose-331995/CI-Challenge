using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendData : MonoBehaviour
{
    public static bool sendFight = false, sendRobot = false, sendQuestions = false, authorisation = false;
    public static string module;
    public static List<float> answerData = new List<float>();
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (sendRobot || sendQuestions || authorisation) //if any are true run function
        {
            sendData();
        }
    }

    void sendData()
    {
        StartCoroutine(Send());  //in coroutine
    }

    IEnumerator Send()
    {
        if (sendFight)
        {
            FightData.fight = true;
            sendFight = false;
            WWW data = new WWW(API.baseURL + API.fight + API.username);
            yield return data; //data is not used but this line is necessary for script to run
        }
        if (sendRobot)
        {
            string type = "";
            string lvl = "";
            for(int i = 0; i < DataHandling.moduleList.Count; i++) //for every module
            {
                if(DataHandling.moduleList[i].moduleName == module)  //if the module is the same as the one being equipped
                {
                    type = DataHandling.moduleList[i].moduleType;  //add this modules data
                    lvl = DataHandling.moduleList[i].moduleLevel.ToString();
                }
            }
            sendRobot = false;
            WWW data = new WWW(API.baseURL + API.build + API.username + "$" + type + lvl); //send this data
            yield return data;
        }
        if (sendQuestions)
        {
            sendQuestions = false;
            string tempdata = "";
            for(int i = 0; i < answerData.Count; i++)
            {
                tempdata += "~"; //seperate the answers with a ~ for formatting
                tempdata += answerData[i].ToString();
            }
            WWW data = new WWW(API.baseURL + API.questions + API.username + tempdata);
            yield return data;
        }

        if (authorisation)
        {
            authorisation = false;
            WWW data = new WWW(API.baseURL + API.createUser + API.username); //authorisation request
            yield return data;
        }
    }

    public void Fight()
    {
        sendFight = true;
        sendData();
    }

    public static void Equip(string name)
    {
        sendRobot = true;
        module = name;
    }

    public static void Answers(float[] data)
    {
        sendQuestions = true;
        for(int j = 0; j < data.Length; j++)
        {
            answerData.Add(data[j]);
        }
    }
}

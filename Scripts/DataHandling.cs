using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandling : MonoBehaviour
{
    public Data data;
    public bool debug, offlineTesting;
    public string debugUsername;
    public static bool authorisation, fight;
    public static List<Modules> moduleList = new List<Modules>();


    void Start()
    {
        if(debug == true)
        {
            API.username = debugUsername; //debugging purposes
            getData();
        }
    }

    void Update()
    {
        if (authorisation) //if either are true run getData()
        {
            getData();
        }
        if (fight)
        {
            getData();
        }
    }

    public void getData()
    {
        StartCoroutine(GetData()); //this is in a coroutine for saftey
    }


    IEnumerator GetData()
    {
        if (fight)  //GETTING FIGHT REQUEST
        {
            fight = false;
            
            WWW getdata1 = new WWW(API.baseURL + API.users + API.username + "/Fights/fightCount.txt");
            yield return getdata1;
            string numb = getdata1.text;

            WWW getdata = new WWW(API.baseURL + API.users + API.username + "/Fights/fightReport" + numb + ".txt");
            yield return getdata;
            FightData.GetData(getdata.text);

        }
        else if (authorisation)  //GETTING AUTHORISATION REQUEST
        {
            authorisation = false;
            
            WWW getdata = new WWW(API.baseURL + API.users + API.username + API.auth);
            yield return getdata;
            TwitterIDEnter.data = getdata.text;
        }
        else
        {
            if (offlineTesting)  //DEBUG FEATURE FOR NON SERVER PARSE TESTS
            {
                SortData("12 4 6 3 5");

                for (int i = 0; i < 20; i++)  //create random modules
                {
                    int numb = Random.Range(1, 10);
                    string test = "test" + numb.ToString();
                    moduleList.Add(new Modules(test, test, numb, test, test, (float)numb));
                }
            }
            else
            {

                // -- GET USER DATA -- //
                WWW getdata = new WWW(API.baseURL + API.users + API.username + API.userStats); //send request string
                yield return getdata; //get result

                data.text_data = getdata.text;
                SortData(getdata.text); //send to sort

                // -- MODULE DATA -- //
                int count = 1;

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        WWW getModuleData = new WWW(API.baseURL + API.module + API.types[i] + "/LVL" + (j + 1) + ".txt"); //each request
                        yield return getModuleData;
                        SortModuleData(getModuleData.text, API.types[i], j + 1, count);
                        count++;
                    }
                }
                // -- DEBUG MODULE DATA -- //
                string testString = "ModulesLoaded: ";
                for (int j = 0; j < moduleList.Count; j++)
                {
                    testString += moduleList[j].moduleName.ToString() + " / ";
                }
                //Debug.Log(testString);
                ModuleLoad.loadModules = true;

                // -- ROBOT DATA -- //
                WWW robotData = new WWW(API.baseURL + API.users + API.username + API.robotStats);
                yield return robotData;
                SortRobotData(robotData.text);

            }
        }
    }

    void SortData(string tempdata)
    {
        string data = "";
        int count = 0;

        if(tempdata[tempdata.Length-1] != ' ') //if does not end with space add a space
        {
            tempdata += ' '; 
        }

        for (int i = 0; i < tempdata.Length ; i++)
        {
            if(tempdata[i] != ' ') //if not a space must be value
            {
                data += tempdata[i].ToString(); //add to data
            }
            else //if space
            {
                count++;                
                switch (count)
                {
                    case 1:
                        Data.openness = int.Parse(data); //set data as value
                        break;
                    case 2:
                        Data.conscientiousness = int.Parse(data);
                        break;
                    case 3:
                        Data.extraversion = int.Parse(data);
                        break;
                    case 4:
                        Data.agreeableness = int.Parse(data);
                        break;
                    case 5:
                        Data.emotional_range = int.Parse(data);
                        break;
                    default:
                        break;
                }
                data = "";
            }
        }
        //open, agree, conciencious, extra, emo,
        float[] userdata = { Data.openness, Data.agreeableness, Data.conscientiousness, Data.extraversion, Data.emotional_range };
        UserProgress.Change(userdata, "user"); //send to userprogress
        UserProgress.change = true;
    }

    void SortModuleData(string tempdata, string type, int lvl, int count)
    {
        string temp = "";
        string[] data = { "", "", "" };
        int check = 0;

        for (int y = 0; y < type.Length; y++)
        {
            if(type[y] != '/') //if not / is value
            {
                temp += type[y].ToString();
            }
        }        

        type = temp;
        temp = "";

        for (int q = 0; q < tempdata.Length; q++)
        {
            if(tempdata[q] == '\n') //when endline
            {
                data[check] = temp;
                check++;
                temp = "";
            }
            else
            {
                temp += tempdata[q].ToString();
            }
        }             

        // string type, string name, int level, string icon, string des, int mod
        Modules mod = new Modules(type.ToLower(), data[0], lvl, (type.ToLower() + lvl.ToString()), data[2], float.Parse(data[1]) );
        moduleList.Add(mod);  //push each into list
    }

    void SortRobotData(string tempdata)
    {
        //Debug.Log("robot data recieved: " + tempdata);
        string data = "";
        int count = 0;

        if (tempdata[tempdata.Length - 1] != '/')
        {
            tempdata += '/';
        }

        for (int i = 0; i < tempdata.Length; i++)
        {
            if (tempdata[i] != '/')
            {
                data += tempdata[i].ToString();
            }
            else
            {
                count++;
                switch (count)
                {
                    case 1:
                        Robot.powerName = data;
                        break;
                    case 2:
                        Robot.powerLVL = float.Parse(data);
                        break;
                    case 3:
                        Robot.shieldName = data;
                        break;
                    case 4:
                        Robot.shieldLVL = float.Parse(data);
                        break;
                    case 5:
                        Robot.movemnentName = data;
                        break;
                    case 6:
                        Robot.movementLVL = float.Parse(data);
                        break;
                    case 7:
                        Robot.weaponsName = data;
                        break;
                    case 8:
                        Robot.weaponsLVL = float.Parse(data);
                        break;
                    case 9:
                        Robot.powerup = data;
                        break;
                    default:
                        break;
                }
                data = "";
            }
        }
        RobotBars.change = true;
        Debug.Log("Robot Data: Damage: " + Robot.weaponsLVL + " / Attack Speed: " + Robot.powerLVL + " / Health: " + Robot.shieldLVL + " / Evasion: " + Robot.movementLVL);
    }
}

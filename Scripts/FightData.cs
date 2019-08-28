using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightData : MonoBehaviour
{
    public static bool fight, got, runSequence, next, moveback;
    public int count = 0,  user1Health = 0, user2Health = 0;
    public Text username, opponent;
    private bool move;
    public GameObject mainScreen, fightScreen, tabs, cube1, cube2, robot1, robot2;
    public List<string> actions = new List<string>();
    public List<FightSequence> events = new List<FightSequence>();
    public static string data;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fight)
        {
            fight = false;
            FightStart();
            move = true;
        }
        if (move) //moves the screen into the fight zone
        {
            if (fightScreen.GetComponent<RectTransform>().position.y > 3)
            {
                RectTransform thisRect;

                thisRect = fightScreen.GetComponent<RectTransform>();
                thisRect.position = new Vector3((thisRect.position.x), thisRect.position.y - 1, thisRect.position.z);

                thisRect = mainScreen.GetComponent<RectTransform>();
                thisRect.position = new Vector3((thisRect.position.x), thisRect.position.y - 1, thisRect.position.z);

                thisRect = tabs.GetComponent<RectTransform>();
                thisRect.position = new Vector3(thisRect.position.x, thisRect.position.y - 1, thisRect.position.z);
            }
            else
            {
                move = false;
                next = true;
                cube2.SetActive(true);
                robot1.SetActive(true);
                robot2.SetActive(true);
                StartCoroutine(FightRequest()); //get the fight data
            }
        }

        if (moveback) //moves back once fight is done
        {
            cube2.SetActive(false);

            if (fightScreen.GetComponent<RectTransform>().position.y != 55)
            {
                RectTransform thisRect;

                thisRect = fightScreen.GetComponent<RectTransform>();
                thisRect.position = new Vector3((thisRect.position.x), thisRect.position.y + 1, thisRect.position.z);

                thisRect = mainScreen.GetComponent<RectTransform>();
                thisRect.position = new Vector3((thisRect.position.x), thisRect.position.y + 1, thisRect.position.z);

                thisRect = tabs.GetComponent<RectTransform>();
                thisRect.position = new Vector3(thisRect.position.x, thisRect.position.y + 1, thisRect.position.z);
            }
            else
            {
                moveback = false;
                cube1.SetActive(true);
            }

        }

        if (got) //once obtained, sort
        {
            got = false;
            SortData(data);
        }

        if (runSequence) //running the sequence
        {
            if(count < events.Count)
            {
                if (next)
                {
                    next = false;
                    if (events[count].thisEvent == "hit")
                    {
                        PlayerShoot(events[count].attacker, events[count].damage); //pass info
                    }
                    else if (events[count].thisEvent == "miss")
                    {
                        if (events[count].attacker == API.username)
                        {
                            Dodge("P2");
                        }
                        else
                        {
                            Dodge("P1");
                        }
                    }
                    count++;
                }
            }
            else
            {
                count = 0;
                FightActions.end = true; //fight is done
                runSequence = false;
                events.Clear(); //clear ready for next fight
            }
        }

    }

    public static void GetData(string tempdata)
    {
        got = true;
        data = tempdata;
    }

    void FightStart()
    {
        cube1.SetActive(false);     
    }

    public void SortData(string tempdata)
    {
        string data = "";
        tempdata += '\n';

        //sort into list
        for (int i = 0; i < tempdata.Length; i++)
        {
            if (tempdata[i] != '\n')
            {
                data += tempdata[i];
            }
            else
            {
                if(data != "")
                {
                    data = data.Remove(data.Length - 1);
                    data += " ";
                    actions.Add(data); //sorting into lines
                }
                data = "";
            }
        }

        //formatting
        for(int j = 0; j < actions.Count; j++)
        {
            //1st username / event / second username / number
            //! = stop line
            string actionLine = actions[j], sequence = "", attacker = "", thisEvent = "", damage = "1";
            int count = 1;
            for (int k = 0; k < actionLine.Length; k++)
            {
                if(actionLine[k] != ' ')
                {
                    sequence += actionLine[k];
                }
                else
                {
                    if(sequence == "wins")
                    {
                        FightActions.winner = attacker;
                    }
                    else if (sequence != "dealing" && sequence != "dmg") //ignore these words
                    {
                        switch (count)
                        {
                            case 1:
                                if (sequence == API.username)
                                {
                                    attacker = "P1";
                                }
                                else
                                {
                                    opponent.text = sequence;
                                    username.text = API.username;
                                    attacker = "P2";
                                }
                                break;
                            case 2:
                                if (sequence == "hit")
                                {
                                    thisEvent = "hit";
                                }
                                else
                                {
                                    thisEvent = "miss";
                                }
                                break;
                            case 3: //ignored as this is the second players name, which is unusable but cannot be skipped as theres no way to know what it will be
                                break;
                            case 4:
                                if (thisEvent == "hit")
                                {
                                    damage = sequence;
                                    Debug.Log(damage);
                                    if(attacker == "P1")
                                    {
                                        user2Health += int.Parse(damage);
                                    }
                                    else
                                    {
                                        user1Health += int.Parse(damage);
                                    }
                                }
                                break;
                        }
                        count++;
                    }
                    sequence = "";
                }

            }
            events.Add(new FightSequence(attacker, thisEvent, int.Parse(damage))); //useable data
            FightActions.user1health = user1Health;
            FightActions.user2health = user2Health;
            FightActions.sethp = true;
        }
        runSequence = true;
        actions.Clear(); //for the next fight
    }
    
    void PlayerShoot(string attacker, int damage)
    {
        FightActions.Fire(attacker, damage);
    }

    void Dodge(string dodger)
    {
        FightActions.Move(dodger);
    }

    IEnumerator FightRequest()
    {
        yield return new WaitForSeconds(1); //wait for one second before sending datahandling request so the server has time to generate a fight report
        DataHandling.fight = true;
    }
}
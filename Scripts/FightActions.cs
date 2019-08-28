using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightActions : MonoBehaviour
{
    public Transform firepoint1, firepoint2;
    public Text text;
    public GameObject projectile1, projectile2, cube1, cube2, active, user1hp, user2hp;
    public static string firer, dodger, winner;
    public static bool fire, move, flag, movecomplete, getposition, end, sethp, settext;
    private float position1, position2;
    public static int damage, user1health, user2health;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (settext)
        {
            settext = false;
            text.text = "Fight!";
        }

        if (fire)
        {
            fire = false;
            if (firer == "P1")
            {
                GameObject newObj;
                newObj = (GameObject)Instantiate(projectile1, firepoint1.position, firepoint1.rotation); //fire a projectile
                newObj.GetComponent<Projectile>().setDamage(damage);
            }
            if (firer == "P2")
            {
                GameObject newObj;
                newObj = (GameObject)Instantiate(projectile2, firepoint2.position, firepoint2.rotation);
                newObj.GetComponent<Projectile>().setDamage(damage);
            }
        }

        if (getposition)
        {
            getposition = false;
            if (dodger == "P1")
            {
                position1 = cube1.GetComponent<RectTransform>().position.x; //save current position to move back to later
                active = cube1;
            }
            else
            {
                position1 = cube2.GetComponent<RectTransform>().position.x;
                active = cube2;
            }
            move = true;
        }

        if (move)
        {
            RectTransform rect = active.GetComponent<RectTransform>();

            if (!flag)
            {
                if (rect.position.x <= 0.58f) //move until here
                {
                    rect.position = new Vector3((rect.position.x + 0.015f), rect.position.y);
                }
                else
                {
                    flag = true;
                }
            }
            else
            {
                if (rect.position.x >= position1) //and then move back
                {
                    rect.position = new Vector3((rect.position.x - 0.015f), rect.position.y);
                }
                else
                {
                    movecomplete = true;
                }
            }
        }

        if (movecomplete)
        {
            move = false;
            movecomplete = false;
            flag = false;
        }

        if (end)
        {
            end = false;
            text.text = winner + " wins!";
            if(winner == "P1")
            {
                Player2Health.die = true;
            }
            else
            {
                Player1Health.die = true;   
            }

            StartCoroutine(MoveBack());
        }
    }

    public static void Fire(string Firer, int dmg)
    {
        firer = Firer;
        fire = true;
        damage = dmg;        
    }

    public static void Move(string Dodger)
    {
        dodger = Dodger;
        getposition = true;

        string attacker;
        if (dodger == "P1")
        {
            attacker = "P2";
        }
        else
        {
            attacker = "P1";
        }

        Fire(attacker, 0); // bc it has to be dodging something
    }
    
    IEnumerator MoveBack()
    {
        yield return new WaitForSeconds(2);
        FightData.moveback = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public static bool move1, move2;
    // Start is called before the first frame update
    void Start()
    {
        move1 = move2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        Animator anim = GetComponent<Animator>(); //accesses the animator


        if (move1)
        {
            if (!anim.GetBool("toMain"))
            {     
                anim.SetBool("toMain", true);
                anim.SetBool("toBuild", false);
                move1 = false;
            }
        }
        if (move2)
        {
            if (!anim.GetBool("toBuild"))
            {
                anim.SetBool("toBuild", true);
                anim.SetBool("toMain", false);
                move2 = false;
            }
        }

        if (anim.GetBool("toMain"))
        {            
            anim.Play("cubeMoveToMain");
        }
        if (anim.GetBool("toBuild"))
        {
            anim.Play("cubeMoveToBuild");
        }
    }
}

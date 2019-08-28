using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TwitterIDEnter : MonoBehaviour
{
    public InputField twtinput;
    public static string twitterid;
    public GameObject wrongInput, stage1, stage2, sprite;
    private bool authorised = false, callagain = false;
    public static string data = "...";

    // Start is called before the first frame update
    void Start()
    {
        //setting the scene
        wrongInput.SetActive(false);
        sprite.SetActive(false);
        stage2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (callagain) //calls authorisation again
        {
            callagain = false;
            CallAgain();
        }
    }

    public void TwitterCheck()
    {
        twtinput = FindObjectOfType<InputField>(); //get the input field
        if(twtinput.text != "")
        {
            twitterid = twtinput.text;
            API.username = twitterid; //set username as id inputted

            stage1.SetActive(false); //change screen to authorisation
            sprite.SetActive(true);
            stage2.SetActive(true);
            
            SendData.authorisation = true; //send authorisation request
            Authorisation();
        }
        else
        {
            wrongInput.SetActive(true); //tells user to input again
        }
    }
    
    public void Send()
    {
        TwitterCheck(); //when button pressed run function
    }

    void Authorisation()
    {
        Contact(); //asks for authorisation response 
        if (!authorised)
        {
            callagain = true; //call again if not authorised
        }
        else
        {
            DataHandling.authorisation = false; //stop requests
            SceneManager.LoadScene("Quiz", LoadSceneMode.Single); //go to quiz
        }
    }

    void Contact()
    {
        DataHandling.authorisation = true; //get response

        if(data[0] == '1') //if response is '1' user is authorised
        {
            authorised = true;
        }
    }
    
    void CallAgain()
    {
        StartCoroutine(SendRequest()); //coroutine to wait for 5 seconds before running authorisation again, as to not overwhelm the server
    }

    IEnumerator SendRequest()
    {
        yield return new WaitForSeconds(5);
        Authorisation();
    }
}

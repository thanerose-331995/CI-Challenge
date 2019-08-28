using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizQuestions : MonoBehaviour
{
    public string[] type = { "em", "ex", "ex", "ex", "ex", "ex", "o", "o", "o", "o", "o", "o", "c", "c", "c", "c", "c", "c", "a", "a", "a", "a", "a", "a"};
    public string Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9, Q10, Q11, Q12, Q13, Q14, Q15, Q16, Q17, Q18, Q19, Q20, Q21, Q22, Q23, Q24;
    private int[] answered = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private bool[] answers = { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true };
    public int openScore, agreeScore, concScore, extraScore, emScore;
    public GameObject agree, disagree, start, endQuiz, counter;
    private int current, count = 0;
    private bool end;
    // Start is called before the first frame update
    void Start()
    {
        openScore = agreeScore = concScore = extraScore = 0; //setting the base values to 0
        emScore = 5; //this base value is 5 because theres only 1 emotional range question
    }

    // Update is called once per frame
    void Update()
    {
        if(count == 23)
        {
            end = true; //when the questions are all done end is true
        }
    }

    public void StartQuiz()
    {
        start.SetActive(false); //setup
        agree.SetActive(true);
        disagree.SetActive(true);
        ShowQuestion();    
    }

    void ShowQuestion()
    {
        string[] questions = { Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9, Q10, Q11, Q12, Q13, Q14, Q15, Q16, Q17, Q18, Q19, Q20, Q21, Q22, Q23, Q24 }; //all the questions
        counter.GetComponent<Text>().text = (count + 1) + "/24"; //show which question user is on
        if (!end) //not the end of the quiz
        {
            bool flag = true;

            while(flag){

                int numb = Random.Range(1, 24);
                if (answered[numb] != 1) // if question not answered
                {
                    gameObject.GetComponent<Text>().text = questions[numb];
                    count++;
                    current = numb;
                    flag = false;
                    answered[numb] = 1; //this question has been answered
                }
            }
        }
        else
        {
            //load endscreen
            gameObject.GetComponent<Text>().text = "Thanks for taking part, these scores will be compared with your Watson Personality scores from Twitter to track your progress.";
            endQuiz.SetActive(true);
            agree.SetActive(false);
            disagree.SetActive(false);
            Sort(questions); //sort the answers
        }
    }

    public void Agree()
    {
        answers[current] = true;
        ShowQuestion(); //next question
    }

    public void Disagree()
    {
        answers[current] = false;
        ShowQuestion();
    }

    public void EndQuiz()
    {
        SceneManager.LoadScene("AllScenes", LoadSceneMode.Single); //change screen
    }

    public void Sort(string[] questions)
    {
        for(int i = 0; i < questions.Length; i++) //for all of the questions
        {
            switch (type[i]) //each type
            {
                case "em":
                    if (answers[i]) //this answer is true
                    {
                        emScore += 1; //tally up
                    }
                    break;
                case "ex":
                    if (answers[i])
                    {
                        extraScore += 1;
                    }
                    break;
                case "o":
                    if (answers[i])
                    {
                        openScore += 1;
                    }
                    break;
                case "c":
                    if (answers[i])
                    {
                        concScore += 1;
                    }
                    break;
                case "a":
                    if (answers[i])
                    {
                        agreeScore += 1;
                    }
                    break;
                default:
                    break;
            }
        }
        
        int[] scores = { emScore, extraScore, openScore, concScore, agreeScore }; //final values

        for (int j = 0; j < 5; j++)
        {
            switch (j) //setting them to the struct values
            {
                case 0:
                    Ideals.idealEm = ((scores[j]));
                    break;
                case 1:
                    Ideals.idealEx = ((scores[j] * 10) / 5);
                    break;
                case 2:
                    Ideals.idealO = ((scores[j] * 10) / 6);
                    break;
                case 3:
                    Ideals.idealC = ((scores[j] * 10) / 6);
                    break;
                case 4:
                    Ideals.idealA = ((scores[j] * 10) / 6);
                    break;
                default:
                    break;
            }
        }

        //open, agree, conciencious, extra, emo,
        float[] values = { Ideals.idealO, Ideals.idealA, Ideals.idealC, Ideals.idealEx, Ideals.idealEm };
        UserProgress.Change(values, "ideals"); //send the values to userprogress to use later
        SendData.Answers(values); //send to senddata to send to system
        
    }
}

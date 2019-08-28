using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowData : MonoBehaviour
{
    public float openness;
    public float conscientiousness;
    public float extraversion;
    public float agreeableness;
    public float emotional_range;
    public string baseURL;
    public string users;
    public string userStats;
    public string createUser;
    public string username;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        openness = Data.openness;
        conscientiousness = Data.conscientiousness;
        extraversion = Data.extraversion;
        agreeableness = Data.agreeableness;
        emotional_range = Data.emotional_range;
        baseURL = API.baseURL;
        users = API.users;
        userStats = API.userStats;
        createUser = API.createUser;
        username = API.username;
    }
}

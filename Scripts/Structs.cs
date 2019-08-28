
// Assets/DataStructures/Post.cs
public struct Data //user data
{
    public string text_data;
    public static float openness;
    public static float conscientiousness;
    public static float extraversion;
    public static float agreeableness;
    public static float emotional_range;
}
public struct Robot //robot module data
{
    public static float powerLVL;
    public static string powerName;
    public static float shieldLVL;
    public static string shieldName;
    public static float movementLVL;
    public static string movemnentName;
    public static float weaponsLVL;
    public static string weaponsName;
    public static string powerup;
}
public class Modules //data for each module
{
    public string moduleType;
    public string moduleName;
    public int moduleLevel;
    public string iconName;
    public string description;
    public float modifier;

    public Modules(string type, string name, int level, string icon, string des, float mod)
    {
        moduleType = type;
        moduleName = name;
        moduleLevel = level;
        iconName = icon;
        description = des;
        modifier = mod;
    }
}
public class FightSequence //each entry in the fight sequence
{
    public string attacker;
    public string thisEvent;
    public int damage;

    public FightSequence(string attack, string Event, int dam)
    {
        attacker = attack;
        thisEvent = Event;
        damage = dam;
    }
}
public struct Ideals //the ideal values
{
    public static float idealO = 20;
    public static float idealC = 20;
    public static float idealEx = 20;
    public static float idealA = 20;
    public static float idealEm = 20;
}
public class API //request syntax
{
    public static string baseURL = "http://2.26.244.72/";
    public static string users = "Users/";
    public static string userStats = "/stats.txt";
    public static string robotStats = "/robotStats.txt";
    public static string createUser = "@";
    public static string username;
    public static string module = "/Modules";
    public static string[] types = { "/Power", "/Shields", "/Weapons", "/Movement", "/PowerUps" };
    public static string build = "$";
    public static string fight = "!";
    public static string questions = "%";
    public static string auth = "/auth.txt";

}
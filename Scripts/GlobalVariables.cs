using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour {

    //first is row of mze, second is column, third describes walls around and if traps on the current cell 
    public static int[,,] maze;
    
    //higher int = higher difficulty should NOT be zero, as it wil be a variable affecting the enemy parameters
    public static int difficulty;

    //
    public static int level;

    //
    public static float time;

    //
    public static float score;

    //
    public static int monstersPoints;

    //
    public static float timeMultiplier;

    //
    public static float levelMultiplier;

    void Update() {
        score = (timeMultiplier) * time + (levelMultiplier) * level + monstersPoints;
    }

}

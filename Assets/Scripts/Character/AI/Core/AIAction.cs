using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : ScriptableObject
{
    // Defines an action
    // THINK: "Actions" should be further refines into unique actions types;
    //      - Attacking: Has Tell/Start up, Has attack action, Has attack exit
    // THINK: All actions have a "total time" of action


    public abstract void Act(StateController controller);


    // Complex Action
    // List:
    //  | AI Action     |
    //  |   1. Action 1

}

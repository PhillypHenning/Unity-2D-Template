using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Simple_Idle_Action")]
public class Simple_Idle_Action : AIAction
{
    public override void Act(StateController controller)
    {
        Debug.Log("Idle Action ...");
    }
}

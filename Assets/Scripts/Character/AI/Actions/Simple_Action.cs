using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Simple_Action")]
public class Simple_Action : AIAction
{
    [SerializeField] string _ActionDebugName;
    public override void Act(StateController controller)
    {
        Debug.Log("Action in process: " + _ActionDebugName);
    }
}

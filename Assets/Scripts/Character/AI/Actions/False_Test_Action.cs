using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/False_Action")]
public class False_Test_Action : AIAction
{
    [SerializeField] string _Tag;

    public override void Act(StateController controller)
    {
        Debug.Log("In false state ["+_Tag+"]");
    }
}

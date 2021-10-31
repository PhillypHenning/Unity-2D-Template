using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/True_Action")]
public class True_Test_Action : AIAction
{
    [SerializeField] string _Tag;

    public override void Act(StateController controller)
    {
        Debug.Log("In true state ["+_Tag+"]");
    }
}

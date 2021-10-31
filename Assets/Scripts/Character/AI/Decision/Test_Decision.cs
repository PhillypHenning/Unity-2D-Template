using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Test_Decision")]
public class Test_Decision : AIDecision
{
    [SerializeField] bool _TestSimpleDecision = false;
    [Range(1,10)]
    [SerializeField] int _Priority = 1;

    public override Decision Decide(StateController controller)
    {
        Decision decision = new Decision();

         decision._DecisionResult = _TestSimpleDecision;
         decision._Priority = _Priority;

        return decision;
    }
}

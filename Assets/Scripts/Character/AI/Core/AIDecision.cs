using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : ScriptableObject
{
    public abstract Decision Decide(StateController controller);
}

public class Decision {
    public bool _DecisionResult;
    [Range(1,10)] // 1 = lowest; 10 = highest
    public int _Priority = 1;
}


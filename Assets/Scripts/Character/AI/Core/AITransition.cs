using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AITransition
{
    public AIDecision _Decision;
    public AIState _TrueState;
    public AIState _FalseState;
}

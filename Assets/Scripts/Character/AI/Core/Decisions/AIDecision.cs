using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AIDecision : ScriptableObject
{
    public abstract Decision Decide(StateController controller);
}


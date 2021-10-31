using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class AIAction : ScriptableObject
{
    public abstract void Act(StateController controller);
}

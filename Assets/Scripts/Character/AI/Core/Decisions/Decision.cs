using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Decision {
    public bool _DecisionResult;
    [Range(1,10)] // 1 = lowest; 10 = highest
    public int _Priority = 1;
}

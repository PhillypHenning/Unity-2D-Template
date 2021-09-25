using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : Health
{
    protected override void Start(){
        base.Start();
        _Character = GetComponent<Character>();
    }
}

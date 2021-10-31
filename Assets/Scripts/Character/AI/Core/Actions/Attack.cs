using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Attack
{
    // Attacks are made up of;
    //  1. Total time of attack
    //  2. Tell time for attack
    //  3. Attack time
    //  4. Outro for attack

    // General
    [SerializeField] private string _AttackName;
    [SerializeField] private float _TotalTimeOfAttack;

    // Tell
    [SerializeField] private float _TimeOfTell; // The time from start to finish for the tell to take place
    
    // Attack
    [SerializeField] private float _TimeOfAttack;
    

    // Outro
    [SerializeField] private float _TimeOfOutro;
}

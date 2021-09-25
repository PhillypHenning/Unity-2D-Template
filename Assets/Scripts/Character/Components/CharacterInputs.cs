using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputs : MonoBehaviour
{
    // Jumping
    [SerializeField] private KeyCode _JumpKeyCode = KeyCode.Space; 
    public KeyCode JumpKeyCode {get => _JumpKeyCode; set => _JumpKeyCode = value;}

    // Dodging
    [SerializeField] private KeyCode _DodgeKeyCode = KeyCode.LeftShift;
    public KeyCode DodgeKeyCode {get => _DodgeKeyCode; set => _DodgeKeyCode = value;}

    // Movement
    //[SerializeField] private KeyCode _MovementCode;
    // public Input MovementCode {get => _MovementCode; set => _MovementCode = value;}
    [SerializeField] private KeyCode _MovementLeftKeyCode = KeyCode.A;
    public KeyCode MovementLeftKeyCode {get => _MovementLeftKeyCode; set => _MovementLeftKeyCode = value;}
    [SerializeField] private KeyCode _MovementRightKeyCode = KeyCode.D;
    public KeyCode MovementRightKeyCode {get => _MovementRightKeyCode; set => _MovementRightKeyCode = value;}
}

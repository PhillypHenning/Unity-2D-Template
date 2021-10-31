using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Actionable state
    // Used to contain metadata of the character state

    // Private
    [Header("Timers")]
    private float _GameStartTime;
    private float _GameEndTime;

    [Header("Lockouts")]
    private bool _IsActionable; // Actionable is used to specify abilities.
    private bool _MovementLocked; // Movement Locked is to specify movement
    private bool _DirectionalLocked = false;
    public bool DirectionalLocked { get => _DirectionalLocked; set => _DirectionalLocked = value; }

    [Header("Movement Settings")]
    private bool _IsMoving;
    private bool _IsFalling;
    private bool _IsGrounded;
    private bool _IsFacingRight = true;
    [SerializeField] private Sensor _GroundSensor;
    public Sensor GroundSensor {get => _GroundSensor; set => _GroundSensor = value; }

    [Header("Wall Slide")]
    [SerializeField] private Sensor _SlidingSensorL1;
    [SerializeField] private Sensor _SlidingSensorR1;
    private bool _IsSliding = false;
    private bool _CanWallSlideJump = false;
    public Sensor SlidingSensorL1 {get => _SlidingSensorL1; set => _SlidingSensorL1 = value; }
    public Sensor SlidingSensorR1 {get => _SlidingSensorR1; set => _SlidingSensorR1 = value; }
    public bool IsSliding {get => _IsSliding; set => _IsSliding = value; }
    public bool CanWallSlideJump {get => _CanWallSlideJump; set => _CanWallSlideJump = value; }


    [Header("Jump")]
    // TODO: MOVE JUMP

    [Header("Dodge")]
    private bool _CanDodge = true;
    private bool _IsDodging = false;
    private float _TimeUntilDodgeIsDone = 0f;
    private float _DodgeCooldownFinish = 0f;
    [SerializeField] private float _DodgeCooldownDuraction;
    [SerializeField] private float _DodgeDistance = 3f;
    [SerializeField] private float _DodgeSpeed = 0.05f;
    public bool CanDodge { get => _CanDodge; set => _CanDodge = value; }
    public bool IsDodging { get => _IsDodging; set => _IsDodging = value; }
    
    public float DodgeCooldownFinish { get => _DodgeCooldownFinish; set => _DodgeCooldownFinish = value; }
    public float DodgeCooldownDuraction { get => _DodgeCooldownDuraction; set => _DodgeCooldownDuraction = value; }
    
    public float DodgeDistance { get => _DodgeDistance; set => _DodgeDistance = value; }
    public float DodgeSpeed { get => _DodgeSpeed; set => _DodgeSpeed = value; }

    
    [Header("Invun")]


    [Header("Health")]
    private bool _IsHitable;
    private bool _IsAlive = true;


    [Header("Settings")]
    private StateOfInteractions _StateOfInteraction;
    private LayerMask _OriginalLayer;
    private Rigidbody2D _RigidBody2D;

    [SerializeField] private LayerMask _CurrentLayer;
    [SerializeField] private CharacterTypes _CharacterType;
    [SerializeField] private GameObject _Sprite;

    [Header("Weapon Settings")]
    [SerializeField] private Transform _WeaponPosition;


    // Public 
    public bool IsAlive { get => _IsAlive; set => _IsAlive = value; }
    public bool IsActionable { get => _IsActionable; set => _IsActionable = value; }
    public bool MovementLocked { get => _MovementLocked; set => _MovementLocked = value; }
    public bool IsMoving { get => _IsMoving; set => _IsMoving = value; }
    public bool IsFalling { get => _IsFalling; set => _IsFalling = value; }
    public bool IsFacingRight { get => _IsFacingRight; set => _IsFacingRight = value; }
    public bool IsGrounded { get => _IsGrounded; set => _IsGrounded = value; }
    public bool IsHitable { get => _IsHitable; set => _IsHitable = value; }
    
    public CharacterTypes CharacterType { get => _CharacterType; set => _CharacterType = value; }
    public StateOfInteractions StateOfInteraction { get => _StateOfInteraction; set => _StateOfInteraction = value; }
    public Rigidbody2D RigidBody2D { get => _RigidBody2D; set => _RigidBody2D = value; }
    public Transform WeaponPosition { get => _WeaponPosition; set => _WeaponPosition = value; }
    
    // READ ONLY
    public LayerMask OriginalLayer => _OriginalLayer;
    public GameObject Sprite => _Sprite;

    // Enums
    public enum CharacterTypes {
        Inactive,
        Player,
        AI
    }

    public enum StateOfInteractions {
        // AI
        Inactive,
        Active,
        // Player
        Intro,
        CutScene,
        Playing,
        Pause,
        Locked
    }

    private void Start() {
        if(_CharacterType == CharacterTypes.Player){ StateOfInteraction = StateOfInteractions.Intro;}
        if(_CharacterType == CharacterTypes.AI){ StateOfInteraction = StateOfInteractions.Active; }

        _OriginalLayer = _CurrentLayer;

        RigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        
    }
}

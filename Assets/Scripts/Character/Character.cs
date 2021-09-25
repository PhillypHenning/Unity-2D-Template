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
    private bool _CharacterIsActionable; // Actionable is used to specify abilities.
    private bool _CharacterMovementLocked; // Movement Locked is to specify movement
    private bool _DirectionalLocked = false;
    public bool DirectionalLocked { get => _DirectionalLocked; set => _DirectionalLocked = value; }

    [Header("Movement Settings")]
    private bool _CharacterIsMoving;
    private bool _CharacterIsGrounded;
    private bool _CharacterIsFacingRight = true;
    [SerializeField] private Sensor _GroundSensor;
    public Sensor GroundSensor {get => _GroundSensor; set => _GroundSensor = value; }

    [Header("Jump")]
    // TODO: MOVE JUMP

    [Header("Dodge")]
    private bool _CharacterCanDodge = true;
    private bool _CharacterIsDodging = false;
    private float _TimeUntilDodgeIsDone = 0f;
    private float _DodgeCooldownFinish = 0f;
    [SerializeField] private float _DodgeCooldownDuraction;
    [SerializeField] private float _DodgeDistance = 3f;
    [SerializeField] private float _DodgeSpeed = 0.05f;
    public bool CharacterCanDodge { get => _CharacterCanDodge; set => _CharacterCanDodge = value; }
    public bool CharacterIsDodging { get => _CharacterIsDodging; set => _CharacterIsDodging = value; }
    
    public float DodgeCooldownFinish { get => _DodgeCooldownFinish; set => _DodgeCooldownFinish = value; }
    public float DodgeCooldownDuraction { get => _DodgeCooldownDuraction; set => _DodgeCooldownDuraction = value; }
    
    public float DodgeDistance { get => _DodgeDistance; set => _DodgeDistance = value; }
    public float DodgeSpeed { get => _DodgeSpeed; set => _DodgeSpeed = value; }

    
    [Header("Invun")]


    [Header("Health")]
    private bool _CharacterIsHitable;
    private bool _CharacterIsAlive = true;


    [Header("Settings")]
    private StateOfInteractions _CharacterStateOfInteraction;
    private LayerMask _OriginalLayer;
    private Rigidbody2D _CharacterRigidBody2D;

    [SerializeField] private LayerMask _CurrentLayer;
    [SerializeField] private CharacterTypes _CharacterType;
    [SerializeField] private GameObject _CharacterSprite;

    [Header("Weapon Settings")]
    [SerializeField] private Transform _WeaponPosition;


    // Public 
    public bool CharacterIsAlive { get => _CharacterIsAlive; set => _CharacterIsAlive = value; }
    public bool CharacterIsActionable { get => _CharacterIsActionable; set => _CharacterIsActionable = value; }
    public bool CharacterMovementLocked { get => _CharacterMovementLocked; set => _CharacterMovementLocked = value; }
    public bool CharacterIsMoving { get => _CharacterIsMoving; set => _CharacterIsMoving = value; }
    public bool CharacterIsFacingRight { get => _CharacterIsFacingRight; set => _CharacterIsFacingRight = value; }
    public bool CharacterIsGrounded { get => _CharacterIsGrounded; set => _CharacterIsGrounded = value; }
    public bool CharacterIsHitable { get => _CharacterIsHitable; set => _CharacterIsHitable = value; }
    
    public CharacterTypes CharacterType { get => _CharacterType; set => _CharacterType = value; }
    public StateOfInteractions CharacterStateOfInteraction { get => _CharacterStateOfInteraction; set => _CharacterStateOfInteraction = value; }
    public Rigidbody2D CharacterRigidBody2D { get => _CharacterRigidBody2D; set => _CharacterRigidBody2D = value; }
    public Transform WeaponPosition { get => _WeaponPosition; set => _WeaponPosition = value; }
    
    // READ ONLY
    public LayerMask OriginalLayer => _OriginalLayer;
    public GameObject CharacterSprite => _CharacterSprite;

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
        if(_CharacterType == CharacterTypes.Player){ CharacterStateOfInteraction = StateOfInteractions.Intro;}
        if(_CharacterType == CharacterTypes.AI){ CharacterStateOfInteraction = StateOfInteractions.Active; }

        _OriginalLayer = _CurrentLayer;

        CharacterRigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        
    }
}

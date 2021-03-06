using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : CharacterComponent
{
    // private
    private bool _CharacterIsJumping = false;
    private float _TimeSinceLastJump = 0f;
    private float _LowJumpModifier = 2.5f;
    private float _JumpStartPos;
    // private CharacterMovement _CharacterMovement;

    // Serialized
    [SerializeField] private float _FallMultiplier = 2.5f;
    [SerializeField] private float _GravityScaled;
    [SerializeField] private float _VerticalTakeOff = 15f;
    [SerializeField] private float _TimeBetweenJumps;
    [SerializeField] private float _WallJumpPushOff = 1000f;

    // TESTING
    [SerializeField] private bool WallJumpType2 = true;


    // public
    public float FallMultiplier { get => _FallMultiplier; set => _FallMultiplier = value; }
    public float GravityScaled { get => _GravityScaled; set => _GravityScaled = value; }
    public float VerticalTakeOff { get => _VerticalTakeOff; set => _VerticalTakeOff = value; }
    public float TimeBetweenJumps { get => _TimeBetweenJumps; set => _TimeBetweenJumps = value; }
    public float LowJumpModifier { get => _LowJumpModifier; set => _LowJumpModifier = value; }
    public bool CharacterIsJumping { get => _CharacterIsJumping; set => _CharacterIsJumping = value; }

    protected override void Start()
    {
        base.Start();
        // _CharacterMovement = GetComponent<CharacterMovement>();
    }

    protected override void HandleBasicComponentFunction()
    {
        DecideIfCharacterLanded();
        CalculateComponentData();
    }

    protected override void HandlePhysicsComponentFunction()
    {
        ApplyGravity();
    }

    protected override bool HandlePlayerInput()
    {
        if (!base.HandlePlayerInput()) return false;

        if (DecideIfCharacterCanJump()) Jump();
        if (DecideIfCharacterCanWallJump()) WallJump();

        return true;
    }

    protected override bool HandleAIInput()
    {
        // TODO: AI JUMP
        if (!base.HandleAIInput()) return false;
        return false;
    }

    private bool DecideIfCharacterCanJump()
    {
        // TODO: JUMP TIMEOUT
        // TODO: Add lockouts
        return _Character.GroundSensor.SensorActivated && JumpInput();
    }
    
    private bool DecideIfCharacterCanWallJump()
    {
        // TODO: JUMP TIMEOUT
        // TODO: Add lockouts
        return _Character.CanWallSlideJump && JumpInput();
    }
    private bool JumpInput()
    {
        return Input.GetKeyDown(CharacterInputs.JumpKeyCode);
    }

    private void Jump()
    {
        _JumpStartPos = transform.position.y;
        _Character.RigidBody2D.velocity = Vector2.up * VerticalTakeOff;
        CharacterIsJumping = true;
    }
    
    private void WallJump()
    {
        _JumpStartPos = transform.position.y;
        _Character.RigidBody2D.velocity = Vector2.up * VerticalTakeOff;
        
        float wallJumpVertical = WallJumpType2 ? _WallJumpPushOff : 0;
        float wallJumpPushOff =  !_Character.IsFacingRight ? _WallJumpPushOff : -_WallJumpPushOff;
        _Character.RigidBody2D.AddForce(new Vector2(wallJumpPushOff, wallJumpVertical), ForceMode2D.Force);
        CharacterIsJumping = true;
    }

    private void ApplyGravity()
    {
        if (_Character.RigidBody2D.velocity.y < 0f)
        {
            _Character.IsFalling = true;
            // Effects rigidbody with downward force
            _Character.RigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (FallMultiplier - GravityScaled) * Time.deltaTime;
        }
        else if (_Character.RigidBody2D.velocity.y > 0f)
        {
            _Character.IsFalling = false;
            // Creates "Video game" jump that has a snappier up and floaty down
            _Character.RigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (LowJumpModifier - GravityScaled) * Time.deltaTime;
        }
    }

    private void DecideIfCharacterLanded()
    {
        if (_Character.GroundSensor.SensorActivated && _Character.RigidBody2D.velocity.y <= 0f && CharacterIsJumping)
        {
            // TODO: Lots of things can be done here, but one that I think I may be interested in looking into is fall damage
            CharacterIsJumping = false;
            _Animation.ChangeAnimationState("Land", CharacterAnimation.AnimationType.Static);
        }
    }

    private void CalculateComponentData()
    { // TODO: MAYBE MOVE THIS TO THE COMPONENT CLASS
        if (!IsPlayer()) return;
        float curJumpHeight = transform.position.y;
        if (curJumpHeight > CharacterAchievements.HighestJumpedReached && _CharacterIsJumping)
        {
            CharacterAchievements.HighestJumpedReached = curJumpHeight;
        }
    }
}

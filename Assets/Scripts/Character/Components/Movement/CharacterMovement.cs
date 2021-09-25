using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : CharacterComponent
{
    // Private
    private float _HorizontalMovement;
    private float _VerticalMovement;
    // Protected

    // Serialized
    [SerializeField] private float _MovementSpeed;
    [SerializeField] private bool _UsesVerticalMovement = false;
    [SerializeField] private float _MaxSpeed = 100f;

    // Public
    public float HorizontalMovement { get => _HorizontalMovement; set => _HorizontalMovement = value; }
    public float VerticalMovement { get => _VerticalMovement; set => _VerticalMovement = value; }

    protected override void Start(){
        base.Start();
        SetLayerCollisionIgnores();
    }

    protected override void Update()
    {
        base.Update();
        DetectIfGrounded();
    }

    protected override void HandlePhysicsComponentFunction(){
        base.HandlePhysicsComponentFunction();
        if(_Character.DirectionalLocked) return;

        // Horizontal
        if(!_Character.CharacterMovementLocked && !_UsesVerticalMovement){
            var calc = _MovementSpeed * _HorizontalMovement;
            if(calc > _MaxSpeed) calc = _MaxSpeed;
            else if(calc < -_MaxSpeed) calc = -_MaxSpeed;
            _Character.CharacterRigidBody2D.AddForce(new Vector2(calc, 0), ForceMode2D.Impulse); // <-- Immediate force applied        
        }
        // Vertical 
        if(_UsesVerticalMovement && !_Character.CharacterMovementLocked){
            var calch = _MovementSpeed * _HorizontalMovement;
            var calcv = _MovementSpeed * _VerticalMovement;

            if(calch > _MaxSpeed) calch = _MaxSpeed;
            else if(calch < -_MaxSpeed) calch = -_MaxSpeed;

            if(calcv > _MaxSpeed) calcv = _MaxSpeed;
            else if(calcv < -_MaxSpeed) calcv = -_MaxSpeed;

            _Character.CharacterRigidBody2D.AddForce(new Vector2(calch, calcv), ForceMode2D.Force);        
        }
    }

    protected override bool HandlePlayerInput(){
        if(!base.HandlePlayerInput()) return false;

        _HorizontalMovement = Input.GetAxisRaw("Horizontal");
        if(_UsesVerticalMovement) _VerticalMovement = Input.GetAxisRaw("Vertical");

        if(!_Character.CharacterMovementLocked){
            _Character.CharacterIsMoving = _HorizontalMovement != 0;
            
            if(_Character.CharacterIsMoving){
                if(_Character.CharacterIsFacingRight && _HorizontalMovement < 0 || !_Character.CharacterIsFacingRight && _HorizontalMovement > 0){
                    FlipCharacter();
                }
            }
        }
        return true;
    }

    protected override bool HandleAIInput(){
        if(!base.HandleAIInput()) return false;
        // Movement values are handled through the public SetFunctions
        if(!_Character.CharacterMovementLocked){
            _Character.CharacterIsMoving = _HorizontalMovement != 0;
            if(_Character.CharacterIsMoving){
                if(_Character.CharacterIsFacingRight && _HorizontalMovement < 0 || !_Character.CharacterIsFacingRight && _HorizontalMovement > 0){
                    FlipCharacter();
                }

                // Character is moving.
                // Animation logic to be added here.
            }

        }
        return true;
    }

    private void FlipCharacter(){
        var character = _Character.CharacterSprite.transform;
        _Character.CharacterIsFacingRight = !_Character.CharacterIsFacingRight;
        character.localRotation = Quaternion.Euler(character.rotation.x, _Character.CharacterIsFacingRight ? 0 : -180, character.rotation.z);

        // Weapon Holder stuff may be best in their own script.
        // var weaponHolderPos = _WeaponHolder.transform.localPosition;
        // _WeaponHolder.transform.localPosition = new Vector3(weaponHolderPos.x * -1, weaponHolderPos.y, weaponHolderPos.z);
    }

    private void SetLayerCollisionIgnores(){
        // Ignore Layer collision would go in here.
        /* Sample of the old version.. 
        if (_Character.CharacterType == Character.CharacterTypes.Player){ Physics2D.IgnoreLayerCollision(9, 9); // <--  ignore collision with "Dead Bodies" while rolling
        }

        else if (_Character.CharacterType == Character.CharacterTypes.AI){Physics2D.IgnoreLayerCollision(8, 8);
        }
        */
    }

    public void MovePosition(Vector2 newPos){
        _Character.CharacterRigidBody2D.MovePosition(newPos);
    }

    private void DetectIfGrounded(){
        if(_Character.GroundSensor.SensorActivated) _Character.CharacterIsGrounded = true;
        else _Character.CharacterIsGrounded = false;
    }
}

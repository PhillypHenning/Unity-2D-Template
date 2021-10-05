using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : CharacterComponent
{
    // Private
    private float _HorizontalMovement;
    private float _VerticalMovement;
    private float _HorizontalForceApplied;
    private float _MovementCompoundValue = 0.015f;

    // Protected

    // Serialized
    [SerializeField] private float _MovementSpeed;
    [SerializeField] private bool _UsesVerticalMovement = false;
    [SerializeField] private float _MaxSpeed = 100f;
    [SerializeField] private float _DragToBeApplied = 3f;

    // Public
    public float HorizontalMovement { get => _HorizontalMovement; set => _HorizontalMovement = value; }
    public float VerticalMovement { get => _VerticalMovement; set => _VerticalMovement = value; }
    public float DragToBeApplied { get => _DragToBeApplied; set => _DragToBeApplied = value; }

    protected override void Start(){
        base.Start();
        SetLayerCollisionIgnores();
        _Character.CharacterRigidBody2D.drag = DragToBeApplied;
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
            _HorizontalForceApplied = _MovementSpeed * _HorizontalMovement;
            if(_HorizontalForceApplied > _MaxSpeed) _HorizontalForceApplied = _MaxSpeed;
            else if(_HorizontalForceApplied < -_MaxSpeed) _HorizontalForceApplied = -_MaxSpeed;
            _Character.CharacterRigidBody2D.AddForce(new Vector2(_HorizontalForceApplied, 0), ForceMode2D.Impulse); // <-- Immediate force applied
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

        //_HorizontalMovement = Input.GetAxis("Horizontal");
        CalcPlayerHorizontalInputs();
        CalcPlayerVerticalInputs();

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
                    //FlipCharacter();
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

    private void CalcPlayerHorizontalInputs(){
        // It's suggested online that KeyCodes are used over Input.RawAxis
        // This allows us to retain control over the Keybindings in the CharacterInput class.
        if(Input.GetKey(CharacterInputs.MovementLeftKeyCode) && Input.GetKey(CharacterInputs.MovementRightKeyCode)){
            _HorizontalMovement = 0;
        }
        else if(Input.GetKey(CharacterInputs.MovementLeftKeyCode)){
            if(_HorizontalMovement > -.9f){
                _HorizontalMovement += -_MovementCompoundValue;
            }
        }else if(Input.GetKey(CharacterInputs.MovementRightKeyCode)){
            if(_HorizontalMovement < .9f){ // .9 Has a really good feel to it with the current player values. 
                _HorizontalMovement += _MovementCompoundValue;
            }
        } 
        else{
            _HorizontalMovement = 0;
            // _Character.CharacterRigidBody2D.velocity = new Vector2(0,0);
        }
    }

    private void CalcPlayerVerticalInputs(){
        if(!_UsesVerticalMovement){return;}
        // TODO ^^ Based on the above Horizontal.
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDodge : CharacterComponent
{
    private Vector3 _StartPos;
    private Vector3 _EndPos;
    private float _Fraction = 0f;
    private bool _CharacterCanDodgeAgain = false;

    private CharacterMovement _CharacterMovement;

    protected override void Start()
    {
        base.Start();
        _CharacterMovement = GetComponent<CharacterMovement>();
    }

    protected override void HandlePhysicsComponentFunction(){
        CalculateDodge();
        CalculateDodgeReset();
    }

    protected override bool HandlePlayerInput(){
        if(!base.HandlePlayerInput()) return false;
        
        if(DecideIfCharacterCanDodge()) Dodge();

        return true;   
    }

    protected override bool HandleAIInput(){
        return base.HandleAIInput();

    }

    private bool DecideIfCharacterCanDodge(){        
        if(DodgeInput() && _Character.CharacterCanDodge && _CharacterCanDodgeAgain) return true;
        return false;
    }

    private bool DodgeInput(){
        if(Input.GetKeyDown(CharacterInputs.DodgeKeyCode)) return true;
        return false;
    }

    private void Dodge(){
        _Character.CharacterIsDodging = true;
        _Character.CharacterCanDodge = false;
        _Character.DirectionalLocked = true;
        _CharacterCanDodgeAgain = false;
        _CharacterMovement.HorizontalMovement = 0;
        
        _StartPos = transform.position;
        _EndPos = new Vector3(_StartPos.x + (_Character.CharacterIsFacingRight ? _Character.DodgeDistance : -_Character.DodgeDistance), _StartPos.y, 0);
    }

    private void CalculateDodge(){
        if (_Character.CharacterIsDodging)
        {
            if(_Fraction < 1){
                _Fraction += (_Character.DodgeDistance * _Character.DodgeSpeed);
                transform.position = Vector3.Lerp(_StartPos, _EndPos, _Fraction);
            }else{
                _Character.DirectionalLocked = false;
                _Character.CharacterIsDodging = false;
                _Character.CharacterCanDodge = true;
                _Fraction = 0f;
            }
        }
    }

    private void CalculateDodgeReset(){
        if(_Character.CharacterIsGrounded) _CharacterCanDodgeAgain = true;
        // HEY! LISTEN:
        // MuLtIpLe JuMp FlAgS cAn Be SeT hErE
    }

}

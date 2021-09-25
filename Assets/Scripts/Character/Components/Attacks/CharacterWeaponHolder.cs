using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeaponHolder : CharacterComponent
{
    [SerializeField] private Weapon _StartingWeapon;

    private Weapon _CurrentWeapon;
    private Weapon[] _WeaponsHeld;

    public Weapon CurrentWeapon { get => _CurrentWeapon; set => _CurrentWeapon = value; }

    protected override void Start()
    {
        base.Start();
        EquipWeapon(_StartingWeapon);
    }

    protected override void Update()
    {
        base.Update();
        EvaluateWeaponFlip();
    }

    private void EquipWeapon(Weapon weapon, Transform weaponPosition = null){
        if(weaponPosition == null) weaponPosition = _Character.WeaponPosition;

        // Swap Weapon Logic goes here

        CurrentWeapon = Instantiate(weapon, _Character.WeaponPosition);
        
        CurrentWeapon.WeaponOwner = _Character;
        // Reset projectile spawn position?
    }

    protected override bool HandlePlayerInput()
    {
        if(! base.HandlePlayerInput()) return false;
        
        if(DecideIfPlayerUseWeaponInput()) CurrentWeapon.InitiateUseWeapon();
        if(DecideIfPlayerUseReloadInput()) CurrentWeapon.InitiateReload();
        return true;
    }

    private bool DecideIfPlayerUseWeaponInput(){
        return Input.GetKeyDown(CharacterInputs.WeaponKeyCode);
    }

    private bool DecideIfPlayerUseReloadInput(){
        return Input.GetKeyDown(CharacterInputs.WeaponReloadKeyCode);
    }

    private void EvaluateWeaponFlip(){
        if(!_Character.CharacterIsFacingRight) { 
            if(_Character.WeaponPosition.transform.localPosition.x < 0) return;
            _Character.WeaponPosition.transform.localPosition = new Vector3(_Character.WeaponPosition.transform.localPosition.x * -1, _Character.WeaponPosition.transform.localPosition.y, _Character.WeaponPosition.transform.localPosition .z);
            _CurrentWeapon.FlipWeapon();
        }
        else if(_Character.CharacterIsFacingRight) {
            if(_Character.WeaponPosition.transform.localPosition.x > 0) return;
            _Character.WeaponPosition.transform.localPosition = new Vector3(_Character.WeaponPosition.transform.localPosition.x * -1, _Character.WeaponPosition.transform.localPosition.y, _Character.WeaponPosition.transform.localPosition .z);
            _CurrentWeapon.FlipWeapon();
        }
    }


}

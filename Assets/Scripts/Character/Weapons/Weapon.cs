using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Unity 
    [SerializeField] protected string _WeaponName;
    [SerializeField] protected float _TimeBetweenUse = 0.5f;
    [SerializeField] protected float _TimeUntilNextUse = 0f;
    [SerializeField] protected WeaponTypes _WeaponType;

    // private
    protected Character _WeaponOwner;
    protected bool _WeaponInAction = false;
    protected bool _WeaponIsUsable; // Used for internal flagging
    protected bool _WeaponIsEnabled = true; // Used for external control of weapons

    // public
    public Character WeaponOwner { get => _WeaponOwner; set => _WeaponOwner = value; }
    public WeaponTypes WeaponType { get => _WeaponType; set => _WeaponType = value; }
    public string WeaponName { get => _WeaponName; set => _WeaponName = value; }
    public bool WeaponInAction { get => _WeaponInAction; set => _WeaponInAction = value; }
    public bool WeaponIsUsable { get => _WeaponIsUsable; set => _WeaponIsUsable = value; }
    public float TimeBetweenUse { get => _TimeBetweenUse; set => _TimeBetweenUse = value; }
    public float TimeUntilNextUse { get => _TimeUntilNextUse; set => _TimeUntilNextUse = value; }

    public enum WeaponTypes
    {
        Melee,
        Projectile
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    protected virtual void EvaluateIfUsable()
    {

    }

    protected virtual void AssignOwner(Character character)
    {
        _WeaponOwner = character;
    }

    public virtual void InitiateAssignOwner(Character character)
    {
        AssignOwner(character);
    }


    protected virtual void UseWeapon()
    {
        // if(!WeaponIsUsable) return;
        // What does it mean to use a weapon?

    }

    public virtual void InitiateUseWeapon()
    {
        // Should call the private function and nothing else. 
        UseWeapon();
    }

    protected virtual void UseReload()
    {

    }

    public virtual void InitiateReload()
    {
    }

    public void FlipWeapon(){
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}

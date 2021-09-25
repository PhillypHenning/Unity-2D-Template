using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [SerializeField] private bool _UsesBullets = true;
    [SerializeField] private bool _UsesReload = true;

    [SerializeField] private float _MaxMagazineSize = 10;
    [SerializeField] private Transform _ProjectileSpawnPosition;

    // private ObjectPooler;

    private float _CurrentAmmo;
    private bool _CanShoot;


    public bool UsesBullets { get => _UsesBullets; set => _UsesBullets = value; }
    public bool UsesReload { get => _UsesReload; set => _UsesReload = value; }

    public float MaxMagazineSize { get => _MaxMagazineSize; set => _MaxMagazineSize = value; }
    public float CurrentAmmo { get => _CurrentAmmo; set => _CurrentAmmo = value; }

    public Transform ProjectileSpawnPosition { get => _ProjectileSpawnPosition; set => _ProjectileSpawnPosition = value; }

    private void Awake()
    {
        RefillAmmo();
        WeaponIsUsable = true;

    }

    protected override void Start()
    {
        WeaponType = WeaponTypes.Projectile;
        if (UsesBullets)
        {
            // ObjectPooler instantiation
        }
    }

    protected override void Update()
    {
        EvaluateIfUsable();
    }

    protected override void EvaluateIfUsable()
    {
        if(Time.time > TimeUntilNextUse && WeaponInAction){
            WeaponInAction = false;
        }
    }

    private void RefillAmmo()
    {
        _CurrentAmmo = _MaxMagazineSize;
    }

    protected override void UseWeapon()
    {
        if (DecideIfWeaponIsUsable()) UseProjectileWeapon();
    }

    private void UseProjectileWeapon()
    {
        if (_CurrentAmmo <= 0)
        {
            WeaponEmpty();
            return;
        }

        HandleProjectile();
        WeaponInAction = true;
        TimeUntilNextUse = Time.time + TimeBetweenUse;

        Debug.Log("Weapon was used");
    }

    private void WeaponEmpty()
    {
        // Weapon is Empty
        Debug.Log("Weapon is empty..");
    }

    private void HandleProjectile(){
        Debug.Log("Projectile Spawned.. ");

        _CurrentAmmo-=1;
    }

    override protected void UseReload(){
        _CurrentAmmo = _MaxMagazineSize;
        Debug.Log("Reloading..");
    }

    override public void InitiateReload(){
        UseReload();
    }

    private bool DecideIfWeaponIsUsable(){
        if(WeaponIsUsable && !WeaponInAction) return true;
        return false;
    }
}

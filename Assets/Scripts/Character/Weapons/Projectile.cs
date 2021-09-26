using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] protected float _StartingSpeed = 0f;
    [SerializeField] protected float _Acceleration = 0f;
    [SerializeField] protected float _ProjectileDamage = 1f;
    [SerializeField] protected ProjectileTypes _ProjectileType;

    protected Rigidbody2D _ProjectileRigidBody2D;
    protected SpriteRenderer _ProjectileSpriteRender;
    protected Vector2 _ProjectileMovement;
    protected ReturnToPool _ProjectileReturnToPool;
    protected Character _ProjectileOwner;
    protected float _TotalSpeed = 0f;

    public Vector2 Direction { get; set; }
    public float Speed {get => _TotalSpeed; set => _TotalSpeed = value;}
    public Character ProjectileOwner { get => _ProjectileOwner; set => _ProjectileOwner = value; }
    public ProjectileTypes ProjectileType {get => _ProjectileType; set=> _ProjectileType = value; }

    public enum ProjectileTypes
    {
        Bullet
    }

    private void Awake() {
        _ProjectileRigidBody2D = GetComponent<Rigidbody2D>();
        _ProjectileSpriteRender = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        SetLayerCollisionIgnores();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void FixedUpdate(){
        MoveProjectile();
    }

    protected virtual void SetLayerCollisionIgnores(){

    }

    protected virtual void MoveProjectile(){

    }

    protected virtual void FlipProjectile(){
        _ProjectileSpriteRender.flipX = !_ProjectileSpriteRender.flipX;
    }

    public  virtual void SetDirection(Vector2 newDirection, Quaternion newRotation, bool isFacingRight=true){
        Direction = newDirection;
        if(!isFacingRight){
            FlipProjectile();
        }
        transform.rotation = newRotation;
    }

    public void Reset(){
        _ProjectileSpriteRender.flipX = false;
        Speed = _StartingSpeed;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        // This will need to improve drastically.
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _Speed = 10f;
    [SerializeField] private float _Acceleration = 0f;
    [SerializeField] private float _ProjectileDamage = 1f;

    private Rigidbody2D _ProjectileRigidBody2D;
    private SpriteRenderer _ProjectileSpriteRender;
    private Vector2 _ProjectileMovement;
    private ReturnToPool _ProjectileReturnToPool;
    private Character _ProjectileOwner;

    public Vector2 Direction { get; set; }
    public float Speed {get => _Speed; set => _Speed = value;}
    public Character ProjectileOwner { get => _ProjectileOwner; set => _ProjectileOwner = value; }

    private void Start() {
        _ProjectileRigidBody2D = GetComponent<Rigidbody2D>();
        _ProjectileSpriteRender = GetComponent<SpriteRenderer>();
        SetLayerCollisionIgnores();
    }

    private void Update() {
        
    }

    private void FixedUpdate()
    {
        MoveProjectile();
    }

    private void SetLayerCollisionIgnores(){
        // State what is being ignored in a comment please.
    }

    private void MoveProjectile(){
        _ProjectileMovement = Direction * Speed * Time.deltaTime;
        _ProjectileRigidBody2D.MovePosition(_ProjectileRigidBody2D.position + _ProjectileMovement);

        // REMINDER: Having Acceleration and Speed at the same value will result in immediate force
        Speed += _Acceleration * Time.deltaTime;
    }

    public void FlipProjectile(){
        // Flips to the opposite of what it currently is. 
        Debug.Log("Projectile Fliipped");
        _ProjectileSpriteRender.flipX = !_ProjectileSpriteRender.flipX;
    }

    public void SetDirection(Vector2 newDirection, Quaternion newRotation, bool isFacingRight=true){
        Direction = newDirection;
        if(!isFacingRight){
            FlipProjectile();
        }
        transform.rotation = newRotation;
    }

    public void Reset(){
        Debug.Log("Reset Called");
        _ProjectileSpriteRender.flipX = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // This will need to improve drastically.
    }

}

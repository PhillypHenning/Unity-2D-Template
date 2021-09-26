using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    [SerializeField] private float _LifeTime = 2f;
    private Projectile _Projectile;

    void Start()
    {
        _Projectile = GetComponent<Projectile>();
    }

    private void Return(){
        if(_Projectile != null){
            _Projectile.Reset();
        }
        gameObject.SetActive(false);
    }

    private void OnEnable(){
        Invoke(nameof(Return), _LifeTime);
    }

    private void OnDisable(){
        CancelInvoke();

    }

    public void DestroyObject(){
        Return();
    }
}

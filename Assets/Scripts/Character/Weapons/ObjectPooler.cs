using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject _ObjectPrefab;
    [SerializeField] private int _PoolSize = 10;

    private Weapon _Weapon;
    private GameObject _ParentPooledObject;
    private List<GameObject> _PooledObjects;
    private string _ObjectPoolerName;

    public string ObjectPoolerName { get => _ObjectPoolerName; set => _ObjectPoolerName = value; }
    public Weapon Weapon { get => _Weapon; set => _Weapon = value; }
    public GameObject ParentPoolObject { get => _ParentPooledObject; set => _ParentPooledObject = value; }
    public List<GameObject> PooledObjects { get => _PooledObjects; set => _PooledObjects = value; }
    public int PoolSize {get => _PoolSize; set => _PoolSize = value; }

    private void Start()
    {
        Weapon = GetComponent<Weapon>();
        ObjectPoolerName = _Weapon.WeaponOwner.CharacterType + " " + _Weapon.WeaponName + " " + ObjectPoolerName + " Pool";
        
        ParentPoolObject = new GameObject(ObjectPoolerName);

        Refill();
    }

    private void Refill(){
        if(PooledObjects == null){
            PooledObjects = new List<GameObject>();
            for (int i = 0; i < _PoolSize; i++)
            {
                AddGameObjectToPool();
            }
        }
    }

    private void AddGameObjectToPool(){
        GameObject newObject = Instantiate(_ObjectPrefab, _ParentPooledObject.transform);
        newObject.SetActive(false);
        _PooledObjects.Add(newObject);
    } 

    public GameObject GetGameObjectFromPool(){
        for (int i = 0; i < _PooledObjects.Count; i++)
        {
            // Check if the pooledObject is already active, if it isn't;
            if (!_PooledObjects[i].activeInHierarchy)
            {
                return _PooledObjects[i];
            }
        
            if (i == _PooledObjects.Count - 2)
            {
                AddGameObjectToPool();
            }
        }

        return null;
    }
}

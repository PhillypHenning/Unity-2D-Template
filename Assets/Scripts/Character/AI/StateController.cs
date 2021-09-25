using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    // Private

    // Serialized
    [SerializeField] private AITypes _AIType;

    // Public
    public AITypes AIType { get => _AIType; set => _AIType = value; }
    public enum AITypes {
        None,
        Basic,
        Miniboss,
        Boss
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

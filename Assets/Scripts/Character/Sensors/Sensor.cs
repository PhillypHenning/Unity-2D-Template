using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    protected Collider2D _Sensor;
    protected bool _SensorActivated = false;

    public bool SensorActivated {get => _SensorActivated; set => _SensorActivated = value;}

    // Start is called before the first frame update
    void Start()
    {
        _Sensor = GetComponent<Collider2D>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    protected virtual void OnTriggerExit2D(Collider2D other) {
        
    }
}

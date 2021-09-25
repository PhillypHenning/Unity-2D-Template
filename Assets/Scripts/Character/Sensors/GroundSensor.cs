using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : Sensor
{
    protected override void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Platform"){
            SensorActivated = true;
        }
    }

    protected override void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Platform"){
            SensorActivated = false;
        }
    }

}

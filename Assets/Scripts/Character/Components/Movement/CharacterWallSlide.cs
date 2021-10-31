using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWallSlide : CharacterComponent
{


    protected override void HandleBasicComponentFunction()
    {
        // Check if the character is in a slide state
        DecideIfCharacterWallSliding();
        WallSlide();
    }

    private void DecideIfCharacterWallSliding(){
        // If the Knee sensors are activated && character is not touching the ground
        if(!_Character.GroundSensor.SensorActivated && (_Character.SlidingSensorL1.SensorActivated || _Character.SlidingSensorR1.SensorActivated)) _Character.IsSliding = true; 
        if(_Character.GroundSensor.SensorActivated || (!_Character.SlidingSensorL1.SensorActivated && !_Character.SlidingSensorR1.SensorActivated)) _Character.IsSliding = false;
    }

    private void WallSlide(){
        if(!_Character.IsSliding){ 
            _Character.CanWallSlideJump = false;
            return; 
        }
        // Character Should be able to jump
        _Character.CanWallSlideJump = true;
    }
}

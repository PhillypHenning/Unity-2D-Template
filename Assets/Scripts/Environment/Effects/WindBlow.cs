using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBlow : MonoBehaviour
{
    [SerializeField] private float WindBlowingForce = 50;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {   
            Character character = other.GetComponent<Character>();
            if(!character.IsInWind){
                Debug.Log("Entered the wind ...");
                character.IsInWind = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Character character = other.GetComponent<Character>();
            CharacterMovement characterMovement = other.GetComponent<CharacterMovement>();
            characterMovement.EnviromentalForceApplied = WindBlowingForce;
            //character.RigidBody2D.AddRelativeForce(new Vector2(10,0));
            
            Debug.Log("In the wind");

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Player")
        {   
            Character character = other.GetComponent<Character>();
            CharacterMovement characterMovement = other.GetComponent<CharacterMovement>();
            if(character.IsInWind){
                Debug.Log("Left the wind ... ");
                character.IsInWind = false;
                characterMovement.EnviromentalForceApplied = 0;
            }
        }
        
    }
}

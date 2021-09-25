using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterComponent : MonoBehaviour
{
    protected Character _Character;
    protected ComponentAchievements _CharacterAchievements;
    protected CharacterInputs _CharacterInputs;

    public ComponentAchievements CharacterAchievements { get => _CharacterAchievements; set => _CharacterAchievements = value; }
    public CharacterInputs CharacterInputs { get => _CharacterInputs; set => _CharacterInputs = value; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _Character = GetComponent<Character>();
        CharacterAchievements = GetComponent<ComponentAchievements>();
        CharacterInputs = GetComponent<CharacterInputs>();
    }

    // Calculations, checks and the such should be in here
    protected virtual void Update()
    {
        HandleInput();
        HandleBasicComponentFunction();
    }

    // Physics based in here
    private void FixedUpdate() {
        HandlePhysicsComponentFunction();
    }

    protected virtual void HandleInput(){
        HandlePlayerInput();
        HandleAIInput();
    }

    protected virtual bool HandlePlayerInput(){
        return IsPlayer();
    }

    protected virtual bool HandleAIInput(){
       return IsAI();
    }

    protected virtual void HandleBasicComponentFunction(){

    }

    protected virtual void HandlePhysicsComponentFunction(){

    }

    protected virtual bool IsPlayer(){
        return _Character.CharacterType == Character.CharacterTypes.Player;
    }

    protected virtual bool IsAI(){
        return _Character.CharacterType == Character.CharacterTypes.AI;
    }
}

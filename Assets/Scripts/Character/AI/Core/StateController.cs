using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    // Private
    private Character _Character;
    private GameObject _Target;
    private AIState _TransitionState;
    private Decision _TransitionContext;
    
    // Serialized
    [SerializeField] private AIState _CurrentState;
    [SerializeField] private AIState _RemainState;

    // Public 
    public Character Character {get => _Character; set => _Character = value;}
    public GameObject Target {get => _Target; set => _Target = value;}
    
    // Start is called before the first frame update
    void Start()
    {
        _Character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Check if Character is Actionable
        _CurrentState.EvaluateState(this);
    }

    public void TransitionToState(AIState nextState, Decision decision){
        /*
        TODO: decision is a new addition. This adds priority to the decision.
        */
        Debug.Log("Transition To State called by: " + nextState.name + " With priority: " + decision._Priority);
        if(nextState != _RemainState){
            if(_TransitionContext == null || _TransitionState == null){
                _TransitionContext = decision;
                _TransitionState = nextState;
            }
            
            if(_TransitionContext._Priority < decision._Priority){
                _TransitionContext = decision;
                _TransitionState = nextState;
            }
        }
    }

    public void EvaluateTransitions(){
        // Original code
        if(_CurrentState == _TransitionState || _TransitionState == null) return;
        Debug.Log("Transitioning State from: " + _CurrentState.name + " To State: " + _TransitionState.name);
        _CurrentState = _TransitionState;
        ResetTransitions();
    }

    public void ResetTransitions(){
        _TransitionState = null;
        _TransitionContext = null;
    }

}

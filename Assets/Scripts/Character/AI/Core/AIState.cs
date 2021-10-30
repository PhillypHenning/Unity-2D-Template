using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "AI/State/State_")]
public class AIState : ScriptableObject {
    // The AIState is the most fleshed out of the AI* models
    // It takes the data and context of the other AI* components and processes them
    public AIAction[] _AIActions;
    public AITransition[] _AITransitions;

    public void EvaluateState(StateController controller){
        if(controller == null){
            Debug.Log("AI Statecontroller is emptry...");
            return;
        }


        // Perform action first, than evaluate. If this is being called for the first time, the action should go off.
        PerformActions(controller);
        EvaluateTransitions(controller);
    }

    public void EvaluateTransitions(StateController controller){
        if(_AITransitions != null || _AITransitions.Length > 1){
            //Debug.Log("AI Transitions in queue: " + _AITransitions);
            foreach(AITransition transition in _AITransitions){
                Decision decision = transition._Decision.Decide(controller);
                
                if(decision._DecisionResult){
                    controller.TransitionToState(transition._TrueState, decision);
                }else{
                    controller.TransitionToState(transition._FalseState, decision);
                }
            }

            controller.EvaluateTransitions();

        }
    }

    public void PerformActions(StateController controller){
        if(_AIActions != null || _AIActions.Length > 1){
            //Debug.Log("AI Actions in queue: " + _AIActions);
            foreach(AIAction action in _AIActions){
                action.Act(controller);
                //controller.ResetTransitions();
            }
        }
    }
}


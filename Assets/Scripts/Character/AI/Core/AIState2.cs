using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState2 : ScriptableObject
{
    public AIAction _AIMicroAction;
    public AIAction[] _AIMacroActions;
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
        PerformMicroActions(controller);
        PerformMacroActions(controller);

    }

    public void PerformMicroActions(StateController controller){
        // If the states micro action is blank, carry over the _RemainAction from the controller
        if(_AIMicroAction == null){

        }
    }

    public void PerformMacroActions(StateController controller){
        if(_AIMacroActions != null || _AIMacroActions.Length > 1){
            //Debug.Log("AI Actions in queue: " + _AIActions);
            foreach(AIAction action in _AIMacroActions){
                action.Act(controller);
                //controller.ResetTransitions();
            }
        }
    }
}

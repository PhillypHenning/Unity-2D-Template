using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator _Animator;
    private Character _Character;

    private float _StaticAnimationTime;
    private float _PriorityAnimationTime;


    private Dictionary<string, float> _AnimationTimes = new Dictionary<string, float>();
    private string _CurrentAnimation;


    public Dictionary<string, float> AnimationTimes => _AnimationTimes;

    /// Dynamic animations are fluid and handeled within the UpdateAnimations function
    /// Static animations will prevent dynamic animation logic until it has played through
    /// Priority animations will prevent ALL other animations until it has played through
    public enum AnimationType
    {
        Dynamic,
        Static,
        Priority
    }

    private void Start()
    {
        _Animator = GetComponentInChildren<Animator>();
        _Character = GetComponent<Character>();

        if (_Character == null) print("CharacterAnimation couldn't find Character to assign to _Character.");
        if (_Animator == null) print("CharacterAnimation couldn't find Animator component to assign to _Animator.");

        UpdateAnimationTimes();
    }

    // Populates _AnimationTime Dict with animation states and their respective durations
    private void UpdateAnimationTimes()
    {
        AnimationClip[] clips = _Animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            //Debug.Log(String.Format("[{0}] = {1}", clip.name, clip.length));
            _AnimationTimes.Add(clip.name, clip.length);
        }
    }

    private void Update()
    {
        UpdateAnimationCooldowns();
        DynamicAnimations();
        /* for testing purposes */
        if (!Input.GetKeyDown(KeyCode.Mouse0)) return;

        ChangeAnimationState(_Character.IsGrounded ? "Attack" : "AirAttack", AnimationType.Static);
    }

    private void DynamicAnimations()
    {
        if (PriorityAnimationPlaying() || StaticAnimationPlaying() || !_Character.IsAlive) return;

        if (_Character.IsGrounded)
        {
            // if the current animation state is "falling" then we know to play the landing anim
            if (_CurrentAnimation == "Falling")
            {
                ChangeAnimationState("landing", AnimationType.Static);
            }
            else if (_Character.IsMoving)
            {
                ChangeAnimationState("Run");
            }
            else
            {
                ChangeAnimationState("Idle");
            }
        }
        else if (_Character.IsFalling)
        {
            // if the current animation state is "jump", then we know to play "jumpToFall"
            ChangeAnimationState("Fall");
        }
        else
        {
            ChangeAnimationState("Jump");
        }
    }

    private void UpdateAnimationCooldowns()
    {
        if (StaticAnimationPlaying()) _StaticAnimationTime -= Time.deltaTime;
        if (PriorityAnimationPlaying()) _PriorityAnimationTime -= Time.deltaTime;
    }

    private bool StaticAnimationPlaying()
    {
        return _StaticAnimationTime > 0;
    }

    private bool PriorityAnimationPlaying()
    {
        return _PriorityAnimationTime > 0;
    }

    /// <summary>
    /// Updates the AnimationState using the name of desired animation clip and an animation type.<br />
    /// Available Types:<br />
    ///   Dynamic  - handeled automatically using the states within Charater class (eg. IsFalling)<br />
    ///   Static - stops Dynamic and Static animations until it is played through once or interrupted by a Static or Priority animation<br />
    ///   Priority - stops all animation until it is played through once or interrupted by another Priority animation
    /// </summary>
    /// <param name="newState">Represents the name of the desired animation clip.</param>
    /// <param name="animationType">Uses the AnimationType enum to determine the priority level of the animation. Defaults to the lowest priority, Dynamic.</param>
    public void ChangeAnimationState(string newState, AnimationType animationType = AnimationType.Dynamic)
    {
        if (_CurrentAnimation == newState) return;

        if (!_AnimationTimes.ContainsKey(newState))
        {
            Debug.LogWarning(String.Format("{0} was not found in the AnimationTimes dict", newState));
            return;
        }

        if (PriorityAnimationPlaying() && animationType != AnimationType.Priority) return;

        _Animator.Play(newState);

        _CurrentAnimation = newState;

        switch (animationType)
        {
            case (AnimationType.Static):
                SetStaticAnimationDelay(_AnimationTimes[newState]);
                break;
            case (AnimationType.Priority):
                SetPriorityAnimationDelay(_AnimationTimes[newState]);
                break;
            default:
                break;
        }
    }

    private void SetStaticAnimationDelay(float delay)
    {
        _StaticAnimationTime = delay;
    }

    private void SetPriorityAnimationDelay(float delay)
    {
        _PriorityAnimationTime = delay;
    }
}

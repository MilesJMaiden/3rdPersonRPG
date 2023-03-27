using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//MonoBehaviour is a component to be on a GO (REMOVE)
public abstract class State //Abstract, you cannot create a state BUT you can inherit it
{
    public abstract void Enter(); //When Entering State //IF you are a State, you MUST have the below methods

    public abstract void Tick(float deltaTime); //Everyframe

    public abstract void Exit(); //When Exiting state

    protected float GetNormalizedTime(Animator animator) //for anims
    {
        //Uses unity Anim TAGS
        //current state > next state
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0); //current info
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(1); //Next anim info

        if (animator.IsInTransition(0) && nextInfo.IsTag("Attack")) //If we are transitioning into an attack
        {
            return nextInfo.normalizedTime; //how far through the animation
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag("Attack")) //if not transition but playing attack animation
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0;
        }
    }

}


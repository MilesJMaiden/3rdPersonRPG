using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Cinemachine;
using UnityEngine;


public class Targeter : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup cinemachineTargetGroup;

    [SerializeField] private List<Target> targets = new List<Target>();
    public Target currentTarget { get; private set; } //currentTarget can on be set in this script

    private void OnTriggerEnter(Collider other) //Unity method - when something enters collider
    {
        //Target target = other.GetComponent<Target>();
        //if(target == null) { return; } 
        
        //Refactor below

        if(!other.TryGetComponent<Target>(out Target target)) { return; }

        //If target has enterted the collider, it will be added to list
        targets.Add(target);
        target.OnDestroyed += RemoveTarget;
        
    }

    private void OnTriggerExit(Collider other) //Unity method - when something exits collider
    {
        //Target target = other.GetComponent<Target>();
        //if (target == null) { return; }
        //Refactor below

        if (!other.TryGetComponent<Target>(out Target target)) { return; }

        //If target has exited the collider, it will be removed from the list
        RemoveTarget(target);
    }

    public bool SelectTarget() //returns a BOOL
    {
        if(targets.Count == 0) { return false; }

        currentTarget = targets[0];

        cinemachineTargetGroup.AddMember(currentTarget.transform, 1f, 2f);

        return true;
    }

    public void Cancel()
    {
        if(currentTarget == null) { return ; }

        cinemachineTargetGroup.RemoveMember(currentTarget.transform);

        currentTarget = null;
    }

    private void RemoveTarget(Target target)
    {
        if(currentTarget == target)
        {
            cinemachineTargetGroup.RemoveMember(currentTarget.transform);
            currentTarget = null;
        }

        target.OnDestroyed -= RemoveTarget; //unsub
        targets.Remove(target);
    }
}

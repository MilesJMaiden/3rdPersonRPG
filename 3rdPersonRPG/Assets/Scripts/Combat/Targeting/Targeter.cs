using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Cinemachine;
using UnityEngine;


public class Targeter : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup cinemachineTargetGroup;

    [SerializeField] private List<Target> targets = new List<Target>();

    private Camera mainCamera;
    public Target currentTarget { get; private set; } //currentTarget can on be set in this script

    private void Start()
    {
        mainCamera = Camera.main;
    }

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

        Target closestTarget = null;
        float closestTargetDistance = Mathf.Infinity;

        //loop through all targets to identify which is closest to the middle of view
        foreach (Target target in targets) 
        {
            Vector2 viewPosition = mainCamera.WorldToViewportPoint(target.transform.position);   
            
            //if its visible to the player
            if(viewPosition.x < 0 || viewPosition.x > 1 || viewPosition.y < 0 || viewPosition.y > 1)
            {
                continue;
            }

            currentTarget = targets[0];

            //how far from center
            Vector2 toCenter = viewPosition - new Vector2(0.5f, 0.5f);
            //Magnitude tells you how big a vector is //SqrMagnitude is more optimal
            if(toCenter.sqrMagnitude <  closestTargetDistance)
            {
                closestTarget = target;
                closestTargetDistance = toCenter.sqrMagnitude;
            }
        } 

        if(closestTarget == null) { return false; } // we wont enter target state (no target)

        currentTarget = closestTarget;
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

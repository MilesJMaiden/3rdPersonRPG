using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    public List<Target> targets = new List<Target>();

    private void OnTriggerEnter(Collider other) //Unity method - when something enters collider
    {
        //Target target = other.GetComponent<Target>();
        //if(target == null) { return; } 
        
        //Refactor below

        if(!other.TryGetComponent<Target>(out Target target)) { return; }

        //If target has enterted the collider, it will be added to list
        targets.Add(target);
        
    }

    private void OnTriggerExit(Collider other) //Unity method - when something exits collider
    {
        //Target target = other.GetComponent<Target>();
        //if (target == null) { return; }
        //Refactor below

        if (!other.TryGetComponent<Target>(out Target target)) { return; }

        //If target has exited the collider, it will be removed from the list
        targets.Remove(target);
    }
}

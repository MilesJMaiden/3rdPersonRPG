using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float drag = 0.3f;

    private float verticalVelocity;
    private Vector3 dampingVelocity;
    private Vector3 impact;

    public Vector3 movement => impact + Vector3.up * verticalVelocity; //impact - knockback and external forces

    // Update is called once per frame
    void Update()
    {
        if(verticalVelocity < 0f && characterController.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        } 
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime; //Multiplying by deltatime makes it framerate independant
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);


        if (agent != null) //disable agent while force is applied
        {
            if(impact.sqrMagnitude < 0.2f * 0.2f)
            {
                impact = Vector3.zero;
                agent.enabled = true;
            }             
        }        
    }

    public void AddForce(Vector3 force)
    {
        impact += force;

        if(agent != null) //disable agent while force is applied
        {
            agent.enabled = false;
        }
    }

    public void Jump(float jumpForce)
    {
        verticalVelocity += jumpForce;
    }
}

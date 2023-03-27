using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private float drag = 0.1f;

    private float verticalVelocity;

    private Vector3 dampingVelocity;

    private Vector3 imapct;

    public Vector3 movement => imapct + Vector3.up * verticalVelocity; //impact - knockback and external forces

    // Start is called before the first frame update
    void Start()
    {
        
    }

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

        imapct = Vector3.SmoothDamp(imapct, Vector3.zero, ref dampingVelocity, drag);

        if(imapct == Vector3.zero )
        {
            if (agent != null) //disable agent while force is applied
            {
                agent.enabled = true;
            }        
        }
    }

    public void AddForce(Vector3 force)
    {
        imapct += force;
        if(agent!= null) //disable agent while force is applied
        {
            agent.enabled = false;
        }
    }
}

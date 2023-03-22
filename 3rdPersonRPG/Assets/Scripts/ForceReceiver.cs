using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;

    private float verticalVelocity;

    public Vector3 movement => Vector3.up * verticalVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(characterController != null)
        {
            if(verticalVelocity < 0f && characterController.isGrounded)
            {
                verticalVelocity = Physics.gravity.y * Time.deltaTime;
            } 
            else
            {
                verticalVelocity += Physics.gravity.y * Time.deltaTime; //Multiplying by deltatime makes it framerate independant
            }

            return;
        }
    }
}

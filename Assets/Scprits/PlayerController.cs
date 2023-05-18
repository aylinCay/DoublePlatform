using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using static UnityEngine.Vector3;
using Object = UnityEngine.Object;

public class PlayerController : MonoBehaviour
{


    private Rigidbody _rigidbody;
    
    private Vector3 _firstInput;

    private Vector3 _secondInput;

    private Vector3 result;
    
    private Vector3 direction;
    
    private Vector3 transformPosition;
    
    private Vector3 targetDir;

    private Vector3 _newPosition;

    private float _distance;

    public Vector3 velocityZ =>
        new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, forwardSpeed);
    
   
     public Vector3 refVector ;
    public Vector3 targetPos { get; set; } = Vector3.zero;


    public float forwardSpeed = 20f;

    public float jumpForce;
    
    public bool isGrounded;
    
    public bool isTargetSetted;

    public InputManager ınputManager;

    public Animator animator;
        
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

       
    void Update()
    {
        targetDir = ınputManager.ReadInput();
         if (Input.GetButtonUp("Fire1")){
             targetPos = isTargetSetted ? targetPos + targetDir:  transform.position  + targetDir;
             isTargetSetted = true;
         }

         if (isGrounded && targetDir == Vector3.up)
         {
             Jump();

             targetDir = Vector3.zero;
         }
         
         Move(targetPos);
    }
    
    public void Move( Vector3 target)
    {
        _rigidbody.velocity = velocityZ;
       
            if (isTargetSetted)
            {
                 transformPosition = transform.position;
                 _distance = Vector3.Distance(transform.position, target);
                if (_distance > .025f)
                {
                     _newPosition = Vector3.SmoothDamp(transform.position, target, ref refVector, .15f);
                    transformPosition.x = _newPosition.x;
                    transform.position = transformPosition;
                }
                else
                {
                    transform.position = target;
                    isTargetSetted = false;
                }
               
            }
            
    }

    public void Jump()
    {
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetBool("isJump",true);

    }
    
    private void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject != null)
        {
            isGrounded = true;
         animator.SetBool("isJump" , false);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        isGrounded = false;
        
    }
}


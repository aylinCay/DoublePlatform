using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
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

    public Vector3 velocityZ =>
        new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, forwardSpeed);

    public float forwardSpeed = 20f;

    public float jumpForce;
    public bool isGrounded;
        
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

       
    void Update()
    {
        targetDir = ReadInput();
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

    private Vector3 targetDir;


    Vector3 ReadInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _firstInput = Input.mousePosition;
        }

        if (Input.GetButton("Fire1"))
        {
            _secondInput = Input.mousePosition;
           
        }

        if (Input.GetButtonUp("Fire1"))
        {
            result = _firstInput - _secondInput;
        }

        var isY = Mathf.Abs(result.y) > Mathf.Abs(result.x);
        if (isY)
        {
            result = Vector3.zero;
            return Vector3.up;
        }
        
        return ( result.x > 0 ? -1 : 1) * Vector3.right;
        
    }

    public void Move( Vector3 target)
    {
        _rigidbody.velocity = velocityZ;
       
            if (isTargetSetted)
            {
                 transformPosition = transform.position;
                var distance = Vector3.Distance(transform.position, target);
                if (distance > .025f)
                {
                    var newPos = Vector3.SmoothDamp(transform.position, target, ref v, .15f);
                    transformPosition.x = newPos.x;
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
       
    }

    public bool isTargetSetted;
    public Vector3 v ;
    public Vector3 targetPos { get; set; } = Vector3.zero;


    private void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject != null)
        {
            isGrounded = true;
         
        }
    }

    private void OnCollisionExit(Collision other)
    {
        isGrounded = false;
        
    }
}


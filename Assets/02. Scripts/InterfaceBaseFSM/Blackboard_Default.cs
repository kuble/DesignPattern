using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//데이터를 중앙에서 관리하기 위한 Blackboard
public class Blackboard_Default : MonoBehaviour, IBlackboardBase
{
    public float JumpForce = 3f;
    public float moveSpeed = 3.0f;
   
    public Animator animator;
    public Rigidbody rigidbody;
    public InputAction moveInput;
    public InputAction jumpInput;

    public new void InitBlackboard()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        moveInput = GetComponent<PlayerInput>().actions["Move"];
        jumpInput = GetComponent<PlayerInput>().actions["Jump"];   
    }   
}

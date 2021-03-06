using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentGroundMovement : AgentMovementBase
{
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float jumpForce = 15f;
    [SerializeField] float onGroundDistance = .6f;
    [SerializeField] float jumpPointDistance = 1f;
    [SerializeField] float rotationSpeed = -3.5f;
    [SerializeField] float timeBetweenJumps = .1f;


    bool isJumping = false;
    public bool IsJumping 
    { 
        get => isJumping; 
        set
        {
            if (isJumping == value) return;
            isJumping = value;

            if (isJumping && NearJumpPoint())            
                JumpBehaviour();
        }
    }

    float timeSinceLastJumping = Mathf.Infinity;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
    }

    private void Update()
    {
        if (IsJumping && IsOnGround()) JumpBehaviour();

        if (!IsOnGround())
            rb.angularVelocity = rotationSpeed * Mathf.Rad2Deg;

        timeSinceLastJumping += Time.deltaTime;
    }

    public void JumpBehaviour()
    {
        if(isActiveAndEnabled && timeSinceLastJumping > timeBetweenJumps)
        {
            rb.velocity = Vector2.up * jumpForce;            
            timeSinceLastJumping = 0f;
        }        
    }

    bool IsOnGround()
    {
        var hit = Physics2D.Raycast(transform.position, Vector2.down, onGroundDistance, 1 << LayerMask.NameToLayer("Platform"));
        return hit.collider != null;
    }

    private bool NearJumpPoint()
    {
        var hit = Physics2D.CircleCast(transform.position, jumpPointDistance, Vector2.zero, 0, 1 << LayerMask.NameToLayer("Interactable"));        
        return hit.collider != null && hit.collider.GetComponent<JumpPoint>() != null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position, Vector2.down * onGroundDistance);
        Gizmos.DrawWireSphere(transform.position, jumpPointDistance);
    }
}

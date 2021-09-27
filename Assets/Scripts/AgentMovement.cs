using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float jumpForce = 15f;
    [SerializeField] float onGroundDistance = 1f;
    [SerializeField] float jumpPointDistance = 1f;
    [SerializeField] float rotationSpeed = 30f;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
    }

    public void Jump()
    {
        if(IsOnGround() || NearJumpPoint())
        {
            rb.velocity = Vector2.up * jumpForce;
            rb.AddTorque(rotationSpeed, ForceMode2D.Impulse);
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

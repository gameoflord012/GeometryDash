using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentGroundMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float jumpForce = 15f;
    [SerializeField] float onGroundDistance = .6f;
    [SerializeField] float jumpPointDistance = 1f;
    [SerializeField] float rotationSpeed = -5f;
    [SerializeField] float timeBetweenJumps = .3f;

    public bool IsJumping { get; set; }

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
        if (IsJumping && timeSinceLastJumping > timeBetweenJumps && IsOnGround())
        {
            rb.velocity = Vector2.up * jumpForce;
            rb.AddTorque(rotationSpeed, ForceMode2D.Impulse);
            timeSinceLastJumping = 0f;
        }

        timeSinceLastJumping += Time.deltaTime;
    }

    bool IsOnGround()
    {
        var hit = Physics2D.Raycast(transform.position, GetGroundRay(), onGroundDistance, 1 << LayerMask.NameToLayer("Platform"));
        return hit.collider != null;
    }

    Vector2 GetGroundRay()
    {
        Vector2 result = Quaternion.AngleAxis(-90, Vector3.forward) * rb.velocity.normalized;
        return result.normalized;
    }

    private bool NearJumpPoint()
    {
        var hit = Physics2D.CircleCast(transform.position, jumpPointDistance, Vector2.zero, 0, 1 << LayerMask.NameToLayer("Interactable"));        
        return hit.collider != null && hit.collider.GetComponent<JumpPoint>() != null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position, GetGroundRay() * onGroundDistance);
        Gizmos.DrawWireSphere(transform.position, jumpPointDistance);
    }
}

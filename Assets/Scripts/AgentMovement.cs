using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AgentMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float jumpForce = 15f;
    [SerializeField] float onGroundDistance = 1f;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
    }

    public void Jump()
    {
        if(IsOnGround())
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    bool IsOnGround()
    {
        var hit = Physics2D.Raycast(transform.position, Vector2.down, onGroundDistance, 1 << LayerMask.NameToLayer("Platform"));
        return hit.collider != null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position, Vector2.down * onGroundDistance);
    }
}

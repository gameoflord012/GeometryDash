using System.Collections;
using UnityEngine;

public class AgentFlyingMovement : MonoBehaviour
{
    [SerializeField] float flyingSpeed = 10f;
    [SerializeField] float flyUpForce = 15f;

    public bool IsFlyingUp { get; set; }

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(flyingSpeed, IsFlyingUp ? flyUpForce: rb.velocity.y);
        rb.transform.right = rb.velocity.normalized;
    }
}
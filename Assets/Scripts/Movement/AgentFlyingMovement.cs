﻿using System.Collections;
using UnityEngine;

public class AgentFlyingMovement : MonoBehaviour
{
    [SerializeField] float flyingSpeed = 10f;
    [SerializeField] float flyUpForce = 15f;
    [SerializeField] 

    public bool IsFlyingUp { get; set; }

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(IsFlyingUp)
            rb.AddForce(flyUpForce * Vector2.up * Time.fixedDeltaTime, ForceMode2D.Impulse);

        rb.velocity = new Vector2(flyingSpeed, rb.velocity.y);

        rb.transform.right = rb.velocity.normalized;
    }
}
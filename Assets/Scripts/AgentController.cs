using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AgentController : MonoBehaviour
{
    [field: SerializeField] public AgentState CurrentState { get; set; } = AgentState.Ground;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            DeadBehaviour();
    }

    private void DeadBehaviour()
    {
        Destroy(gameObject);
    }
}

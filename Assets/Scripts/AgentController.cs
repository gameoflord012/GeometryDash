using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AgentController : MonoBehaviour
{
    [SerializeField] GameObject GroundStateModel;
    [SerializeField] GameObject FlyingStateModel;
    [SerializeField] AgentState currentState;

    public AgentState CurrentState
    {
        get => currentState;
        set
        {
            currentState = value;

            if(currentStateModel != null) currentStateModel.SetActive(false);

            switch (currentState)
            {
                case AgentState.Ground:
                    currentStateModel = GroundStateModel;
                    break;
                case AgentState.Fly:
                    currentStateModel = FlyingStateModel;
                    break;
            }

            currentStateModel.SetActive(true);
        }
    }

    GameObject currentStateModel;
    Rigidbody2D rb;

    private void Awake()
    {
        CurrentState = currentState;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            DeadBehaviour();
    }

    private void DeadBehaviour()
    {
        Destroy(gameObject);
    }

    public void FreezeMovement()
    {
        GetComponentInChildren<AgentMovementBase>().enabled = false;
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
}

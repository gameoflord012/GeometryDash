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

    private void Awake()
    {
        CurrentState = currentState;
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
}

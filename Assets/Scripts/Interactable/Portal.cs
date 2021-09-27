using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] AgentState stateToChange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.attachedRigidbody.GetComponent<AgentController>().CurrentState = stateToChange;
    }
}

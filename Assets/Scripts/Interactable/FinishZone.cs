using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        var controller = collider2D.GetComponentInParent<AgentController>();
        if (controller != null) controller.DisableMovement();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentInputEvents : MonoBehaviour
{
    [SerializeField] KeyCode jumpKey;

    public UnityEvent OnJumpKeyPressed;

    private void Update()
    {
        if (Input.GetKeyDown(jumpKey))
            OnJumpKeyPressed?.Invoke();
    }
}

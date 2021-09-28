using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentInputEvents : MonoBehaviour
{
    [SerializeField] KeyCode jumpKey;
    [SerializeField] KeyCode flyingKey;

    public UnityEvent OnJumpKeyPressed;
    public UnityEvent OnJumpKeyReleased;

    public UnityEvent OnFlyingKeyPressed;
    public UnityEvent OnFlyingKeyReleased;

    private void Update()
    {
        if (Input.GetKeyDown(jumpKey))
            OnJumpKeyPressed?.Invoke();

        if (Input.GetKeyUp(jumpKey))
            OnJumpKeyReleased?.Invoke();

        if (Input.GetKeyDown(flyingKey))
            OnFlyingKeyPressed?.Invoke();

        if (Input.GetKeyUp(flyingKey))
            OnFlyingKeyReleased?.Invoke();
    }
}

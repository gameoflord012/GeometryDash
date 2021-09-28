using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishZone : MonoBehaviour
{
    [SerializeField] Transform finishPoint;
    [SerializeField] float finishDuration = 2f;
    [SerializeField] AnimationCurve curve;
    [SerializeField] float curveOffset;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        var controller = collider2D.GetComponentInParent<AgentController>();
        if (controller != null) controller.FreezeMovement();

        StopAllCoroutines();
        StartCoroutine(FinishRoutine(controller.transform));
    }

    private IEnumerator FinishRoutine(Transform agentTransform)
    {
        var startPosition = agentTransform.position;
        var startRotation = agentTransform.rotation;

        float timeElapsed = 0f;
        while(timeElapsed < finishDuration)
        {
            agentTransform.rotation = Quaternion.Lerp(startRotation, finishPoint.rotation, timeElapsed / finishDuration);

            agentTransform.position = 
                Vector3.Lerp(startPosition, finishPoint.position, timeElapsed / finishDuration) + 
                Vector3.up * curve.Evaluate(timeElapsed / finishDuration) * curveOffset;

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        agentTransform.rotation = finishPoint.rotation;
        agentTransform.position = finishPoint.position;     
        
        Destroy(agentTransform.gameObject);
    }
}

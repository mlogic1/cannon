using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraState
{
    ZoomedOut = 0,
    Action
};

public class CameraController : MonoBehaviour
{
    public Vector3 ZoomedOutCameraPosition;
    public float ActionStateZOrder = -5.0f;
    public float ActionStateLerpVelocity = 10.0f;

    private CameraState CurrentCameraState = CameraState.ZoomedOut;
    private GameObject TrackedgGameObject = null;

    public void SetCameraState(CameraState newState, GameObject trackedObject = null)
    {
        CurrentCameraState = newState;
        if (trackedObject != null)
        {
            TrackedgGameObject = trackedObject;
        }
    }

    void Start()
    {

    }

    void FixedUpdate()
    {
        Vector3 targetPos;
        switch(CurrentCameraState)
        {
            case CameraState.ZoomedOut:
                targetPos = ZoomedOutCameraPosition;
                break;

            case CameraState.Action:
                if (TrackedgGameObject == null)
                {
                    Debug.Log("Camera lost tracking of object, returning to ZoomedOut State");
                    SetCameraState(CameraState.ZoomedOut, null);
                    return;
                }

                targetPos = TrackedgGameObject.transform.position;
                targetPos.z = ActionStateZOrder;
                break;

            default:
                targetPos = new Vector3(0.0f, 0.0f);
                break;
        }

        Vector3 currentPos = gameObject.transform.position;
        Vector3 lerp = Vector3.Lerp(currentPos, targetPos, Time.deltaTime * ActionStateLerpVelocity);
        //gameObject.transform.Translate(lerp); // bugs out for some reason
        gameObject.transform.position = new Vector3(lerp.x, lerp.y, lerp.z);
    }
}

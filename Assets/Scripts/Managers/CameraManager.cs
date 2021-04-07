using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target;

    public Vector3 cameraLocalPosition;
    public Vector3 localTargetLookAtPosition;

    public float positionLerpSpeed = 0.01f;
    public float lookLerpSpeed = 0.01f;

    private Vector3 wantedPosition;
    private Quaternion wantedRotation;

    private void Update()
    {
        wantedPosition = target.TransformPoint(cameraLocalPosition);
        wantedPosition.y = cameraLocalPosition.y + target.position.y;

        transform.position = Vector3.Lerp(transform.position, wantedPosition, positionLerpSpeed);

        wantedRotation = Quaternion.LookRotation(target.TransformPoint(localTargetLookAtPosition) - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, lookLerpSpeed);
    }
}

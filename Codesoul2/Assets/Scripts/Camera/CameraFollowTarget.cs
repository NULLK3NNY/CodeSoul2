using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollowTarget : MonoBehaviour
{
    Transform targetPos;
    [SerializeField] Vector3 cameraOffset = new Vector3(0, 0, -10);
    [SerializeField] float time;

    private void Awake()
    {
        targetPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        CameraFollow(targetPos);
    }

    void CameraFollow(Transform target)
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, target.transform.position + cameraOffset, time);
    }
}

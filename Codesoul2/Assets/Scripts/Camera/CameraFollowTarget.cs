using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField] Transform targetPos;
    [SerializeField] Vector3 cameraOffset = new Vector3(0, 0, -10);
    [SerializeField] float time;
    [SerializeField] Player player;

    private void Update()
    {
        CameraFollow(targetPos);
        //CameraFollowV(player.mousePosition);
    }

    void CameraFollow(Transform target)
    {
        //Camera.main.transform.position = target.position + camOffset + cameraOffset;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, target.transform.position + cameraOffset, time);
    }

    void CameraFollowV(Vector3 vector)
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, vector + cameraOffset, time);
    }
}

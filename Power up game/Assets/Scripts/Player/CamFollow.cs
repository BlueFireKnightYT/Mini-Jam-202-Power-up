using UnityEngine;
using UnityEngine.InputSystem;

public class CamFollow : MonoBehaviour
{
    public Transform target;
    public float pixelsPerUnit = 16f;

    public Vector2 idealPosition { get; private set; }
    public Vector2 snapError { get; private set; }

    void LateUpdate()
    {
        idealPosition = target.position;

        float x = idealPosition.x * pixelsPerUnit;
        float y = idealPosition.y * pixelsPerUnit;

        float sx = Mathf.Round(x);
        float sy = Mathf.Round(y);

        Vector2 snapped = new Vector2(sx, sy) / pixelsPerUnit;

        snapError = idealPosition - snapped;

        transform.position = new Vector3(snapped.x, snapped.y, transform.position.z);
    }
}

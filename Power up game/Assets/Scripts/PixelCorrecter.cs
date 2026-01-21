using UnityEngine;

public class PixelCorrecter : MonoBehaviour
{
        public CamFollow source;
        public float pixelsPerUnit = 16f;

        void LateUpdate()
        {
            Vector2 offset = source.snapError;

            transform.position = new Vector3(
                -offset.x,
                -offset.y,
                transform.position.z
            );
        }
}

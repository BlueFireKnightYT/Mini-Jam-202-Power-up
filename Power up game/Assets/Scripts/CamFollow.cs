using UnityEngine;
using UnityEngine.InputSystem;

public class CamFollow : MonoBehaviour
{
    public Transform player;
    public PlayerInput pi;
    public float displaceModifier;
    InputAction lookAt;
    private float zAxis = -10f;

    private void Start()
    {
        lookAt = pi.actions.FindAction("LookAt");
    }
    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(lookAt.ReadValue<Vector2>());
        Vector3 mouseDisplace = (mousePos - new Vector2(player.transform.position.x, player.transform.position.y)) * displaceModifier;

        Vector3 finalCamPos = player.transform.position + mouseDisplace;
        finalCamPos.z = zAxis;
        transform.position = finalCamPos;
    }
}

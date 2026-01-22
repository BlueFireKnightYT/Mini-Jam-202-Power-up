using UnityEngine;

public class Delete : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 0.1f);
    }
}

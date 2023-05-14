using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform[] playerTransforms;

    private void Update()
    {
        transform.position = new Vector3(
            playerTransforms.Sum(t => t.position.x) / playerTransforms.Length,
            playerTransforms.Sum(t => t.position.y) / playerTransforms.Length,
            transform.position.z
        );
    }
}

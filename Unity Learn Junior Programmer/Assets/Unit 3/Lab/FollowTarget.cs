using UnityEngine;

namespace Lab3
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] private Vector3 offset;
        [SerializeField] private Transform target;

        private void LateUpdate()
        {
            transform.position = target.position + offset;
        }
    }
}

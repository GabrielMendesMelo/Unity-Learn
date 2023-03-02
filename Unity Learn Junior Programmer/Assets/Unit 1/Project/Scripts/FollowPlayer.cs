using UnityEngine;

namespace Prototype1
{
    public class FollowPlayer : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private Vector3 offset;
        [SerializeField] private bool primeiraPessoa;

        void LateUpdate()
        {
            transform.position = player.transform.position + offset;
        }

        private void Update()
        {
            if (primeiraPessoa)
            {
                transform.rotation = player.transform.rotation;
            }
        }
    }
}

using UnityEngine;

namespace Challenge2
{
    public class DetectCollisionsX : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            Destroy(gameObject);
        }
    }
}

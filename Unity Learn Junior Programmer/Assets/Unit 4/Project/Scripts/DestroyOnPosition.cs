using UnityEngine;

namespace Prototype4
{
    public class DestroyOnPosition : MonoBehaviour
    {
        [SerializeField] private Vector3 maxPosition = new Vector3(30, 30, 30);

        void Update()
        {
            float x = transform.position.x;
            float y = transform.position.y;
            float z = transform.position.z;
            float maxX = maxPosition.x;
            float maxY = maxPosition.y;
            float maxZ = maxPosition.z;

            if (x > maxX || y > maxY || z > maxZ || x < -maxX || y < -maxY || z < -maxZ)
            {
                Destroy(gameObject);
            }
        }
    }
}

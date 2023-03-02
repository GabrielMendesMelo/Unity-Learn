using UnityEngine;

namespace Prototype4
{
    public class SnapOtherPosition : MonoBehaviour
    {
        [SerializeField] private GameObject other;

        void Update()
        {
            transform.position = other.transform.position;
        }
    }
}

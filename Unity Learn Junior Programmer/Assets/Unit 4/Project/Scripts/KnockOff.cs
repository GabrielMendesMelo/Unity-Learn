using System.Collections;
using UnityEngine;

namespace Prototype4
{
    public class KnockOff : MonoBehaviour
    {
        [SerializeField] private float knockOffStrength;
        [SerializeField] private ParticleSystem knockOffParticleEffect;
        [SerializeField] private string[] tags = new string[] { "Inimigo", "InimigoHarder" };
        [SerializeField] private float destroyAfter = 1;

        private void OnTriggerEnter(Collider collider)
        {
            GameObject other = collider.gameObject;
            foreach (string tag in tags)
            {
                if (other.CompareTag(tag))
                {
                    Rigidbody otherRb = other.GetComponent<Rigidbody>();
                    if (otherRb != null)
                    {
                        Vector3 away = transform.position - other.transform.position;
                        otherRb.AddForce(away.normalized * knockOffStrength, ForceMode.Impulse);
                        knockOffParticleEffect?.Play();
                        StartCoroutine("DestroyAfterX");
                    }
                }
            }
        }

        private IEnumerator DestroyAfterX()
        {
            yield return new WaitForSeconds(destroyAfter);
            Destroy(gameObject);
        }
    }
}

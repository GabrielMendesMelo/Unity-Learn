using System.Collections;
using UnityEngine;

namespace Prototype4
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float velocidade;
        [SerializeField] private float velocidadeShiftMult;
        [SerializeField] private GameObject focalPoint;
        [SerializeField] private float powerUpStrengh = 15f;
        [SerializeField] private float powerUpDuration = 7f;
        [SerializeField] private GameObject powerUpIndicator;

        private Rigidbody rb;
        private bool hasPowerUp = false;

        private float VelocidadeComShift { get { return Input.GetKey(KeyCode.LeftShift) ? velocidadeShiftMult : 1; } }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            float verticalInput = Input.GetAxis("Vertical");
            rb.AddForce(focalPoint.transform.forward * verticalInput * velocidade * Time.deltaTime * VelocidadeComShift);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Inimigo") && hasPowerUp)
            {
                Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

                enemyRigidbody.AddForce(awayFromPlayer * powerUpStrengh, ForceMode.Impulse);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Powerup"))
            {
                hasPowerUp = true;
                Destroy(other.gameObject);
                StartCoroutine("PowerUpCountdownRoutine");
                powerUpIndicator.SetActive(true);
            }
        }

        private IEnumerator PowerUpCountdownRoutine()
        {
            yield return new WaitForSeconds(powerUpDuration);
            hasPowerUp = false;
            powerUpIndicator.SetActive(false);
        }
    }
}

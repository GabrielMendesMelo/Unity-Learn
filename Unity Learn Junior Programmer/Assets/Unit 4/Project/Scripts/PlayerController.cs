using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private GameObject rocket;
        [SerializeField] private float rocketLaunchForce;
        [SerializeField] private GameObject powerUpSmashIndicator;
        [SerializeField] private float smashUpForce = 2f;
        [SerializeField] private float smashDownForce = 2f;
        [SerializeField] private float smashMaxY = 5f;
        [SerializeField] private float smashForceMult = 15f;
        [SerializeField] private ParticleSystem explosionSmashParticle;
        [SerializeField] private float distanciaMaxima;

        private Rigidbody rb;
        private bool hasPowerUp = false;
        private bool hasPowerUpSmash = false;
        private bool estaNoChao = true;

        private float VelocidadeComShift { get { return Input.GetKey(KeyCode.LeftShift) ? velocidadeShiftMult : 1; } }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            float verticalInput = Input.GetAxis("Vertical");
            rb.AddForce(focalPoint.transform.forward * verticalInput * velocidade * Time.deltaTime * VelocidadeComShift);

            if (hasPowerUpSmash)
            {
                SmashPowerUp();
            }
        }

        private void SmashPowerUp()
        {
            if (estaNoChao)
            {
                rb.AddForce(Vector3.up * smashUpForce, ForceMode.Impulse);
                estaNoChao = false;
            }
            else if (transform.position.y >= smashMaxY && !estaNoChao)
            {
                rb.AddForce(Vector3.down * smashDownForce, ForceMode.Impulse);
                estaNoChao = true;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Inimigo") && hasPowerUp)
            {
                Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

                enemyRigidbody.AddForce(awayFromPlayer * powerUpStrengh, ForceMode.Impulse);
            }
            if (collision.gameObject.CompareTag("InimigoHarder"))
            {
                Vector3 awayFromEnemy = transform.position - collision.gameObject.transform.position;
                rb.AddForce(awayFromEnemy * collision.gameObject.GetComponent<Rigidbody>().mass, ForceMode.Impulse);
            }
            if (collision.gameObject.CompareTag("Chao"))
            {
                estaNoChao = true;
                if (hasPowerUpSmash)
                {
                    explosionSmashParticle.Play();
                    EmpurrarInimigosBaseadoNaDistancia();
                    hasPowerUpSmash = false;
                }
            }
        }

        private void EmpurrarInimigosBaseadoNaDistancia()
        {
            foreach (GameObject enemy in Enemies)
            {
                float distancia = Vector3.Distance(transform.position, enemy.transform.position);
                if (distancia < distanciaMaxima)
                {
                    Vector3 away = transform.position - enemy.transform.position;
                    float fatorAtenuacao = Mathf.Exp(-distancia / distanciaMaxima);
                    Vector3 forca = -away.normalized * fatorAtenuacao * smashForceMult;
                    Debug.Log(enemy.name + ": " + forca);
                    enemy.GetComponent<Rigidbody>().AddForce(forca, ForceMode.Impulse);
                }
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
            if (other.CompareTag("RocketPowerup"))
            {
                foreach (GameObject enemy in Enemies)
                {
                    Vector3 direction = enemy.transform.position - transform.position;
                    Quaternion rotation = Quaternion.LookRotation(direction);
                    GameObject rocketInstance = Instantiate(rocket, transform.position, rotation);
                    rocketInstance.GetComponent<Rigidbody>().AddForce(direction * rocketLaunchForce, ForceMode.Impulse);
                }
                Destroy(other.gameObject);
            }
            if (other.CompareTag("SmashPowerUp"))
            {
                hasPowerUpSmash = true;
                Destroy(other.gameObject);
            }
        }

        private IEnumerator PowerUpCountdownRoutine()
        {
            yield return new WaitForSeconds(powerUpDuration);
            hasPowerUp = false;
            powerUpIndicator.SetActive(false);
        }

        private GameObject[] Enemies
        {
            get
            {
                List<GameObject> enemies = new List<GameObject>();
                GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Inimigo");
                GameObject[] inimigosHarder = GameObject.FindGameObjectsWithTag("InimigoHarder");

                foreach (GameObject o in inimigos)
                {
                    enemies.Add(o.gameObject);
                }
                foreach (GameObject o in inimigosHarder)
                {
                    enemies.Add(o.gameObject);
                }
                return enemies.ToArray();
            }
        }
    }
}

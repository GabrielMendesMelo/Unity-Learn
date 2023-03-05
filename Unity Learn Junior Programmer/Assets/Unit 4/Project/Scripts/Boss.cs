using System.Collections;
using UnityEngine;

namespace Prototype4
{
    public class Boss : MonoBehaviour
    {
        [SerializeField] private float velocidadeNormal = 1000f;
        [SerializeField] private float desaceleracaoMult = 5f;
        [SerializeField] private float foraDaTelaPosY = -30;
        private Rigidbody rb;
        private GameObject player;

        [SerializeField] private GameObject[] minions;
        [SerializeField] private float minionOffset = 4;
        [SerializeField] private int maxMinions = 5;

        [SerializeField] private GameObject rocket;
        [SerializeField] private float rocketLaunchForce = 5f;

        [SerializeField] private float jumpForce;
        [SerializeField] private ParticleSystem jumpParticle;
        [SerializeField] private float distanciaMaxima = 5f;
        [SerializeField] private float onImpactForce = 10f;

        [SerializeField] private float minPoderTimer = 10f;
        [SerializeField] private float maxPoderTimer = 20f;
        private float poderCooldown;

        private bool estaNoChao = false;
        private float distanciaProJogadorAnterior;

        private float velocidade;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            player = FindObjectOfType<PlayerController>().gameObject;

            poderCooldown = Random.Range(minPoderTimer, maxPoderTimer);
            StartCoroutine("UsarPoderRoutine");

            distanciaProJogadorAnterior = Vector3.Distance(player.transform.position, transform.position);
            velocidade = velocidadeNormal;
        }

        private void Update()
        {
            float distanciaProJogadorAtual = Vector3.Distance(player.transform.position, transform.position);
            Vector3 lookDirection = player.transform.position - transform.position;
            lookDirection.Normalize();

            if (distanciaProJogadorAtual > distanciaProJogadorAnterior)
            {
                velocidade = velocidadeNormal * desaceleracaoMult;
            }
            else
            {
                velocidade = velocidadeNormal;
            }
            rb.AddForce(lookDirection * velocidade * Time.deltaTime);

            if (transform.position.y < foraDaTelaPosY)
            {
                Morrer();
            }
        }

        private IEnumerator UsarPoderRoutine()
        {
            yield return new WaitForSeconds(poderCooldown);

            poderCooldown = Random.Range(minPoderTimer, maxPoderTimer);
            UsarPoder();
            StartCoroutine("UsarPoderRoutine");
        }

        private void UsarPoder()
        {
            int poder = Random.Range(0, 3);
            switch (poder)
            {
                case 0:
                    SpawnMinions();
                    break;

                case 1:
                    ShootProjectiles();
                    break;


                case 2:
                    Jump();
                    break;
            }
        }

        private void SpawnMinions()
        {
            int maxMinionsToInstantiate = maxMinions - FindObjectsOfType<Enemy>().Length;
            if (maxMinionsToInstantiate < 0) maxMinionsToInstantiate = 0;
            for (int i = 0; i < maxMinionsToInstantiate; i++)
            {
                GameObject minion = minions[Random.Range(0, minions.Length)];
                float x = transform.position.x + Random.Range(-minionOffset, minionOffset);
                float z = transform.position.z + Random.Range(-minionOffset, minionOffset);
                while (x < 2.5f && x > -2.5f && z < 2.5f && z > -2.5f)
                {
                    x = transform.position.x + Random.Range(-minionOffset, minionOffset);
                    z = transform.position.z + Random.Range(-minionOffset, minionOffset);
                }
                Vector3 minionPos = new Vector3(x, 0, z);
                Instantiate(minion, minionPos, Quaternion.identity);
            }
        }

        private void ShootProjectiles()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector3 direction = player.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            GameObject rocketInstance = Instantiate(rocket, transform.position, rotation);
            rocketInstance.GetComponent<Rigidbody>().AddForce(direction * rocketLaunchForce, ForceMode.Impulse);
        }

        private void Jump()
        {
            if (estaNoChao)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                estaNoChao = false;
            }
        }

        private void Morrer()
        {
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Chao"))
            {
                if (!estaNoChao)
                {
                    estaNoChao = true;
                    jumpParticle.Play();

                    float distancia = Vector3.Distance(transform.position, player.transform.position);
                    if (distancia < distanciaMaxima)
                    {
                        Vector3 away = transform.position - player.transform.position;
                        float fatorAtenuacao = Mathf.Exp(-distancia / distanciaMaxima);
                        Vector3 forca = -away.normalized * fatorAtenuacao * onImpactForce;
                        player.GetComponent<Rigidbody>().AddForce(forca, ForceMode.Impulse);
                    }
                }
            }
        }
    }
}

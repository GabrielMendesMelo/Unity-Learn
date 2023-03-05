using UnityEngine;

namespace Prototype4
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float velocidade;
        [SerializeField] private float foraDaTelaPosY = -30;
        private Rigidbody rb;
        private GameObject player;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            player = FindObjectOfType<PlayerController>().gameObject;
        }

        private void Update()
        {
            Vector3 lookDirection = player.transform.position - transform.position;
            lookDirection.Normalize();
            rb.AddForce(lookDirection * velocidade * Time.deltaTime);

            if (transform.position.y < foraDaTelaPosY)
            {
                Morrer();
            }
        }

        private void Morrer()
        {
            SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
            if (FindObjectsOfType<Enemy>().Length <= 1)
            {
                if (!spawnManager.SpawnouBoss)
                {

                    spawnManager.SpawnEnemyWave();
                }
            }
            Destroy(gameObject);
        }
    }
}

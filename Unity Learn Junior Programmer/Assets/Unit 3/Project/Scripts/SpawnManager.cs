using UnityEngine;

namespace Prototype3
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] obstaculos;
        [SerializeField] private float spawnPosX = 22;
        [SerializeField] private float startDelay = 1f;
        [SerializeField] private float minSpawnTimer = 1f;
        [SerializeField] private float maxSpawnTimer = 2f;

        void Start()
        {
            Invoke("Spawn", startDelay);
        }

        private void Spawn()
        {
            GameObject obstaculo = obstaculos[Random.Range(0, obstaculos.Length)];
            Vector3 pos = new Vector3(spawnPosX, obstaculo.transform.position.y, obstaculo.transform.position.z);
            Instantiate(obstaculo, pos, obstaculo.transform.rotation);

            if (!FindObjectOfType<PlayerController>().EstaGameOver)
            {
                float spawnTimer = Random.Range(minSpawnTimer, maxSpawnTimer);
                Invoke("Spawn", spawnTimer);
            }
        }
    }
}

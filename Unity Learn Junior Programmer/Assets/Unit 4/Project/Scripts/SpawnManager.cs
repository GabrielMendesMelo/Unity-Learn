using UnityEngine;

namespace Prototype4
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private Vector3 spawnPosition;

        [SerializeField] private GameObject[] enemies;
        [SerializeField] private int maxInimigos = 9;

        [SerializeField] private GameObject[] powerUps;

        private int numInimigosParaSpawn = 1;

        void Start()
        {
            SpawnEnemyWave();
        }

        public void SpawnEnemyWave()
        {
            for (int i = 0; i < numInimigosParaSpawn; i++)
            {
                CriarInimigo();
            }
            CriarPowerUp();
            if (numInimigosParaSpawn <= maxInimigos)
            {
                numInimigosParaSpawn++;
            }
        }

        private void CriarInimigo()
        {
            GameObject enemy = RandomArrayItem(enemies);
            Vector3 pos = GenerateSpawnPosition();
            Instantiate(enemy, pos, enemy.transform.rotation);
        }

        private void CriarPowerUp()
        {
            GameObject powerup = RandomArrayItem(powerUps);
            Vector3 pos = GenerateSpawnPosition();
            Instantiate(powerup, pos, powerup.transform.rotation);
        }

        private T RandomArrayItem<T>(T[] array)
        {
            return array[Random.Range(0, array.Length)];
        }

        private Vector3 GenerateSpawnPosition()
        {
            float posX = Random.Range(-spawnPosition.x, spawnPosition.x);
            float posZ = Random.Range(-spawnPosition.z, spawnPosition.z);
            return new Vector3(posX, 0, posZ);
        }
    }
}

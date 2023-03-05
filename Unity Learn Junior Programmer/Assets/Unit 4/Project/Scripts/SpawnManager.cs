using System.Collections;
using UnityEngine;

namespace Prototype4
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private Vector3 spawnPosition;

        [SerializeField] private GameObject[] enemies;
        [SerializeField] private int maxInimigos = 10;

        [SerializeField] private GameObject[] powerUps;

        [SerializeField] private GameObject boss;
        [SerializeField] private Vector3 bossPosicao;

        private int numInimigosParaSpawn = 8;

        public bool SpawnouBoss { get; private set; }

        void Start()
        {
            SpawnEnemyWave();
        }

        public void SpawnEnemyWave()
        {
            if (numInimigosParaSpawn < maxInimigos)
            {
                numInimigosParaSpawn++;

                for (int i = 0; i < numInimigosParaSpawn; i++)
                {
                    CriarInimigo();
                }
                CriarPowerUp();
            }
            else
            {
                CriarBoss();
            }


        }

        private void CriarInimigo()
        {
            GameObject enemy = RandomArrayItem(enemies);
            Vector3 pos = GenerateSpawnPosition();
            Instantiate(enemy, pos, enemy.transform.rotation);
        }

        private void CriarBoss()
        {
            Instantiate(boss, bossPosicao, boss.transform.rotation);
            SpawnouBoss = true;
            CriarPowerUpPorTempo();
        }

        private void CriarPowerUp()
        {
            GameObject powerup = RandomArrayItem(powerUps);
            Vector3 pos = GenerateSpawnPosition();
            Instantiate(powerup, pos, powerup.transform.rotation);
        }

        private void CriarPowerUpPorTempo()
        {
            InvokeRepeating("CriarPowerUp", 10, 10);
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

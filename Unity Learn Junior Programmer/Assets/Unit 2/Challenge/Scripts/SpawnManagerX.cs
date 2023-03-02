using UnityEngine;

namespace Challenge2
{
    public class SpawnManagerX : MonoBehaviour
    {
        public GameObject[] ballPrefabs;

        private float spawnLimitXLeft = -22;
        private float spawnLimitXRight = 7;
        private float spawnPosY = 30;

        private float startDelay = 1.0f;
        private float spawnIntervalMin = 3.0f;
        private float spawnIntervalMax = 5.0f;
        private float spawnInterval;

        // Start is called before the first frame update
        void Start()
        {
            Invoke("SpawnRandomBall", startDelay);
        }

        // Spawn random ball at random x position at top of play area
        void SpawnRandomBall()
        {
            // Generate random ball index and random spawn position
            Vector3 spawnPos = new Vector3(Random.Range(spawnLimitXLeft, spawnLimitXRight), spawnPosY, 0);

            // instantiate ball at random spawn location
            GameObject ball = ballPrefabs[Random.Range(0, ballPrefabs.Length)];
            Instantiate(ball, spawnPos, ball.transform.rotation);

            spawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax);
            Invoke("SpawnRandomBall", spawnInterval);
        }

    }
}

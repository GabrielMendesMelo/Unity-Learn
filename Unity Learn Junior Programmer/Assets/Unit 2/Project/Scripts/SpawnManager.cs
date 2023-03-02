using UnityEngine;

namespace Prototype2
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] animais;
        [SerializeField] private float maxX = 16;
        [SerializeField] private float posXLados = 20;
        [SerializeField] private float minZ = -2;
        [SerializeField] private float maxZ = 7;
        [SerializeField] private float posZCima = 20;
        [SerializeField] private float startDelay = 2f;
        [SerializeField] private float spawnIntervalMin = 1f;
        [SerializeField] private float spawnIntervalMax = 3f;
        private float spawnInterval;

        private struct PosicaoRotacao
        {
            public Vector3 pos;
            public Quaternion rot;
        }

        void Start()
        {
            Invoke("CriarAnimal", startDelay);
        }

        private void CriarAnimal()
        {
            GameObject animal = animais[Random.Range(0, animais.Length)];
            PosicaoRotacao posRot = PosicaoRotacaoCriacaoDeAnimal();
            Instantiate(animal, posRot.pos, posRot.rot);

            spawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax);
            Invoke("CriarAnimal", spawnInterval);
        }

        private PosicaoRotacao PosicaoRotacaoCriacaoDeAnimal()
        {
            float posX = Random.Range(-maxX, maxX);
            float posZ = Random.Range(minZ, maxZ);
            float rotY = 0;

            int posicaoInicialBase = Random.Range(0, 3);
            switch (posicaoInicialBase)
            {
                // Da esquerda
                case 0:
                    posX = -posXLados;
                    rotY = 90;
                    break;

                // De cima
                case 1:
                    posZ = posZCima;
                    rotY = 180;
                    break;

                // Da direita
                case 2:
                    posX = posXLados;
                    rotY = 270;
                    break;
            }

            return new PosicaoRotacao()
            {
                pos = new Vector3(posX, transform.position.y, posZ),
                rot = Quaternion.Euler(transform.rotation.x, rotY, transform.rotation.z),
            };
        }
    }
}

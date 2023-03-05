using UnityEngine;

namespace Prototype5
{
    public class Target : MonoBehaviour
    {
        [SerializeField] private float forceMin = 12;
        [SerializeField] private float forceMax = 16;
        [SerializeField] private float torqueMult = 10;
        [SerializeField] private float posX;
        [SerializeField] private float posY;
        [SerializeField] private int pontos;
        [SerializeField] private ParticleSystem explosionParticle;

        private GameManager gameManager;

        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * Random.Range(forceMin, forceMax), ForceMode.Impulse);
            rb.AddTorque(Vector3.one * Random.Range(-torqueMult, torqueMult), ForceMode.Impulse);
            transform.position = new Vector2(Random.Range(-posX, posX), posY);

            gameManager = FindObjectOfType<GameManager>();
        }

        public void DestroyTarget()
        {
            if (!gameManager.IsGameOver)
            {
                Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
                gameManager.UpdateScore(pontos);
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!gameObject.CompareTag("Bad") && !gameManager.IsGameOver)
            {
                gameManager.UpdateLives(1);
            }
            Destroy(gameObject);
        }
    }
}

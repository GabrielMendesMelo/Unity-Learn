using UnityEngine;

namespace Prototype3
{
    public class RepeatBackground : MonoBehaviour
    {
        [SerializeField] private float velocidade = 10;
        private Vector3 posInicial;
        private float offsetX;
        private PlayerController playerController;

        private void Awake()
        {
            FaseInfo.Velocidade = velocidade;
        }

        void Start()
        {
            playerController = FindObjectOfType<PlayerController>();
            posInicial = transform.position;
            offsetX = GetComponent<BoxCollider>().size.x / 2;
        }

        void Update()
        {
            if (!playerController.EstaGameOver && FaseInfo.Comecou)
            {
                transform.Translate(Vector3.left * FaseInfo.Velocidade * Time.deltaTime);
                if (transform.position.x < posInicial.x - offsetX)
                {
                    transform.position = posInicial;
                }
            }
        }
    }
}

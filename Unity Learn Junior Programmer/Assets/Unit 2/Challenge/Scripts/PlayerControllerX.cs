using UnityEngine;

namespace Challenge2
{
    public class PlayerControllerX : MonoBehaviour
    {
        public GameObject dogPrefab;
        [SerializeField] private float maxIntervaloParaInstanciarCachorro = 1;
        private float intervaloParaInstanciarCachorro = 0;

        // Update is called once per frame
        void Update()
        {
            intervaloParaInstanciarCachorro += Time.deltaTime;

            // On spacebar press, send dog
            if (Input.GetKeyDown(KeyCode.Space) && intervaloParaInstanciarCachorro >= maxIntervaloParaInstanciarCachorro)
            {
                intervaloParaInstanciarCachorro = 0;
                Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            }
        }
    }
}

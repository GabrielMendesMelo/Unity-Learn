using UnityEngine;

namespace Lab4
{
    public class CriaObjetos : MonoBehaviour
    {
        [SerializeField] private GameObject[] objetos;
        [SerializeField] private Vector3 maxPosicao;
        [SerializeField] private Vector3 minPosicao;

        void Start()
        {
            foreach (GameObject objeto in objetos)
            {
                float x = Random.Range(minPosicao.x, maxPosicao.x);
                float y = Random.Range(minPosicao.y, maxPosicao.y);
                float z = Random.Range(minPosicao.z, maxPosicao.z);
                Vector3 posicao = new Vector3(x, y, z);
                Instantiate(objeto, posicao, objeto.transform.rotation);
            }
        }
    }
}

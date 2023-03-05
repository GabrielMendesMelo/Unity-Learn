using System;
using UnityEngine;
using UnityEngine.AI;

namespace Lab5
{
    public class JogadorControle : MonoBehaviour
    {
        [SerializeField] private LayerMask projetilLayerMask;
        [SerializeField] private GameObject projetil;
        [SerializeField] private TMPro.TextMeshProUGUI numeroProjeteisTxt;
        [SerializeField] private GameObject vitoriaTexto;

        [SerializeField] private LayerMask checkpointLayerMask;

        private bool Atirou { get => Input.GetKeyDown(KeyCode.Space); }

        private int _projeteisQuantidade;
        private MovePorControle _movePorControle;
        private Animator _animator;

        private void Start()
        {
            _projeteisQuantidade = 0;
            AtualizarTextoProjeteis();
            _movePorControle = GetComponent<MovePorControle>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Atirou && _projeteisQuantidade > 0)
            {
                Atirar();
            }
            if (_movePorControle.EstaSeMovendo)
            {
                _animator.SetFloat("Speed_f", 1);
            }
            else
            {
                _animator.SetFloat("Speed_f", 0);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((projetilLayerMask.value & (1 << other.gameObject.layer)) > 0)
            {
                PegarProjetil(other.gameObject);
            }
            if ((checkpointLayerMask.value & (1 << other.gameObject.layer)) > 0)
            {
                GanharJogo(other.gameObject);
            }
        }

        private void Atirar()
        {
            Instantiate(projetil, transform.position + Vector3.forward, projetil.transform.rotation);
            _projeteisQuantidade--;
            AtualizarTextoProjeteis();
        }

        private void AtualizarTextoProjeteis()
        {
            numeroProjeteisTxt.text = "";
            for (int i = 0; i < _projeteisQuantidade; i++)
            {
                numeroProjeteisTxt.text += "| ";
            }
        }

        private void PegarProjetil(GameObject projetil)
        {
            projetil.SetActive(false);
            _projeteisQuantidade++;
            AtualizarTextoProjeteis();
        }

        private void GanharJogo(GameObject checkpoint)
        {
            vitoriaTexto.SetActive(true);
            Destroy(checkpoint);
            Destroy(gameObject);
        }
    }
}

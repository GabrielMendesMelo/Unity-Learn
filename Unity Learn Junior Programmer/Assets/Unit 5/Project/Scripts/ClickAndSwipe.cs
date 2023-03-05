using UnityEngine;

namespace Prototype5
{
    [RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
    public class ClickAndSwipe : MonoBehaviour
    {
        private GameManager gameManager;
        private Camera cam;
        private Vector3 mousePos;
        private TrailRenderer trail;
        private BoxCollider col;

        private bool swiping = false;

        private void Awake()
        {
            cam = Camera.main;
            trail = GetComponent<TrailRenderer>();
            col = GetComponent<BoxCollider>();
            trail.enabled = false;
            col.enabled = false;

            gameManager = FindObjectOfType<GameManager>();
        }

        private void Update()
        {
            if (!gameManager.IsGameOver)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    swiping = true;
                    UpdateComponents();
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    swiping = false;
                    UpdateComponents();
                }
                if (swiping)
                {
                    UpdateMousePosition();
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            Target target = collision.gameObject.GetComponent<Target>();
            if (target != null)
            {
                target.DestroyTarget();
            }
        }

        private void UpdateMousePosition()
        {
            mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cam.transform.position.z));
            transform.position = mousePos;
        }

        private void UpdateComponents()
        {
            trail.enabled = swiping;
            col.enabled = swiping;
        }
    }
}

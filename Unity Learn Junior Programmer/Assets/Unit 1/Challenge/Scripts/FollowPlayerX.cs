﻿using UnityEngine;

namespace Challenge1
{
    public class FollowPlayerX : MonoBehaviour
    {
        public GameObject plane;
        [SerializeField] private Vector3 offset;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.position = plane.transform.position + offset;
        }
    }
}

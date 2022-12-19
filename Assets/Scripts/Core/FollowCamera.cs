using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform target;

        Vector3 difference;

        void Start()
        {
            difference = transform.position - target.position;
        }

        void Update()
        {
            transform.position = target.position + difference;
        }
    }
}


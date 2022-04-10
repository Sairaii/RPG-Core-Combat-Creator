using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform target = null;
        [SerializeField] float cameraMoveSpeed = 5f;

        void Start()
        {

        }

        void LateUpdate()
        {
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, target.position, Time.deltaTime * cameraMoveSpeed); //see [Tip] Smoother Camera Movement
            transform.position = smoothedPosition;
        }
    }
}
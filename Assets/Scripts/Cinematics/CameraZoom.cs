using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace RPG.Cinematics
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera followCamera;
        [SerializeField] float zoomSpeed = 30f;
        
        void Update()
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                float cameraDistancing = Mathf.Clamp(followCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, 10f, 30f);
                followCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = cameraDistancing;
            }
            else
            {
                float cameraDistancing = Mathf.Clamp(followCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, 10f, 30f);
                followCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = cameraDistancing;
            }
        }
    }
}

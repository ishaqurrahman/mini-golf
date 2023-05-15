using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0.2f;   
    public static CameraRotation instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    public void RotateCamera(float XaxisRotation)           
    {
        transform.Rotate(Vector3.down, -XaxisRotation * rotationSpeed); 
    }
}

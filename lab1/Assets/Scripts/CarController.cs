using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CarController : MonoBehaviour { 
    [SerializeField] private Transform _transformFL;
    [SerializeField] private Transform _transformFR; 
    [SerializeField] private Transform _transformBL; 
    [SerializeField] private Transform _transformBR;
    [SerializeField] private WheelCollider _colliderFL; 
    [SerializeField] private WheelCollider _colliderFR; 
    [SerializeField] private WheelCollider _colliderBL;
    [SerializeField] private WheelCollider _colliderBR; 
    [SerializeField] private float _force; 
    [SerializeField] private float _maxAngle;

    private bool CanMove = false;

    public void setMove(bool move)
    {
        this.CanMove = move;
    }

    public void stopInstantly() { 
        setMove(false); 
        _colliderFL.motorTorque = 0f;
        _colliderFR.motorTorque = 0f;
        _colliderFL.steerAngle = 0f;
        _colliderFR.steerAngle = 0f;
        float brake = 10000f;
        _colliderFL.brakeTorque = brake;
        _colliderFR.brakeTorque = brake;
        _colliderBL.brakeTorque = brake;
        _colliderBR.brakeTorque = brake;
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) { 
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        } 
    }
    private void FixedUpdate() {
        if (!CanMove)
        {
            _colliderFL.motorTorque = 0f;
            _colliderFR.motorTorque = 0f;

            _colliderFL.brakeTorque = 3000f;
            _colliderFR.brakeTorque = 3000f;
            _colliderBL.brakeTorque = 3000f;
            _colliderBR.brakeTorque = 3000f;
            return;
        }

        _colliderFL.motorTorque = Input.GetAxis("Vertical") * _force;
        _colliderFR.motorTorque = Input.GetAxis("Vertical") * _force;
        if (Input.GetKey(KeyCode.Space)) { _colliderFL.brakeTorque = 3000f;
            _colliderFR.brakeTorque = 3000f; _colliderBL.brakeTorque = 3000f;
            _colliderBR.brakeTorque = 3000f; } else { _colliderFL.brakeTorque = 0f;
            _colliderFR.brakeTorque = 0f; _colliderBL.brakeTorque = 0f;
            _colliderBR.brakeTorque = 0f;
        } 
        _colliderFL.steerAngle = _maxAngle * Input.GetAxis("Horizontal");
        _colliderFR.steerAngle = _maxAngle * Input.GetAxis("Horizontal");
        RotateWheel(_colliderFL, _transformFL); RotateWheel(_colliderFR, _transformFR);
        RotateWheel(_colliderBL, _transformBL); RotateWheel(_colliderBR, _transformBR);
    }
    private void RotateWheel(WheelCollider collider, Transform transform) { 
        Vector3 position; 
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
        transform.rotation = rotation; 
        transform.position = position;
    }
}
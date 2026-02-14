using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform _transformFL;
    [SerializeField] private Transform _transformFR;
    [SerializeField] private Transform _transformBL;
    [SerializeField] private Transform _transformBR;

    [SerializeField] private WheelCollider _colliderFL;
    [SerializeField] private WheelCollider _colliderFR;
    [SerializeField] private WheelCollider _colliderBL;
    [SerializeField] private WheelCollider _colliderBR;

    [SerializeField] private float _force = 2500f;

    [Header("AI settings")]
    [SerializeField] private float _minSpeedFactor = 0.3f;
    [SerializeField] private float _maxSpeedFactor = 1f;
    [SerializeField] private float _changeSpeedTime = 3f;

    private float _currentSpeedFactor;
    private float _timer;

    private bool CanMove = false;

    public void setMove(bool move)
    {
        this.CanMove = move;
    }

    public void stopInstantly()
    {
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
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
    private void Start()
    {
        _currentSpeedFactor = Random.Range(_minSpeedFactor, _maxSpeedFactor);
    }

    private float _currentSteer = 0f;

    public void SetSteer(float steer)
    {
        _currentSteer = steer;
    }

    private float _externalSpeedMultiplier = 1f;

    public void SetSpeedMultiplier(float value)
    {
        _externalSpeedMultiplier = Mathf.Clamp01(value);
    }

    private void FixedUpdate()
    {
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

        _timer += Time.fixedDeltaTime;

        if (_timer >= _changeSpeedTime)
        {
            _currentSpeedFactor = Random.Range(_minSpeedFactor, _maxSpeedFactor);
            _timer = 0f;
        }

        float motor = _force * _currentSpeedFactor * _externalSpeedMultiplier;

        _colliderFL.motorTorque = motor;
        _colliderFR.motorTorque = motor;

        _colliderFL.steerAngle = _currentSteer;
        _colliderFR.steerAngle = _currentSteer;

        _colliderFL.brakeTorque = 0f;
        _colliderFR.brakeTorque = 0f;
        _colliderBL.brakeTorque = 0f;
        _colliderBR.brakeTorque = 0f;

        RotateWheel(_colliderFL, _transformFL);
        RotateWheel(_colliderFR, _transformFR);
        RotateWheel(_colliderBL, _transformBL);
        RotateWheel(_colliderBR, _transformBR);
    }
    private void RotateWheel(WheelCollider collider, Transform transform)
    {
        Vector3 position;
        Quaternion rotation;

        collider.GetWorldPose(out position, out rotation);

        transform.position = position;
        transform.rotation = rotation;
    }
}

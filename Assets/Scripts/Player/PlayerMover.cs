using Photon.Pun;
using UnityEngine;

public class PlayerMover : MonoBehaviourPun
{
    [SerializeField] private int maxSpeed = 10;
    [SerializeField] private float speedUpRate = 2.0f;
    [SerializeField] private float speedDownRate = -1.0f;
    private float _currentSpeed;
    private Rigidbody _rb;
    private int _tapCounter;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.maxLinearVelocity = maxSpeed;
    }

    private void Update()
    {
        if (!photonView.IsMine) return;

        if (Input.GetKeyDown(KeyCode.Space))
            _tapCounter += 10;
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine) return;

        if (_tapCounter > 0)
            _rb.AddForce(transform.forward * (speedUpRate * _tapCounter), ForceMode.Acceleration);
        else
            _rb.AddForce(transform.forward * speedDownRate, ForceMode.Acceleration);
        if (_rb.linearVelocity.z < 0)
            _rb.linearVelocity = Vector3.zero;
        _tapCounter = 0;
    }
}
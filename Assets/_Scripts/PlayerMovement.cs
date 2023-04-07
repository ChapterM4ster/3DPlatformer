using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("camera")]
    [SerializeField] private Transform _eyes;
    [SerializeField] private float _sensitivity;
    [Range(-90f, 0f)]
    [SerializeField] private float _camLimitMin;
    [Range(0f, 90f)]
    [SerializeField] private float _camLimitMax;
    private float _camAngle = 0.0f;
    [SerializeField] public GameObject gameOverText;

    [Header("scriptableobject")]
    [SerializeField] private float _speed;
    [SerializeField] public int _health;
    [SerializeField] private EntityStats _entityStats;
    private Rigidbody _rb;

    [Header("jump")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private KeyCode _jumpKey;

    [Header("scripts")]
    public Grapple _grapple;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _rb = GetComponent<Rigidbody>();

        _speed = _entityStats._MS;
        _health = _entityStats._HP;
    }

    private void Update()
    {
        RotateEyes();
        RotateBody();

        if (Input.GetKeyDown(_jumpKey))
        {
            TryJump();
        }

        SlowDownTime();

        if (_health <= 0)
        {
            ReloadLevel();
        }

    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(0);
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void RotateEyes()
    {
        float yMouse = Input.GetAxisRaw("Mouse Y") * _sensitivity * Time.deltaTime;
        _camAngle -= yMouse;
        _camAngle = Mathf.Clamp(_camAngle, _camLimitMin, _camLimitMax);
        _eyes.localRotation = Quaternion.Euler(_camAngle, 0, 0);
    }
    private void RotateBody()
    {
        float xMouse = Input.GetAxisRaw("Mouse X") * _sensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * xMouse);
    }

    public void Move()
    {

        if (_grapple.grappling == true)
        {

        }
        else if (_grapple.grappling == false)
        {
            float xDir = Input.GetAxisRaw("Horizontal");
            float zDir = Input.GetAxisRaw("Vertical");

            Vector3 dir = transform.right * xDir + transform.forward * zDir;

            _rb.velocity = new Vector3(0, _rb.velocity.y, 0) + dir.normalized * _speed;
        }

        
    }

    private void TryJump()
    {
        if (IsGrounded())
        {
            Jump(_jumpForce);
        }
    }

    private void Jump(float jumpForce)
    {
        _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
        _rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        return (Physics.Raycast(transform.position, -transform.up, out hit, 1.1f));
    }


    private void SlowDownTime()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Time.timeScale = 0.5f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "OutOfBounds")
        {
            _health = _health - 20;
        }

        if (other.tag == "Victory")
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            gameOverText.gameObject.SetActive(true);
            
        }
    }
}
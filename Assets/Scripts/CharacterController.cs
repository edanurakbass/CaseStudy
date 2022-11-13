using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private GameObject _stackPos;
    [SerializeField] private Transform _lastChildTransform;
    private Joystick _joystick;
    private Rigidbody _rb;
    private Vector3 _joystickDirection;
    private float _yPos = 1f;
    public bool _isCollisionProducer;

    private void Start()
    {
        _lastChildTransform = transform.GetChild(transform.childCount - 1).transform;
        _joystick = FindObjectOfType<DynamicJoystick>();
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _joystickDirection = new Vector3(
            _joystick.Horizontal * 10f,
            _rb.velocity.y,
            _joystick.Vertical * 10f
            );

        if (_joystick.Direction != Vector2.zero)
        {
            transform.forward = _joystickDirection;
        }
        _rb.velocity = _joystickDirection;
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Follower"))
        {
            collision.transform.SetParent(transform);
            collision.transform.localRotation = Quaternion.Euler(0,0,0);
            collision.transform.localPosition = _lastChildTransform.localPosition + Vector3.back;
            _lastChildTransform = transform.GetChild(transform.childCount -1).transform;
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            other.transform.SetParent(transform.GetChild(0).transform);
            other.transform.localRotation = Quaternion.Euler(0,0,0);
            other.transform.DOLocalJump(new Vector3(0, _yPos, -.2f), 1f, 1,1f);
            _yPos += .4f;
        }
        else if (other.CompareTag("Producer") && !_isCollisionProducer)
        {
            other.GetComponent<BigCube>().ProduceModel(PoolObjectType.Cube);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Producer"))
        {
            _isCollisionProducer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Producer"))
        {
            _isCollisionProducer = false;
        }
    }
}

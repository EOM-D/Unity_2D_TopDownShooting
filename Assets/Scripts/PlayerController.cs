using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rigid;
    Animator _anim;

    public float _speed = 1.0f;
    float _h;                // ����
    float _v;                // ����
    int _direction = 0;     // ����; 0 = Down, 1 = Left, 2 = Up, 3 = Right

    enum PlayerState
    {
        Wait,
        Move,
        Attack,
        Over
    }

    PlayerState _state = PlayerState.Wait;

    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
            _state = PlayerState.Attack;
        else if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            _state = PlayerState.Move;
        else
            _state = PlayerState.Wait;

        switch (_state)
        {
            case PlayerState.Wait:
                OnWait();
                break;
            case PlayerState.Move:
                OnMove();
                break;
            case PlayerState.Attack:
                OnAttack();
                break;
            case PlayerState.Over:
                OnOver();
                break;
        }
    }

    void OnWait()
    {
        _rigid.velocity = Vector2.zero;

        // �ִϸ��̼� ó��
    }

    void OnMove()   // �̵�
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");
        bool isHorizonMove = Mathf.Abs(_h) > Mathf.Abs(_v);

        Vector2 moveVec = isHorizonMove ? new Vector2(_h, 0) : new Vector2(0, _v);
        _rigid.velocity = moveVec * _speed;

        // ���� Ȯ��
        if (isHorizonMove)
        {
            if (_h < 0) _direction = 1;
            else _direction = 3;
        }
        else
        {
            if (_v < 0) _direction = 0;
            else _direction = 4;
        }

        // �ִϸ��̼� ó��
    }

    void OnAttack() // ����
    {
        // ���� Ȯ��
        // �ִϸ��̼� ó��
    }

    void OnOver()
    {
        return;
    }
}

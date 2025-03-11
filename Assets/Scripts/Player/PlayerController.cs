using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //private Transform platform; // 현재 서 있는 발판
    //private Vector3 platformPreviousPosition;
    //private bool isOnPlatform = false;

    //컨트롤 움직임 
    [Header("Movement")]
    public float moveSpeed;
    public float _JumpPower;
    private Vector2 _curMovementInput;
    public LayerMask groundLayMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSenSitivity;
    private Vector2 mouseDelta;

    public Action Eat;

    Rigidbody rd;
    private void Awake()
    {
        rd = GetComponent<Rigidbody>();
    }
    private void Update()
    {

    }
    private void FixedUpdate()
    {
        // 발판 위에 있을 때 발판의 이동에 따라 플레이어도 이동
        //if (isOnPlatform && platform != null)
        //{
        //    Vector3 platformDelta = platform.position - platformPreviousPosition;
        //    transform.position += platformDelta;
        //}

        Move();
    }

    private void LateUpdate()
    {
        // 발판 위치 업데이트
        //if (isOnPlatform && platform != null)
        //{
        //    platformPreviousPosition = platform.position;
        //}
        CamerLook();
    }
    //값을 받아왔으니까 get 
    void Move()
    {
        //받아온 값을 가지고 캐릭터를 움직여 줘야해 
        //새로운 백터의 값을 구하는 방법은 ? > 백터의 합 > 방향
        //결국에는 z축(001*input값) 과 x축(100*input값)의 합을 구하는 과정 
        Vector3 dir = (transform.forward * _curMovementInput.y) + (transform.right * _curMovementInput.x);

        dir *= moveSpeed; //속도 
        dir.y = rd.velocity.y; //중력은 유지해줘야된다.
        rd.velocity = dir;
    }

    void CamerLook()
    {
        // 상하 회전(X축) - 카메라만 회전
        camCurXRot += mouseDelta.y * lookSenSitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0); //x축의 움직임을 한계로 제한 

        // 좌우 회전(Y축) - 플레이어 전체 회전
        transform.Rotate(Vector3.up * mouseDelta.x * lookSenSitivity);
    }

    //움직임의 대한 함수 이벤트를 달아줄 함수가 필요 
    // set 역활 
    public void OnMove(InputAction.CallbackContext context) //inputcallback값을 받아와서 그걸 비교해서 이벤트를 발생
    {
        //아마도 context에는 우리가 어떻게 누르는지에 대한 값이 넘어옴
        if (context.phase == InputActionPhase.Performed) //계속 눌러지는 상태 
        {
            //이 값을 받아와야해
            _curMovementInput = context.ReadValue<Vector2>(); // 움직임은 vector2로 우리가 input에서 설정했음 따라서 그값이 넘어옴
        }
        else if (context.phase == InputActionPhase.Canceled) // 눌렀다가 때는 동작 
        {
            _curMovementInput = Vector2.zero;
        }
        //눌러질때만 값이 존재 
    }

    //점프 이벤트 함수 
    public void OnJump(InputAction.CallbackContext context)
    {
        //점프는 한번만 누르넌거고 inputsystem에서 button으로 햇어
        if (context.phase == InputActionPhase.Started &&IsGround())
        //if (context.phase == InputActionPhase.Started)
        {
            rd.AddForce(Vector2.up * _JumpPower, ForceMode.Impulse); //방형 *힘 +형태
        }
    }


    public void OnLock(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnEat(InputAction.CallbackContext context)
    {
        if (context.phase ==InputActionPhase.Started)
        {
            Eat?.Invoke();
        }
    }

    bool IsGround()
    {
        // z,x축을 이용해서 ray를 4개 발사 
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position +(transform.forward*0.2f)+(transform.up *0.01f),Vector3.down),
            new Ray(transform.position +(-transform.forward*0.2f)+(transform.up *0.01f),Vector3.down),
            new Ray(transform.position +(transform.right*0.2f)+(transform.up *0.01f),Vector3.down),
            new Ray(transform.position +(-transform.right*0.2f)+(transform.up *0.01f),Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
           // Debug.DrawRay(rays[i].origin, rays[i].direction * 0.1f, Color.red);
            if (Physics.Raycast(rays[i], 0.1f, groundLayMask))// 위치 길이 타겟
            {
                return true;
            }
        }
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 발판인지 확인 (발판에 "Platform" 태그를 추가해야 함)
        //if (collision.gameObject.CompareTag("Platform"))
        //{
        //    platform = collision.transform;
        //    platformPreviousPosition = platform.position;
        //    isOnPlatform = true;
        //}
    }

    private void OnCollisionExit(Collision collision)
    {
        // 발판에서 벗어났는지 확인
        //if (collision.gameObject.CompareTag("Platform"))
        //{
        //    isOnPlatform = false;
        //    platform = null;
        //}
    }

}

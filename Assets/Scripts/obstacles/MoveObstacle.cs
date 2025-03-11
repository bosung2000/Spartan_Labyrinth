using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementAxis
{
    X_Axis,
    Z_Axis
}

public class MoveObstacle : MonoBehaviour
{

    [SerializeField] private MovementAxis movementAxis = MovementAxis.X_Axis;  // 이동 축 선택
    [SerializeField] private float xmoveSpeed = 0.8f;     // 이동 속도
    [SerializeField] private float xmoveDistance = 5.0f;  // 좌우 이동 거리

    // Z축 이동에 대한 추가 설정 
    [SerializeField] private float zmoveSpeed = 0.8f;
    [SerializeField] private float zmoveDistance = 5.0f;

    // 초기 위치 저장
    private Vector3 startPosition;

    void Start()
    {
        // 시작 위치 저장
        startPosition = transform.position;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector3 newPosition = startPosition;


        switch (movementAxis)
        {
            case MovementAxis.X_Axis:
                float xOffset = Mathf.Sin(Time.time * xmoveSpeed) * xmoveDistance;
                newPosition.x = startPosition.x + xOffset;
                break;
            case MovementAxis.Z_Axis:
                float zOffset = Mathf.Sin(Time.time * zmoveSpeed) * zmoveDistance;
                newPosition.z = startPosition.z + zOffset;
                break;
            default:
                break;
        }

        // 오브젝트 위치 업데이트
        transform.position = newPosition;

    }

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트가 플레이어인지 확인
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어를 발판의 자식으로 설정
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // 충돌에서 벗어난 오브젝트가 플레이어인지 확인
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어를 발판의 자식에서 해제
            collision.transform.SetParent(null);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zelda
{
    public class PlayerController : MonoBehaviour
    {
        [Header("References")]
        CharacterController controller;
        [SerializeField] Animator animator;

        float moveSpeed = 6f;    // 이동 속도
        float gravity = -9.81f;  // 중력 값
        float smoothTime = 0.2f;

        float currentSpeed;
        float velocity;
        const float ZeroF = 0f;

        static readonly int Speed = Animator.StringToHash("Speed");


        void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            HandleMovement();
            UpdateAnimator();
        }

        void HandleMovement()
        {
            // 플레이어 입력 처리
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveZ = Input.GetAxisRaw("Vertical");

            var movementDirection = new Vector3(moveX, 0, moveZ).normalized;
            var adjustedDirection = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up) * movementDirection;
            if (ZeroF < adjustedDirection.magnitude)
            {
                HandleRotation(adjustedDirection);
                SmoothSpeed(moveSpeed);
                MoveCharacter(adjustedDirection);
            }
            else if(ZeroF < currentSpeed)
            {
                MoveCharacter(transform.forward);
                SmoothSpeed(ZeroF);
            }
        }
        void HandleRotation(Vector3 adjustedDirection)
        {
            transform.LookAt(transform.position + adjustedDirection);
        }

        void MoveCharacter(Vector3 adjustedDirection)
        {
            var adjustedMovement = adjustedDirection * currentSpeed * Time.deltaTime;
            controller.Move(adjustedMovement);
            controller.Move(Vector3.down);
        }

        void SmoothSpeed(float value)
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, value, ref velocity, smoothTime);
        }

        void UpdateAnimator()
        {
            animator.SetFloat(Speed, currentSpeed / moveSpeed);
        }
    }
}
using System;
using UnityEngine;

namespace Memoriae.Control
{
    public class PlayerController : MonoBehaviour
    {
        public event EventHandler OnPlayerPaused;

        [SerializeField] private float walkSpeed = 2.0f;
        [SerializeField] private float jumpHeight = 1.0f;
        [SerializeField] private float gravityValue = -9.81f;
        [SerializeField] private float rotationSpeed = 0.1f;
        [SerializeField] private float rotationVelocity;
        [SerializeField] private bool blockedZMovement = false;
        [SerializeField] private AudioSource footstepsAudio;
        [SerializeField] private AudioSource jumpAudio;

        private CharacterController controller;
        private Vector3 playerVelocity;
        private Animator anim;
        private InputManager inputManager;
        private Transform cameraTransform;
        private bool groundedPlayer;

        private void Start()
        {
            inputManager = InputManager.Instance;
            Application.targetFrameRate = 75;
            controller = GetComponent<CharacterController>();
            anim = GetComponentInChildren<Animator>();
            cameraTransform = Camera.main.transform;

            InputManager.Instance.OnPauseAction += InputManager_OnPauseAction;
        }

        private void InputManager_OnPauseAction(object sender, EventArgs e)
        {
            OnPlayerPaused?.Invoke(this, EventArgs.Empty);
        }

        private void Update()
        {
            CheckGrounded();
            Movement();
            Jump();
        }

        private void CheckGrounded()
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0f)
            {
                playerVelocity.y = 0f;
            }
        }

        private void Movement()
        {
            Vector2 inputMovement = inputManager.GetPlayerMovement();
            Vector3 move;

            if (blockedZMovement)
            {
                move = new Vector3(inputMovement.x, 0f, 0f);
            }
            else
            {
                move = new Vector3(inputMovement.x, 0f, inputMovement.y);
            }

            move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
            move.y = 0f;

            if (move.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationVelocity, rotationSpeed);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                if (!footstepsAudio.isPlaying)
                {
                    footstepsAudio.Play();
                }

                footstepsAudio.mute = false;
                anim.SetFloat("Blend", 0.6f);
                controller.Move(move * Time.deltaTime * walkSpeed);
            }
            else
            {
                anim.SetFloat("Blend", 0);
                footstepsAudio.mute = true;
            }
        }

        private void Jump()
        {
            if (inputManager.PlayerJumped() && groundedPlayer)
            {
                anim.SetBool("grounded", false);
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
                jumpAudio.Play();
            }
            else
            {
                anim.SetBool("grounded", true);
            }

            if (!groundedPlayer)
            {
                footstepsAudio.mute = true;
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }
}


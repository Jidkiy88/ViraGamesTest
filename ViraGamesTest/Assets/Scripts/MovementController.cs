using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private HumansHolder player;
        [SerializeField] private Transform playerX;
        [SerializeField] private Transform playerY;
        [SerializeField] private float verticalSpeed;
        [SerializeField] private float horizontalSpeed;
        [SerializeField] private float horizontalSpeedBoost;
        [SerializeField] private Vector2 limitZone;

        private float _halfScreen;
        private bool _editor = false;
        private bool _isMoving = false;

        private void Awake()
        {
            player.OnStartRun += StartRun;
            player.OnDeath += OnDeath;
            player.OnFinish += OnFinish;
            player.OnStateChange += CheckState;
            _halfScreen = Screen.width / 2;
            _editor = Application.platform == RuntimePlatform.WindowsEditor;
        }

        private void OnDestroy()
        {
            player.OnStartRun -= StartRun;
            player.OnDeath -= OnDeath;
            player.OnFinish -= OnFinish;
            player.OnStateChange -= CheckState;
        }

        private void CheckState(bool state)
        {
            _isMoving = state;
        }

        private void OnDeath()
        {
            player.OnDeath -= OnDeath;
            _isMoving = false;
        }

        private void OnFinish()
        {
            player.OnFinish -= OnFinish;
            _isMoving = false;
        }

        public void StartRun()
        {
            _isMoving = true;
        }

        private void Update()
        {
            if (_isMoving)
            {
                VerticalMovement();
                if (_editor)
                {
                    PCInput();
                }
                else
                {
                    HorizontalMovement();
                }
            }
        }

        private void PCInput()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                var playerPos = Camera.main.WorldToScreenPoint(playerX.position);
                var value = (playerPos.x - Input.mousePosition.x) / _halfScreen;
                var delta = (Input.mousePosition.x - _halfScreen) / _halfScreen;
                var xPosRaw = playerX.position.x + ((-value * horizontalSpeed * horizontalSpeedBoost) * Time.deltaTime);
                var xPos = Mathf.Clamp(xPosRaw, limitZone.x, limitZone.y);
                playerX.position = new Vector3(xPos, playerX.position.y, playerX.position.z);
            }
        }

        private void HorizontalMovement()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    var delta = touch.deltaPosition.x;
                    var xPosRaw = playerX.position.x + (delta * horizontalSpeed * Time.deltaTime);
                    var xPos = Mathf.Clamp(xPosRaw, limitZone.x, limitZone.y);
                    playerX.position = new Vector3(xPos, playerX.position.y, playerX.position.z);
                }
            }
        }

        private void VerticalMovement()
        {
            var zPos = playerY.position.z + (verticalSpeed * Time.deltaTime);
            playerY.position = new Vector3(playerY.position.x, playerY.position.y, zPos);
        }
    }
}

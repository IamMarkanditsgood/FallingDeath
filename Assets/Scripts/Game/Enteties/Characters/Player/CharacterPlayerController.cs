using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class CharacterPlayerController : MonoBehaviour, IHitable, ICustomisable
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private CharacterMovementManager _movementEngine;
    [SerializeField] private PlayerInputSystem _playerInputSystem;
    [SerializeField] private Transform _shootingPos;
    
    private CharacterPlayerConfig _playerConfig;

    private BoosterUseManager _boosterUseManager = new BoosterUseManager();
    private Collider2D _playerCollider;

    private float _currentHealth;
    private float _currentDamage;
    private float _currentSpeed;
    private float _currentShootingSpeed;

    private float _screenWidthInUnits;
    private float _horizontalInput;

    private bool _isActive;

    private Coroutine _shooting;

    private void OnDestroy()
    {
        Toogle(false);
    }

    private void OnDisable()
    {
        Toogle(false);
    }

    public void Init(CharacterPlayerConfig characterPlayerConfig)
    {
        _playerConfig = characterPlayerConfig;

        _playerCollider = gameObject.GetComponent<Collider2D>();
        Rigidbody2D playerRB = GetComponent<Rigidbody2D>();

        if (!_mainCamera) _mainCamera = Camera.main;

        _movementEngine.Initialize(playerRB, _playerCollider);
        _boosterUseManager.Init(playerRB);

        _playerInputSystem.Init();

        _currentHealth = _playerConfig.BasicHealth;
        _currentSpeed = _playerConfig.MoveSpeed;
        _currentDamage = _playerConfig.BasicDamage;
        _currentShootingSpeed = _playerConfig.BasicShootingSpeed;

        SetCustomisation();
    }

    public void Toogle(bool state)
    {
        _isActive = state;

        if(_isActive)
        {
            Subscribe();

            _shooting = StartCoroutine(Shooting());
        }
        else
        {
            Unsubscribe();

            StopCoroutine(_shooting);
        }
    }

    public void SetCustomisation()
    {
        ICustomizer customizer = GetComponent<ICustomizer>();

        if (customizer != null)
            customizer.Customize();
    }
    public void Subscribe()
    {
        InputEvents.OnMovePlayer += OnMoveInput;
    }
    public void Unsubscribe()
    {
        InputEvents.OnMovePlayer -= OnMoveInput;
    }

    private void Update()
    {
        if(!_isActive) return;

        _playerInputSystem.UpdateInput();

        CalculateScreenBounds();
        HandleScreenWrapping();
    }

    private void FixedUpdate()
    {
        if (!_isActive) return;
        // Physics-related movement in FixedUpdate
        _movementEngine.Move(_horizontalInput, _currentSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isActive) return;
        HandleCollision(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isActive) return;
        HandleTrigger(collision);
    }

    private void HandleCollision(Collision2D collision)
    {
        if(!IsInteractableTag(collision)) return;
        
    }

    private void HandleTrigger(Collider2D collider)
    {
        if (!IsInteractableMask(collider.gameObject, _playerConfig.BoosterLayers)) return;

    }

    private bool IsInteractableTag(Collision2D collision)
    {
        return collision.gameObject.CompareTag(GameTags.instantiate.PlatformTag) ||
           collision.gameObject.CompareTag(GameTags.instantiate.EnemyTag);
    }
    private bool IsInteractableMask(GameObject obj, LayerMask mask)
    {
        return (mask.value & (1 << obj.layer)) != 0;
    }

    private void OnMoveInput(Vector2 direction)
    {
        _horizontalInput = direction.x;
    }

    private void CalculateScreenBounds()
    {
        float screenHeight = _mainCamera.orthographicSize * 2;
        float screenWidth = screenHeight * Screen.width / Screen.height;
        _screenWidthInUnits = screenWidth / 2f;
    }

    private void HandleScreenWrapping()
    {
        if (transform.position.x > _screenWidthInUnits)
            transform.position = new Vector2(-_screenWidthInUnits, transform.position.y);
        else if (transform.position.x < -_screenWidthInUnits)
            transform.position = new Vector2(_screenWidthInUnits, transform.position.y);
    }

    private void Die()
    {
        if (!_isActive) return;
        Debug.Log("Player Died!");
        Time.timeScale = 0;
    }

    public void Hit(float damage = 0)
    {
        if (!_isActive) return;
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
    }
    
    private IEnumerator Shooting()
    {
        while (true)
        {
            if (_isActive)
            {
                MakeShoot();

                yield return new WaitForSeconds(_currentShootingSpeed);
            }
        }
    }

    private void MakeShoot()
    {
        BasicAmmoController ammo = PoolObjectManager.instant.ammoPoolObjectManager.GetAmmo(_playerConfig.Bullet);
        ammo.gameObject.transform.position = _shootingPos.position;
        ammo.SetDamage(_currentDamage);
        ammo.Toggle(true);
    }
}

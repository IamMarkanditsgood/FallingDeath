using TMPro;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour, ICustomisable, IHitable, IFreezable, IBurnable
{
    [SerializeField] private BasicEnemyConfig _enemyConfig;

    [Header("UI")]
    [SerializeField] private Canvas _enemyCanvas;
    [SerializeField] private TMP_Text _health;

    private bool _isActive;
    private int _currentHealth;
    private float _currentSpeed;

    public EnemyTypes EnemyType => _enemyConfig.EnemyType;

    public virtual void Init()
    {
        _enemyCanvas.worldCamera = Camera.main;       
    }

    public virtual void Toggle(bool state)
    {
        _isActive = state;

        if(!_isActive)
        {
            StopAllCoroutines();
        }
        else
        {
            SetCustomisation();
            ConfigureParameters();
        }
    }

    private void ConfigureParameters()
    {
        float randomHealth = Random.Range(_enemyConfig.BasicHealthRange.x, _enemyConfig.BasicHealthRange.y);
        _currentHealth = Mathf.RoundToInt(randomHealth);

        _currentSpeed = _enemyConfig.MovementSpeed;

        UpdateHealthUI();
    }

    private void Update()
    {
        if (!_isActive)
            return;

        UpdateEnemy();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!_isActive)
            return;

        HandleTriggerContact(collider);
    }

    public virtual void HandleTriggerContact(Collider2D collider)
    {
        if (collider.CompareTag(_enemyConfig.GroundTag))
        {
            Die();
        }
        else if (IsInteractableMask(collider.gameObject, _enemyConfig.PlayerLayer))
        {
            IHitable player = collider.GetComponent<IHitable>();
            if (player != null)
            {
                player.Hit(_enemyConfig.BasicDamage);
            }
        }
    }
    private bool IsInteractableMask(GameObject obj, LayerMask mask)
    {
        return (mask.value & (1 << obj.layer)) != 0;
    }
    public virtual void UpdateEnemy() 
    {
        Move();
    }

    public virtual void Move()
    {
        Vector2 moveDir = new Vector2(_enemyConfig.MovementDirection.x, _enemyConfig.MovementDirection.y).normalized;
        transform.Translate(moveDir * _currentSpeed * Time.deltaTime);
    }
    
    public void Hit(float damage = 0)
    {
        _currentHealth -= Mathf.RoundToInt(damage);

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
        UpdateHealthUI();
    }

    public virtual void Die()
    {
        Toggle(false);
        PoolObjectManager.instant.enemyPoolObjectManager.DisableEnemy(this, _enemyConfig.EnemyType);
    }

    public void SetCustomisation()
    {
        ICustomizer customizer = GetComponent<ICustomizer>();

        if (customizer != null)
            customizer.Customize();
    }

    private void UpdateHealthUI()
    {
        _health.text = _currentHealth.ToString();
    }

    public void Freeze(float time, float speedProcent)
    {
    }

    public void SetFire(float damage, float time)
    {
    }
}
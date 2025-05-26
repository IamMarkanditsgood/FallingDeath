using UnityEngine;

public class BasicEnemyController : MonoBehaviour, ICustomisable, IHitable
{
    [SerializeField] private EnemyTypes _enemyType;

    private BasicEnemyConfig _enemyConfig;
    private bool _isActive;
    private int _currentHealth;
    private float _currentSpeed;

    public EnemyTypes EnemyType => _enemyType;

    public virtual void Init(BasicEnemyConfig enemyConfig)
    {
        _enemyConfig = enemyConfig;
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
        _currentHealth = 100;
        _currentSpeed = _enemyConfig.MovementSpeed;
    }

    private void Update()
    {
        if (!_isActive)
            return;

        UpdateEnemy();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isActive)
            return;

        HandleContact(collision);
    }
    public virtual void HandleContact(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_enemyConfig.GroundTag))
        {
            Die();
        }
        else if(IsInteractableMask(collision.gameObject, _enemyConfig.PlayerLayer))
        {
            IHitable player = collision.gameObject.GetComponent<IHitable>();
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
    }

    public virtual void Die()
    {
        Toggle(false);
        PoolObjectManager.instant.enemyPoolObjectManager.DisableEnemy(this, _enemyType);
    }

    public void SetCustomisation()
    {
        ICustomizer customizer = GetComponent<ICustomizer>();

        if (customizer != null)
            customizer.Customize();
    }
}
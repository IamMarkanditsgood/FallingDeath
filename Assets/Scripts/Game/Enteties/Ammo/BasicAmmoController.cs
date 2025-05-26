using System.Collections;
using UnityEngine;

public abstract class BasicAmmoController : MonoBehaviour, ICustomisable
{
    private BasicAmmoConfig _ammoConfig;

    private float _damage;

    private bool _isActive;

    public AmmoTypes AmmoType { get; private set; }

    private Coroutine _lifeTime;

    public virtual void Init(BasicAmmoConfig ammoConfig)
    {
        _ammoConfig = ammoConfig;
        AmmoType = _ammoConfig.AmmoType;
        SetCustomisation();
    }
    public void SetCustomisation()
    {
        ICustomizer customizer = GetComponent<ICustomizer>();

        if (customizer != null)
            customizer.Customize();
    }
    public virtual void Toggle(bool state)
    {
        _isActive = state;

        if(_isActive)
        {
            _lifeTime = StartCoroutine(Lifetimer());
        }
        else if(!_isActive)
        {        
            StopAllCoroutines();

            if (_lifeTime != null)
                _lifeTime = null;
        }
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    private void Update()
    {
        if(!_isActive) 
            return;

        UpdateAmmo();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isActive)
            return;

        HandleCollision(collision);
    }

    public virtual void UpdateAmmo()
    {
        Move();
    }

    public virtual void Move() { if (!_isActive) return; }

    public virtual void HandleCollision(Collider2D collision)
    {
        if (!_isActive || !IsInteractableMask(collision.gameObject, _ammoConfig.EnemyLayers)) return;

        HitObject(collision.gameObject);
        DisableAmmo();
    }

    private bool IsInteractableMask(GameObject obj, LayerMask mask)
    {
        return (mask.value & (1 << obj.layer)) != 0;
    }

    public virtual void HitObject(GameObject hitedObject)
    {
        IHitable hitable = hitedObject.GetComponent<IHitable>();

        if (hitable != null)
            hitable.Hit(_damage);
    }

    public virtual void DisableAmmo()
    {
        Toggle(false);
        PoolObjectManager.instant.ammoPoolObjectManager.DisableAmmo(this, AmmoType);
    }


    private IEnumerator Lifetimer()
    {
        yield return new WaitForSeconds(_ammoConfig.LifeTime);
        DisableAmmo();
    }
}

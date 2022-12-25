using Assets.Scripts.Decorator_Pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class EnemyBehaviour : MonoBehaviour, ISubject
{
    public bool IsDead { get; private set; }

    public bool IsAttacked { get; private set; }

    [SerializeField]
    List<IObserver> _observers = new List<IObserver>();

    [SerializeField] const float MaxHealth = 10;

    [SerializeField] private Healthbar _healthbar;

    [SerializeField] private float _currentHealth;

    private GameObject _damagingWeapon;

    private WeaponItem _weaponItem;

    private float _damageDone;
    
    // Start is called before the first frame update
    void Start()
    {
        IsDead = false;

        Debug.Log("Yes");

        _currentHealth = MaxHealth;

        _healthbar.UpdateHealthBar(MaxHealth, _currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Weapon"))
        {
            IsAttacked = true;

            Alert();

            _damagingWeapon = collision.gameObject;

            _weaponItem = _damagingWeapon.GetComponent<WeaponItem>();

            _damageDone = _weaponItem.DealDamage();

            _currentHealth -= _damageDone;

            _healthbar.UpdateHealthBar(MaxHealth, _currentHealth);

            if (_currentHealth <= 0)
            {
                IsDead = true;

                Alert();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Weapon"))
        {
            IsAttacked = false;

            Debug.Log("!");

            Alert();
        }
    }

    public void Subscribe(IObserver observer)
    {
        this._observers.Add(observer);
    }

    public void Unsub(IObserver observer)
    {
        this._observers.Remove(observer);
    }

    public void Alert()
    {
        foreach (var observer in _observers)
        {
            observer.Check(this);
        }
    }
}

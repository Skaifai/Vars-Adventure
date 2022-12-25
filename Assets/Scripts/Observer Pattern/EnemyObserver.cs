using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class EnemyObserver : MonoBehaviour, IObserver
{
    [SerializeField] private Image _healthbar;

    private Animator _animator;

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    public void Check(ISubject subject)
    {
        if(subject != null)
        {
            if (((EnemyBehaviour)subject).IsDead == true)
            {
                _animator.SetBool("IsDead", true);
            }
            else _animator.SetBool("IsDead", false);

            if (((EnemyBehaviour)subject).IsAttacked == true)
            {
                _animator.SetBool("IsAttacked", true);
            }
            else _animator.SetBool("IsAttacked", false);
        }
    }
}

using Assets.Scripts.Decorator_Pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Knight observer class, which is responsible for the animator states control.
/// </summary>
internal class KnightObserver : MonoBehaviour, IObserver
{
    [SerializeField]
    private GameObject currentWeapon;

    // Pos and rot of the instantiated weapon
    private Vector3 swordPosition = new Vector3(0, 0, 0);
    private Quaternion swordRotation = new Quaternion(0, 0, 0, 0);

    // This boolean represents the current state of the knight sprite in terms of x direction.
    [SerializeField]
    private bool _lookingRight = true;

    // Knight game object animator field.
    private Animator _knightAnimator;

    // Knight game object sprite renderer field.
    private SpriteRenderer _spriteRenderer;

    // Awake method. Called when the script instance is being loaded.
    void Awake()
    {
        // Fetching the game object's animator component.
        _knightAnimator = gameObject.GetComponent<Animator>();

        // Fetching the GO's sprite renderer component.
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        _lookingRight = true;
        //Debug.Log("KnightObserver Loaded!");
    }

    // Interface method. It checks states of an observable subject and reacts appropriately.
    public void Check(ISubject subject)
    {
        // We should check if the subject field is not null.
        if (subject != null)
        {
            // Checking if the subject is currently running a moving command.
            if (((AgentMovement)subject).IsMoving == false)
            {
                _knightAnimator.SetBool("IsMoving", false);
            }
            else _knightAnimator.SetBool("IsMoving", true);

            // Checking if the current moving command's destination's x coordinate to see if we need to flip the sprite.
            // Since this method is called very often, we should check if the float field is not null and only then execute the code.
            if (((AgentMovement)subject).CurrentCommandXCoord != null)
            {
                // If the player's sprite is facing to the left, but the move command is to the right, then we flip.
                if (((AgentMovement)subject).CurrentCommandXCoord > gameObject.transform.position.x && _lookingRight == false)
                {
                    _spriteRenderer.flipX = false;
                    _lookingRight = true;
                }
                // If the the player's sprite is facing to the right, but the move command is to the left, then we flip.
                else if (((AgentMovement)subject).CurrentCommandXCoord < gameObject.transform.position.x && _lookingRight == true)
                {
                    _spriteRenderer.flipX = true;
                    _lookingRight = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Weapon"))
        {
            swordPosition = new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, gameObject.transform.position.z);
            currentWeapon = Instantiate(collision.gameObject, swordPosition, swordRotation, gameObject.transform);
            collision.gameObject.SetActive(false);
        }
    }
}

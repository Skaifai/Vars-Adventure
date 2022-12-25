using Assets.Scripts.Decorator_Pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Knight observer class, which is responsible for the animator states control.
/// </summary>
internal class KnightObserver : MonoBehaviour, IObserver
{
    // This boolean represents the current state of the knight sprite in terms of x direction.
    [SerializeField]
    private bool _lookingRight = true;

    // Knight game object animator field.
    private Animator _knightAnimator;

    // Right hand game object animator field.
    private Transform _handsTransform;

    // Knight game object sprite renderer field.
    private SpriteRenderer _spriteRenderer;

    // Awake method. Called when the script instance is being loaded.
    void Awake()
    {
        // Fetching the transform of the sprite's hands.
        _handsTransform = gameObject.transform.Find("Hands").GetComponent<Transform>();

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
            // Checking if the subject is currently attacking.
            if (((PlayerControls)subject).IsAttacking == false)
            {
                //_knightAnimator.SetBool("IsAttacking", false);
            }
            else _knightAnimator.SetBool("IsAttacking", true);

            // Checking if the subject is currently running a moving command.
            if (((PlayerControls)subject).IsMoving == false)
            {
                _knightAnimator.SetBool("IsMoving", false);
            }
            else _knightAnimator.SetBool("IsMoving", true);

            // Checking if the current moving command's destination's x coordinate to see if we need to flip the sprite.
            // Since this method is called very often, we should check if the float field is not null and only then execute the code.
            if (((PlayerControls)subject)._currentMoveCommandXCoord != null)
            {
                // If the player's sprite is facing to the left, but the move command is to the right, then we flip.
                if (((PlayerControls)subject)._currentMoveCommandXCoord > gameObject.transform.position.x && _lookingRight == false)
                {
                    _spriteRenderer.flipX = false;
                    _lookingRight = true;
                    _handsTransform.localScale = new Vector3(-1, 1, 1);
                }
                // If the the player's sprite is facing to the right, but the move command is to the left, then we flip.
                else if (((PlayerControls)subject)._currentMoveCommandXCoord < gameObject.transform.position.x && _lookingRight == true)
                {
                    _spriteRenderer.flipX = true;
                    _lookingRight = false;
                    _handsTransform.localScale = new Vector3(1, 1, 1);
                }
            }
        }
    }
}

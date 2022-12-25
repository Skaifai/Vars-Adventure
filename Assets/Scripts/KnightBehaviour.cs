using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBehaviour : MonoBehaviour
{
    // Pos and rot of the instantiated weapon
    private Vector3 swordPosition = new Vector3(0, 0, 0);
    private Quaternion swordRotation = new Quaternion(0, 0, 0, 0);

    public bool HasWeapon
    {
        get
        {
            return currentWeapon != null;
        }
    }

    [SerializeField]
    public GameObject currentWeapon;

    private Transform hands;
    private Transform rightHandTransform;

    private Animator animator;

    void Awake()
    {
        hands = gameObject.transform.Find("Hands");

        rightHandTransform = hands.transform.Find("Hands_2");

        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void EndAttack()
    {
        animator.SetBool("IsAttacking", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon") && HasWeapon == false)
        {
            swordPosition = new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, gameObject.transform.position.z);
            if (rightHandTransform != null)
            {
                currentWeapon = Instantiate(collision.gameObject, rightHandTransform);
                currentWeapon.transform.localPosition = new Vector3(0, 0);
            }
            else currentWeapon = Instantiate(collision.gameObject, swordPosition, swordRotation, gameObject.transform);
            collision.gameObject.SetActive(false);
        }
    }
}

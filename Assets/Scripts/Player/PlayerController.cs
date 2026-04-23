using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerData playerData;
    
    private float currentHP;
    private float speed;

    private PlayerInput playerInput;
    private Vector2 moveInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        currentHP = playerData.maxHP;
        speed = playerData.moveSpeed;
    }

    void Update()
    {
        if (playerInput == null) return;

        HandleMovement();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            TakeDamage(1f);
        }
    }

    void HandleMovement()
    {
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        
        float h = moveInput.x;
        float v = moveInput.y;

        Vector3 direction = new Vector3(h, v, 0);
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void TakeDamage(float damage)
    {
        currentHP -= damage;
        Debug.Log("Player HP: " + currentHP);

        if (currentHP <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }
}
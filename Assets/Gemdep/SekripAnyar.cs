using UnityEngine;
using UnityEngine.InputSystem;

public class SekripAnyar : MonoBehaviour
{

    // Movement
    public float speed = 1.0f;
    public Rigidbody2D rb;
    public InputActionReference input;
    [SerializeField]private Vector2 move;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        move = input.action.ReadValue<Vector2>();
        rb.linearVelocity = move * speed;

    }
}

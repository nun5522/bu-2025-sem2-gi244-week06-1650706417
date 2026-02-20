using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerExam03 : MonoBehaviour
{
    public float speed = 10;
    public float xRange = 15;
    public GameObject projectilePrefab;

    public bool enableAutoFireMode;
    public float autoFireInterval = 0.1f;
    private float autoFireTimer = 0f;

    private float horizontalInput;
    private InputAction moveAction;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        // Movement
        horizontalInput = moveAction.ReadValue<Vector2>().x;
        transform.Translate(horizontalInput * speed * Time.deltaTime * Vector3.right);

        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        if (enableAutoFireMode)
        {
            autoFireTimer += Time.deltaTime;

            if (autoFireTimer >= autoFireInterval)
            {
                autoFireTimer = 0f;
                Instantiate(projectilePrefab, transform.position, transform.rotation);
            }
        }
    }
}
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerExam05 : MonoBehaviour
{
    public float speed;
    public float xRange = 15;
    public GameObject projectilePrefab;


    // Exam 05 ...
    public int maxBulletCount = 10;
    public float bulletRegenerateCooldown = 1f;
    // ...
    private int currentBulletCount;
    private bool isRegenerating = false;
    private float regenTimer = 0f;


    private float horizontalInput;
    private InputAction moveAction;
    private InputAction shootAction;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Shoot");
    }

    // Update is called once per frame
    void Update()
    {
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

        if (shootAction.triggered)
        {
            if (currentBulletCount > 0)
            {
                Instantiate(projectilePrefab, transform.position, transform.rotation);
                currentBulletCount--;
                Debug.Log($"Bullet fired! Remaining: {currentBulletCount}/{maxBulletCount}");

                isRegenerating = false;
                regenTimer = 0f;
            }
            else
            {
                Debug.Log("Out of ammo! Reloading...");
            }
        }

        if (currentBulletCount < maxBulletCount)
        {
            if (!isRegenerating)
            {
                isRegenerating = true;
                regenTimer = 0f;
            }

            regenTimer += Time.deltaTime;

            if (regenTimer >= bulletRegenerateCooldown)
            {
                currentBulletCount = maxBulletCount;
                isRegenerating = false;
                regenTimer = 0f;
                Debug.Log("Bullets Reloaded");
            }
        }
    }
}

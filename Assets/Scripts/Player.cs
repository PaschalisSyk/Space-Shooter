using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [SerializeField] float moveSpeed = 10;
    [SerializeField] float padding = 1f;

    Coroutine firingCoroutine;

    Vector2 minBounds;
    Vector2 maxBounds;

    Vector2 rawInput;

    Shooter shooter;

    private void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //Fire();
        
    }

    private void Move()
    {
        /*var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);*/

        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + padding, maxBounds.x - padding);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + padding *4, maxBounds.y - padding * 10);
        transform.position = newPos;

    }

    private void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    private void OnFire(InputValue value)
    {
        if(shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        minBounds = gameCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = gameCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }
}

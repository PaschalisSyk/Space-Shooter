using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [ColorUsageAttribute(true, true)]
    Color color;

    [SerializeField] float moveSpeed = 10;
    [SerializeField] float padding = 1f;

    Coroutine firingCoroutine;

    Vector2 minBounds;
    Vector2 maxBounds;

    Vector2 rawInput;

    Vector3 touchPosition;
    Vector3 direction;

    Quaternion startRot;

    Shooter shooter;
    public bool shieldied;

    private float deltaX, deltaY;

    UIDisplay display;

    private float doubleTapThreshold = 0.3f;
    private int tapCount;

    private void Awake()
    {
        shooter = GetComponent<Shooter>();
        color = GetComponent<SpriteRenderer>().color;
        display = FindObjectOfType<UIDisplay>();

    }

    void Start()
    {
        StartCoroutine(StartAnim());
        startRot = transform.rotation;
        shieldied = false;
        SetUpMoveBoundaries();

    }

    void Update()
    {
        Move();
        Shielded();
        //Touch();
        EndLevelAnim();
        //FireInteraction();
        //Fire();
        
    }

    private void Move()
    {
        if (display.IsOn() || Time.timeSinceLevelLoad < 3)
        {
            return;
        }
        /*var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);*/

        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + padding, maxBounds.x - padding);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + padding * 3, maxBounds.y - padding * 10);
        transform.DOMove(newPos, 0.05f);

        MovementRotation();


    }

    private void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    private void OnFire(InputValue value)
    {
        //if(shooter != null)
        //{
        //    shooter.isFiring = value.isPressed;
        //}
    }

    //private void OnTouchPosition(InputValue value)
    //{
    //    rawInput = value.Get<Vector2>();
    //}

    //private void OnTouchPress(InputValue value)
    //{
    //    if (shooter != null)
    //    {
    //        shooter.isFiring = value.isPressed;
    //    }
    //}

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        minBounds = gameCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = gameCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void Shielded()
    {
        if (Keyboard.current[Key.Q].wasPressedThisFrame && !shieldied)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetComponentInChildren<Shield>().ShieldScale();
            shieldied = true;
        }
    }

    void Touch()
    {
        if (display.IsOn() || Time.timeSinceLevelLoad < 3)
        {
            return;
        }
        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);

            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            float movingTouchPos = touch.deltaPosition.x;

            if (Input.touchCount == 1)
            {
                if (Input.GetTouch(0).phase == UnityEngine.TouchPhase.Ended)
                {
                    tapCount++;
                    StartCoroutine(SingleOrDoubleTap());
                }
            }

            switch (touch.phase)
            {
                case UnityEngine.TouchPhase.Began:
                    deltaX = touchPos.x - transform.position.x;
                    deltaY = touchPos.y - transform.position.y;
                    break;

                case UnityEngine.TouchPhase.Moved:
                    Vector2 newPos = new Vector2();
                    newPos.x = Mathf.Clamp(touchPos.x - deltaX, minBounds.x + padding, maxBounds.x - padding);
                    newPos.y = Mathf.Clamp(touchPos.y - deltaY, minBounds.y + padding * 3, maxBounds.y - padding * 10);
                    transform.DOMove(newPos, 0.5f);

                    if (deltaX > 0)
                    {
                        transform.DORotate(new Vector3(0, 20, 0), 0.5f);

                    }
                    if (deltaX < 0)
                    {
                        transform.DORotate(new Vector3(0, -20, 0), 0.5f);
                    }

                    break;

                case UnityEngine.TouchPhase.Ended:

                    transform.DORotateQuaternion(startRot, 1f);

                    break;
            }


            if (touch.tapCount == 2 && !shieldied)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetComponentInChildren<Shield>().ShieldScale();
                shieldied = true;
            }

        }

    }



    void MovementRotation()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.DORotate(new Vector3(0, 20, 0), 1f);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.DORotate(new Vector3(0, -20, 0), 1f);
        }
        if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            transform.DORotateQuaternion(startRot, 1f);
        }
    }

    public void IsFiring()
    {
        shooter.isFiring = false;
    }

    IEnumerator StartAnim()
    {
        this.transform.DOMoveY(-5, 2.5f);
        shooter.isFiring = false;
        yield return new WaitForSeconds(2);
        shooter.isFiring = true;

    }

    void EndLevelAnim()
    {
        if(display.IsOn())
        {
            this.transform.position = Vector3.Slerp(this.transform.position, new Vector3(0, -6.5f, 0), Time.deltaTime * 0.4f);
        }
    }

    //void FireInteraction()
    //{
    //    if(shooter.TimeForNext() <= 0.25f)
    //    {
    //        gameObject.GetComponent<SpriteRenderer>().DOColor(Color.white, 0.1f).SetLink(gameObject);
    //    }
    //    else
    //    {
    //        gameObject.GetComponent<SpriteRenderer>().DOColor(color, 0.25f).SetLink(gameObject);
    //    }
    //}

    IEnumerator SingleOrDoubleTap()
    {
        yield return new WaitForSeconds(doubleTapThreshold);

        if (tapCount == 1)
        {
            Debug.Log("SingleTap");
            tapCount = 0;
        }
        else if (tapCount == 2 && !shieldied)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetComponentInChildren<Shield>().ShieldScale();
            shieldied = true;
            tapCount = 0;
        }

    }
}

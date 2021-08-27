using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Drag : MonoBehaviour
{
    public static event Action PuzzuleDone = delegate { };

    [SerializeField]
    private Transform standPosition;

    private Vector2 initialPosition;

    private Renderer rend;

    private float deltaX, deltaY;

    private bool moveAlowed;

    private bool locked;
    
    void Start()
    {
        initialPosition = transform.position;
        rend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && !locked)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                    {
                        moveAlowed = true;
                        rend.sortingOrder = 3;
                        deltaX = touchPos.x - transform.position.x;
                        deltaY = touchPos.y - transform.position.y;
                    }

                    break;

                case TouchPhase.Moved:

                    if (moveAlowed)
                        transform.position = new Vector2(touchPos.x - deltaX, touchPos.y - deltaY);

                    break;

                case TouchPhase.Ended:

                    moveAlowed = false;
                    rend.sortingOrder = 2;

                    if (Mathf.Abs(transform.position.x - standPosition.position.x) <= 1f &&
                        Mathf.Abs(transform.position.y - standPosition.position.y) <= 5f)
                    {
                        switch (PyramidControl.slotsOccupied)
                        {
                            case 0:
                                transform.position = new Vector2(standPosition.position.x, -3.15f);
                                PyramidControl.slotsOccupied = 1;
                                break;

                            case 1:
                                transform.position = new Vector2(standPosition.position.x, -1.5f);
                                PyramidControl.slotsOccupied = 2;
                                break;

                            case 2:
                                transform.position = new Vector2(standPosition.position.x, 0.15f);
                                PyramidControl.slotsOccupied = 3;
                                break;

                            case 3:
                                transform.position = new Vector2(standPosition.position.x, 1.7f);
                                PuzzuleDone();
                                break;
                        }

                        locked = true;
                    }
                    else
                        transform.position = new Vector2(initialPosition.x, initialPosition.y);
                    break;
            }
        }
    }
}

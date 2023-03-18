using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelSlider : MonoBehaviour
{
    public RectTransform panel;
    public float slideDuration = 0.5f;
    public Ease slideEase = Ease.OutQuad;
    public Animator animator;
    private Vector2 slideInPosition;
    private Vector2 slideOutPosition;
    private bool isSliding = false;
    private SpaceshipSelector spaceshipSelector;
    private Vector2 startTouchPos;
    private Vector2 endTouchPos;

    private void Awake()
    {
        spaceshipSelector = FindObjectOfType<SpaceshipSelector>();
        animator.SetInteger("CurrentIndex", 0);
    }

    private void Update()
    {
        Swipe();
    }


    public void SlideLeft()
    {
        slideInPosition = new Vector2(panel.anchoredPosition.x - 225, 0);
        if (panel.anchoredPosition.x <= 0) return;

        if (isSliding) return;
        isSliding = true;
        animator.SetInteger("CurrentIndex", 1);
        panel.DOAnchorPos(slideInPosition, slideDuration).SetEase(slideEase).OnComplete(() => isSliding = false);
    }

    public void SlideRight()
    {
        slideOutPosition = new Vector2(panel.anchoredPosition.x + 225, 0);
        if (panel.anchoredPosition.x >= 225) return; 

        if (isSliding) return;
        isSliding = true;
        animator.SetInteger("CurrentIndex", 0);
        panel.DOAnchorPos(slideOutPosition, slideDuration).SetEase(slideEase).OnComplete(() => isSliding = false);
    }

    void Swipe()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPos = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPos = Input.GetTouch(0).position;

            if (endTouchPos.x < startTouchPos.x)
            {
                SlideLeft();
            }
            if (endTouchPos.x > startTouchPos.x)
            {
                SlideRight();
            }
        }
    }
}

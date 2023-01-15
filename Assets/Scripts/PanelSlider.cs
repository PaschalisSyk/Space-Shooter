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

    private void Awake()
    {
        spaceshipSelector = FindObjectOfType<SpaceshipSelector>();
    }


    public void SlideLeft()
    {
        slideInPosition = new Vector2(panel.anchoredPosition.x - 200, 0);
        if (panel.anchoredPosition.x <= 200) return;

        if (isSliding) return;
        isSliding = true;
        animator.SetInteger("CurrentIndex", 1);
        panel.DOAnchorPos(slideInPosition, slideDuration).SetEase(slideEase).OnComplete(() => isSliding = false);
    }

    public void SliderRight()
    {
        slideOutPosition = new Vector2(panel.anchoredPosition.x + 200, 0);
        if (panel.anchoredPosition.x >= 400) return; 

        if (isSliding) return;
        isSliding = true;
        animator.SetInteger("CurrentIndex", 0);
        panel.DOAnchorPos(slideOutPosition, slideDuration).SetEase(slideEase).OnComplete(() => isSliding = false);
    }
}

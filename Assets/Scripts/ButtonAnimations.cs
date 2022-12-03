using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonAnimations : MonoBehaviour
{
    public void OnButtonClick()
    {
        GameObject children = transform.GetChild(1).gameObject;
        Color color = children.GetComponent<Image>().color;

        GetComponent<Image>().DOColor(color, 0.5f).SetLink(gameObject);
        GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 360, 0), 0.15f,RotateMode.FastBeyond360).SetEase(Ease.InFlash).SetLoops(3).OnComplete(
            () => GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), 0.15f)).OnComplete(
            () => transform.DOMove(new Vector3(0, -6.5f, 0), 2).SetEase(Ease.InSine)
            .SetLink(gameObject));
        transform.DOScale(1.3f, 0.25f).SetEase(Ease.InFlash).SetLoops(1).OnComplete(() => transform.DOScale(0,3f).SetLink(gameObject));
    }
}

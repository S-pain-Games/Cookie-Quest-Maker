using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;


[RequireComponent(typeof(Button))]
public class CloseBakeryButtonAnimations : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private void OnEnable()
    {
        IdleAnimation();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(1.8f, 0.3f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOKill();
        var rot = transform.rotation;
        transform.DORotate(new Vector3(0, 0, -15.0f), 0.3f).SetEase(Ease.InOutSine).OnComplete(() => transform.DORotate(new Vector3(0, 0, 15.0f), 0.3f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IdleAnimation();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(1.0f, 0.3f);
    }

    public void IdleAnimation()
    {
        transform.DOKill();
        transform.DORotate(new Vector3(0, 0, 0.0f), 0.3f);
        transform.DOScale(1.3f, 0.3f).SetLoops(-1, LoopType.Yoyo);
    }
}

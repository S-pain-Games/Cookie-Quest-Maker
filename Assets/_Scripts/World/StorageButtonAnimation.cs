using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

[RequireComponent(typeof(Button))]
public class StorageButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private void OnEnable()
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(1.8f, 0.3f);
        transform.DORotate(new Vector3(0, 0, 0f), 0.3f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(1.2f, 0.3f).SetLoops(-1, LoopType.Yoyo);
        transform.DORotate(new Vector3(0, 0, 25f), 0.3f).OnComplete(
            () => transform.DORotate(new Vector3(0, 0, -25f), 0.3f).SetEase(Ease.InOutSine).ChangeStartValue(new Vector3(0, 0, 25f)).SetLoops(-1, LoopType.Yoyo)
            );
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(1.0f, 0.3f);
        transform.DOShakeRotation(0.2f);
        transform.DORotate(new Vector3(0, 0, 0f), 0.3f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(1.0f, 0.3f);
        transform.DORotate(new Vector3(0, 0, 0f), 0.3f);
    }
}

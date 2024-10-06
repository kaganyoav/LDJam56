using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonScale : MonoBehaviour
{
    [SerializeField] private float originalScale = 1f;
    [SerializeField] private float toScale;
    [SerializeField] private float duration;

    public void HandleSelect()
    {
        transform.DOScale(toScale,duration).SetEase(Ease.InOutSine).SetUpdate(true);
    }

    public void HandleDeselect()
    {
        transform.DOScale(originalScale,duration).SetEase(Ease.InOutSine).SetUpdate(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Unity.VisualScripting;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Vector3 hidePos;
    [SerializeField] float transitionDuration = 1f;
    [SerializeField] Vector3 showPos;
    [SerializeField] bool hideOnEnable = true;

    void OnEnable()
    {
        if(hideOnEnable) 
        {
            image.rectTransform.DOLocalMove(showPos,0.0001f).SetEase(Ease.Linear);
            Hide();
        }
    }

    [ContextMenu("Show")]
    public void Show()
    {
        image.rectTransform.DOLocalMove(showPos,transitionDuration).SetEase(Ease.Linear);
    }

    [ContextMenu("Hide")]
    public void Hide()
    {
        image.rectTransform.DOLocalMove(hidePos,transitionDuration).SetEase(Ease.Linear);;
    }
}

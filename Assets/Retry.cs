using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Retry : MonoBehaviour
{
    [SerializeField] Image image;

    void OnEnable()
    {
        GameManager.instance.showR.AddListener(ShowR);
    }

    void OnDisable()
    {
        GameManager.instance.showR.RemoveListener(ShowR);
    }

    public void ShowR()
    {
        image.DOFade(0.7f,8f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class OptionsManager : MonoBehaviour
{
    [SerializeField] GameObject optionsGameObject;
    [SerializeField] float showDuration = 1f;
    [SerializeField] Vector3 showPos = Vector3.zero;
    [SerializeField] Vector3 hidePos = new Vector3(0,-1080,0);

    public void Pause()
    {
        optionsGameObject.transform.DOLocalMove(showPos,showDuration).SetEase(Ease.Linear);
    }

    public void Resume()
    {
        optionsGameObject.transform.DOLocalMove(hidePos,showDuration).SetEase(Ease.Linear);
    }

    public void RestartGame()
    {
        GameManager.instance.ResetGame();
    }

    public void Quit()
    {
        Application.Quit();
    }
}

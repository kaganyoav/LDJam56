using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] CamZoom camZoom;
    [SerializeField] GameObject buttonGameObject;
    [SerializeField] LevelTransition levelTransition;

    public void StartGame()
    {
        camZoom.ZoomOut();
        buttonGameObject.SetActive(false);
        StartCoroutine(LoadLevelCo());
    }

    IEnumerator LoadLevelCo()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
    }

    public void ReturnToMenu()
    {
        StartCoroutine(ReturnToMenuCo());
    }

    IEnumerator ReturnToMenuCo()
    {
        levelTransition.Show();
        yield return new WaitForSeconds(1f);
        GameManager.instance.ResetGame();
    }

    public void Quit()
    {
        Application.Quit();
    }
}

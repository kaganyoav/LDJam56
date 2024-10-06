using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

    [SerializeField] private int curLevel = 1;
    [SerializeField] private int levelAmount;

    public bool knowsR = false;
    [HideInInspector] public UnityEvent showR;

	void Awake ()
	{
		if (instance == null) 
        {
			instance = this;
            DontDestroyOnLoad(gameObject);
		}
        else
        {
            Destroy(gameObject);
        }
	}

    public void LevelWon()
    {
        StartCoroutine(StartLevelCo());        
    }

    IEnumerator StartLevelCo()
    {
        Debug.Log("loading next level");
        yield return new WaitForSeconds(0f);
        if(curLevel < levelAmount)
        {
            curLevel++;
            SceneManager.LoadScene(curLevel);
        }
        else Debug.Log("end");
    }

    public void ResetGame()
    {
        curLevel = 1;
        SceneManager.LoadScene(0);
    }

    public void ShowR()
    {
        if(!knowsR) showR.Invoke();
    }
}
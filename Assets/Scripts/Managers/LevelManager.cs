using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{   
    [SerializeField] Sentence sentence;
    [SerializeField] Vector3 spawnPoint;
    [SerializeField] GameObject commaPrefab;
    [SerializeField] LevelTransition levelTransition;
    private Comma curComma;
    bool levelCompletedBool = false;
    public int levelID;
    bool debug = false;

    void OnEnable()
    {
        sentence.placedOne.AddListener(SpawnComma);
        sentence.levelComplete.AddListener(LevelComplete);
        SpawnComma();
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && !levelCompletedBool)
        {
            GameManager.instance.knowsR = true;
            ResetPosition();
        }

        if(Input.GetKeyDown(KeyCode.D) && debug)
        {
            LevelComplete();
        }
    }

    void SpawnComma()
    {
        curComma = Instantiate(commaPrefab,spawnPoint,Quaternion.identity,null).GetComponent<Comma>();
    }

    public void ResetPosition()
    {
        curComma.ResetPosition();
    }

    public void LevelComplete()
    {
        curComma.levelComplete = true;
        levelCompletedBool = true;
        StartCoroutine(LevelCompleteCo());
    }

    IEnumerator LevelCompleteCo()
    {
        curComma.WinLevel();
        yield return new WaitForSeconds(2f);
        levelTransition.Show();
        yield return new WaitForSeconds(1f);
        GameManager.instance.LevelWon();
    }
}

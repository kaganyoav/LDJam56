using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sentence : MonoBehaviour
{
    [SerializeField] public List<Place> places;
    private int placedNum = 0;
    
    [Header("Events")]
    [HideInInspector] public UnityEvent placedOne;
    [HideInInspector] public UnityEvent levelComplete;

    void OnEnable()
    {
        for(int i=0;i<places.Count;i++)
        {
            places[i].placed.AddListener(AddPoint);
        }
    }

    void OnDisable()
    {
        for(int i=0;i<places.Count;i++)
        {
            places[i].placed.RemoveListener(AddPoint);
        }
    }
    
    public void AddPoint()
    {
        placedNum++;
        Debug.Log(placedNum);
        if(placedNum >= places.Count)
        {
            levelComplete.Invoke();
        }
        else placedOne.Invoke();
    }
    
}

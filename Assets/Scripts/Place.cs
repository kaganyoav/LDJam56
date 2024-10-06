using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Place : MonoBehaviour
{
    [Header("Events")]
    [HideInInspector] public UnityEvent placed;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        placed.Invoke();
        Destroy(gameObject);
    }
}


using System.Collections;
using System.ComponentModel;
using System.Data;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class Comma : MonoBehaviour
{
	[HideInInspector] public Rigidbody2D rb;
	[HideInInspector] public PolygonCollider2D col;
    [HideInInspector] public CircleCollider2D clickCol;
    [HideInInspector] public TrailRenderer tr;
    [HideInInspector] public Animator anim;

	[HideInInspector] public Vector3 pos { get { return transform.position; } }
    [SerializeField] float pushForce = 4f;
    [SerializeField] float maxMagnitude = 25;
    [SerializeField] float minSpeed;
    [SerializeField] float notMovingResetDuration;
    bool canShoot = true;
    bool dying = false;
    public bool levelComplete = false;


    Vector2 spawnPoint;
    Vector2 startPoint;
	Vector2 endPoint;
	Vector2 direction;
	Vector2 force;
	float distance;
    
    Camera cam;
    public Trajectory trajectory;

    //Animation
    [SerializeField] AnimationClip disappearAnim;

    //SOUND
    [SerializeField] private AudioMixerGroup output;
    [SerializeField] AudioClip[] weeSounds;
    [SerializeField] AudioClip[] yaySounds;

    [SerializeField] AudioClip[] ohSounds;

	void Awake ()
	{
		rb = GetComponent<Rigidbody2D> ();
		col = GetComponent<PolygonCollider2D>();
        clickCol = GetComponent<CircleCollider2D>();
        tr = GetComponent<TrailRenderer> ();
        anim = GetComponent<Animator> ();
        cam = Camera.main;
        spawnPoint = transform.position;
        DesactivateRb();
	}

    void Update()
    {
        if(!canShoot && rb.velocity.magnitude < minSpeed && !dying)
        {
            dying = true;
            StartCoroutine(NotMovingCo());
        }
    }

    IEnumerator NotMovingCo()
    {        
        yield return new WaitForSeconds(notMovingResetDuration);
        if(!levelComplete) SoundManager.instance.PlayRandomAudioClip(ohSounds,output,transform);
        anim.SetTrigger("Disappear");
        yield return new WaitForSeconds(disappearAnim.length);
        GameManager.instance.ShowR();
        ResetPosition();
    }

    void OnMouseDown()
    { 
        if(!canShoot) return;
        DesactivateRb();
        startPoint = transform.position;
        trajectory.Show();
    }
    void OnMouseDrag()
    {
        if(!canShoot) return;
        endPoint = cam.ScreenToWorldPoint (Input.mousePosition);
		distance = Vector2.Distance (startPoint, endPoint);
		direction = (startPoint - endPoint).normalized;
		force = direction * distance * pushForce;

        if(force.magnitude > maxMagnitude)
        {
            force = force.normalized * maxMagnitude;
        }
		trajectory.UpdateDots (startPoint, force);
    }
    void OnMouseUp()
    {
        if(!canShoot) return;
        SoundManager.instance.PlayRandomAudioClip(weeSounds,output,transform);
        canShoot = false;
        ActivateRb();
        Push(force);
        trajectory.Hide();
    }

	public void Push (Vector2 force)
	{
		rb.AddForce(force, ForceMode2D.Impulse);
	}

	public void ActivateRb ()
	{
		rb.isKinematic = false;
        tr.enabled = true;
        clickCol.enabled = false;
	}

	public void DesactivateRb ()
	{
		rb.velocity = Vector3.zero;
		rb.angularVelocity = 0f;
		rb.isKinematic = true;
	}

    public void ResetPosition()
    {
        if(levelComplete) return;
        dying = false;
        tr.enabled = false;
        StopAllCoroutines();
        DesactivateRb();
        canShoot = true;
        transform.SetPositionAndRotation(spawnPoint, Quaternion.identity);
        anim.SetTrigger("Appear");
        clickCol.enabled = true;
    }

    [ContextMenu("StraightenUp")]
    public void StraightenUp()
    {
        DesactivateRb();
    }

    public void WinLevel()
    {
        anim.SetTrigger("Win");
        SoundManager.instance.PlayRandomAudioClip(yaySounds,output,transform);
    }

    public void Dissapear()
    {
        anim.SetTrigger("Disppear");
    }

    // void OnCollisionEnter2D(Collision2D col)
    // {
    //     SoundManager.instance.PlayRandomAudioClip(bangSounds,output,transform);
    // }
}
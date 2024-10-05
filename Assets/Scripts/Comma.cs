
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class Comma : MonoBehaviour
{
	[HideInInspector] public Rigidbody2D rb;
	[HideInInspector] public CircleCollider2D col;

	[HideInInspector] public Vector3 pos { get { return transform.position; } }
    [SerializeField] float pushForce = 4f;
    [SerializeField] float maxMagnitude = 10000000;


    Vector2 spawnPoint;
    Vector2 startPoint;
	Vector2 endPoint;
	Vector2 direction;
	Vector2 force;
	float distance;
    
    Camera cam;
    public Trajectory trajectory;

    [SerializeField] bool grounded = false;

	void Awake ()
	{
		rb = GetComponent<Rigidbody2D> ();
		col = GetComponent<CircleCollider2D> ();
        cam = Camera.main;
        spawnPoint = transform.position;
        DesactivateRb();
	}

    void Update()
    {
        // Debug.Log(rb.velocity.magnitude);
        // if(!grounded && rb.velocity.magnitude < 0.001f) StraightenUp();
        // if(grounded)
        // {
        //     transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * 5f);
        // }
    }

    void OnMouseDown()
    {
        DesactivateRb();
        startPoint = transform.position;
        trajectory.Show ();
    }
    void OnMouseDrag()
    {
        endPoint = cam.ScreenToWorldPoint (Input.mousePosition);
		distance = Vector2.Distance (startPoint, endPoint);
		direction = (startPoint - endPoint).normalized;
		force = direction * distance * pushForce;
        // Debug.Log(force.magnitude);
        if(force.magnitude > maxMagnitude)
        {
            force = force.normalized * maxMagnitude;
        }
		trajectory.UpdateDots (startPoint, force);
    }
    void OnMouseUp()
    {
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
	}

	public void DesactivateRb ()
	{
		rb.velocity = Vector3.zero;
		rb.angularVelocity = 0f;
		rb.isKinematic = true;
	}

    public void ResetPosition()
    {
        DesactivateRb();
        transform.position = spawnPoint;
        transform.rotation = Quaternion.identity;
    }

    [ContextMenu("StraightenUp")]
    public void StraightenUp()
    {
        DesactivateRb();
        grounded = true;
    }
}
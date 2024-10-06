using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
	[SerializeField] int dotsNumber;
	[SerializeField] GameObject dotsParent;
	[SerializeField] GameObject dotPrefab;
	[SerializeField] float dotSpacing;
	[SerializeField] float dotMinScale = 0.01f;
	[SerializeField] float dotMaxScale = 0.3f;
	[SerializeField] float commaGravity = 0.5f;
	Transform[] dotsList;

	Vector2 pos;
	float timeStamp;

	void Awake()
	{
		commaGravity = transform.parent.GetComponent<Rigidbody2D>().gravityScale;
	}

	void Start ()
	{
		Hide ();
		PrepareDots ();
	}

	void PrepareDots ()
	{
		dotsList = new Transform[dotsNumber];
		dotPrefab.transform.localScale = Vector3.one * dotMaxScale;

		float scale = dotMaxScale;
		float scaleFactor = scale / dotsNumber;

		for (int i = 0; i < dotsNumber; i++)
		{
			dotsList[i] = Instantiate(dotPrefab, dotsParent.transform).transform;
			dotsList[i].localScale = Vector3.one * scale;

			//set the alpha based on dot index
			SpriteRenderer sr = dotsList[i].GetComponent<SpriteRenderer>();
			if (sr != null)
			{
				float alpha = Mathf.Lerp(1f, 0.1f, (float)i / dotsNumber);
				sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
			}
			if (scale > dotMinScale)
				scale -= scaleFactor;
		}
	}

	public void UpdateDots (Vector3 originPos, Vector2 forceApplied)
	{
		timeStamp = dotSpacing;
		for (int i = 0; i < dotsNumber; i++) {
			pos.x = (originPos.x + forceApplied.x * timeStamp);
			pos.y = (originPos.y + forceApplied.y * timeStamp) - (Physics2D.gravity.magnitude*commaGravity * timeStamp * timeStamp) / 2f;
			dotsList [i].position = pos;
			timeStamp += dotSpacing;
		}
	}

	public void Show ()
	{
		dotsParent.SetActive (true);
	}

	public void Hide ()
	{
		dotsParent.SetActive (false);
	}

}
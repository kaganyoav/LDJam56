using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

    [SerializeField] private Comma comma;

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		}
	}

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            comma.ResetPosition();
        }
    }
}
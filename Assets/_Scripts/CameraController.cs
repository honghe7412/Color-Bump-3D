using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
	[SerializeField]
    private GameObject bgObj;
    private Vector3 bgDistanceoffuse;
	public static CameraController Instance;

	private string sceneName;
	void Awake()
	{
		if(Instance==null)
			Instance=this;
	}
	// store a public reference to the Player game object, so we can refer to it's Transform
	public GameObject player;

	// Store a Vector3 offset from the player (a distance to place the camera from the player at all times)
    [HideInInspector]
	public Vector3 offset;

	[SerializeField]
	private float smoothing=2.0f;
	// At the start of the game..
	public void Start ()
	{
		sceneName=SceneManager.GetActiveScene().name;

        //if(sceneName=="Endless_Mode")
        {
            bgDistanceoffuse=bgObj.transform.position-transform.position;
        }
		
		// Create an offset by subtracting the Camera's position from the player's position
		offset = transform.position - player.transform.position;
	}

	// After the standard 'Update()' loop runs, and just before each frame is rendered..
	void FixedUpdate ()
	{
		Vector3 endPosition=new Vector3(0,player.transform.position.y,player.transform.position.z)+new Vector3(0,offset.y,offset.z);
        // Set the position of the Camera (the game object this script is attached to)
        // to the player's position, plus the offset amount
        transform.position = Vector3.Lerp(transform.position,endPosition,smoothing*Time.deltaTime);
		
		//if(sceneName=="Endless_Mode")
			bgObj.transform.position=transform.position+bgDistanceoffuse;
	}
}
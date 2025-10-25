using UnityEngine;

[RequireComponent(typeof(Light))]
public class WorldSunController : MonoBehaviour
{
    public WorldClock clock;
    
    private Light sun;
    private Vector3 rot;

	public void Start ()
	{
		sun = GetComponent<Light>();
	}

	void Update ()
    {
        var time = clock.GetTime().AsSeconds();
        sun.transform.rotation = Quaternion.Euler((360f / GameTime.MAX_TIME_SECONDS) * time - 90, 0, 0);
        rot = new Vector3 ((360f / GameTime.MAX_TIME_SECONDS) * time - 90, 0, 0);
    }
}

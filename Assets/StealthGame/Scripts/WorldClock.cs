using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public class WorldClock : NetworkBehaviour
{
    public float minutesPerSecond = 60;

    [SerializeField] GameTime startTime;

	// REMINDER SyncVar does not create an instance of <T> so add a valid one on awake
    // REMINDER Write custom serializer for custom types
	public readonly SyncVar<GameTime> gt = new SyncVar<GameTime>(new SyncTypeSettings(WritePermission.ClientUnsynchronized, ReadPermission.ExcludeOwner));

	public void Awake ()
	{
        gt.Value = new GameTime(startTime);
	}

	private float secondCount = 0;
    void Update()
    {
        secondCount += Time.deltaTime;
        if (secondCount > 0.02)
        {
            float time = secondCount * minutesPerSecond;
            AddTime(new GameTime(0, (int)(time / 60), (int)(time % 60)));
            secondCount = 0;
        }
    }

    [ServerRpc(RunLocally = true)]
    public void SetTime(GameTime gameTime)
    {
        this.gt.Value.SetTime(gameTime.AsSeconds());
    }

    public GameTime GetTime()
    {
        if (this.gt.Value is null) return new GameTime();
        return this.gt.Value.Clone();
    }

    [ServerRpc(RunLocally = true)]
    public void AddTime(GameTime gameTime)
    {
       this.gt.Value += gameTime;
    }
}

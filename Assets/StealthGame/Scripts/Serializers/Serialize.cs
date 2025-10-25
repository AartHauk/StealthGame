using FishNet.Serializing;

public static class Serialize
{

	public static void WriteGameTime(this Writer writer, GameTime gameTime)
	{
		writer.WriteInt32(gameTime.AsSeconds());
	}

	public static GameTime ReadGameTime(this Reader reader)
	{
		return new GameTime(reader.ReadInt32());
	}

}

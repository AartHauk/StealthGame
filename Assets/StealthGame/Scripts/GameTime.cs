using UnityEngine;
using System;

[Serializable]
public class GameTime : IComparable<GameTime>
{
    public const int MAX_TIME_SECONDS = 24 * 3600;

	[SerializeField] private int time = 0;
	public int Hours
    {
        get
        {
            return this[0];
        }
        set {
            this[0] = value;
        }
    }
    public int Minutes
    {
        get 
        {
            return this[1];
        }
        set
        {
            this[1] = value;
        }
    }
    public int Seconds
    {
        get 
        {
            return this[2];
        }
        set
        {
            this[2] = value;
        }
    }

    public GameTime () { }

    public GameTime (int seconds)
    {
        SetTime (seconds);
    }

    public GameTime(GameTime gameTime)
    {
        SetTime(gameTime.time);
    }

    public GameTime(int hours, int minutes, int seconds)
    {
        SetTime(hours, minutes, seconds);
    }

    public GameTime(int[] time)
    {
        if (time.Length == 0) return;
        else if (time.Length == 1) {
            SetTime(time[0], 0, 0);
        } else if (time.Length == 2) {
            SetTime(time[0], time[1], 0);
        } else {
            SetTime(time[0], time[1], time[2]);
        }
    }

    public static GameTime operator +(GameTime a, GameTime b)
    {
        return new GameTime(a.time + b.time);
    }

	public static GameTime operator + (GameTime a, int b)
	{
		return new GameTime(a.time + b);
	}

	public static GameTime operator + (int a, GameTime b)
	{
		return new GameTime(a + b.time);
	}

	public static GameTime operator -(GameTime a, GameTime b)
    {
        return new GameTime(a.time - b.time);
    }

	public static GameTime operator - (GameTime a, int b)
	{
		return new GameTime(a.time - b);
	}

	public static GameTime operator - (int a, GameTime b)
	{
		return new GameTime(a - b.time);
	}

	public int this[int i]
    {
        get
        {
            switch (i)
            {
                case 0: return this.time / 3600;
                case 1: return (this.time / 60) % 60;
                case 2: return this.time % 60;
                default: return 0;
            }
        }

        set
        {
            switch (i)
            {
                case 0:
                    SetTime(value * 3600);
                    break;
                case 1:
                    SetTime(value * 60);
                    break;
                case 2:
                    SetTime(value);
                    break;
                default:
                    break;
            }
        }
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var b = obj as GameTime;
        return this == b;
    }

    public override int GetHashCode()
    {
        return this.time;
    }

    public static bool operator ==(GameTime a, GameTime b)
    {
        if (a is null)
        {
            return b is null;
        }

		if (b is null)
		{
			return a is null;
		}
		return a.time == b.time;
    }

	public static bool operator !=(GameTime a, GameTime b)
    {
        return a.time != b.time;
    }

    public static bool operator >(GameTime a, GameTime b)
    {
        return a.time > b.time;
    }

    public static bool operator <(GameTime a, GameTime b)
    {
        return a.time < b.time;
    }

    public static bool operator <=(GameTime a, GameTime b)
    {
        return a.time <= b.time;
    }

    public static bool operator >=(GameTime a, GameTime b)
    {
        return a.time >= b.time;
    }

    // >0   Given time is earlier than current time
    // =0   Given time is same as current time
    // <0   Given time is later than current time
    public int CompareTime(GameTime b)
    {
        return this.GetHashCode() - b.GetHashCode();
    }

    public void SetTime(int hours, int minutes, int seconds)
    {
        SetTime(hours * 3600 + minutes * 60 + seconds);
    }

    public void SetTime(int seconds)
    {
        this.time = seconds % MAX_TIME_SECONDS;
    }

    public GameTime Clone()
    {
        return new GameTime(this);
    }

    public override string ToString()
    {
        return string.Format("{0:00}:{1:00}:{2:00}", this.Hours, this.Minutes, this.Seconds);
    }

    public int AsSeconds ()
    {
        return this.time;
    }

    public float AsMinutes ()
    {
        return this.time / 60f;
    }

    public float AsHours ()
    {
        return this.time / 3600f;
    }

	public int CompareTo (GameTime other)
	{
		return this.time.CompareTo(other.time);
	}
}

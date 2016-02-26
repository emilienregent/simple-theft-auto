using UnityEngine;

namespace Simple.CustomType
{
	public class Timer
	{
		private float _time = 0f;
		private float _duration = 0f;

		public float time { get { return _time; } }
		public float duration { get { return _duration; } }

		public bool isFinished { get { return Time.time - _time > _duration; } }

		public Timer(float duration)
		{
			_time = Time.time;
			_duration = duration;
		}
	}
}
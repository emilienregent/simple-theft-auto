namespace Simple.CustomType
{
	public enum InputSourceType
	{
		KEYBOARD,
		GAMEPAD
	}

	public class InputSource
	{
		private InputSourceType _type = InputSourceType.KEYBOARD;
        private int _id = -1;

        public InputSourceType type { get { return _type; } }
        public int id { get { return _id; } set { _id = value; } }

		public InputSource(InputSourceType type)
		{
			_type = type;
		}
    }
}
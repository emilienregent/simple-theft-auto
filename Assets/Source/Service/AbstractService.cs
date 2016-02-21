using Simple.CustomType;
using Simple.Context;

namespace Simple.Service
{
    public abstract class AbstractService
	{
        protected   AbstractContext _context = null;
		protected	bool    _isPaused	= false;
		public		bool    isPaused	{ get { return _isPaused; } }

		public abstract void Initialize();

		public virtual void Start()
		{
		}

		public virtual void Update()
		{
		}

		public virtual void Stop()
		{
		}

		public virtual void Pause()
		{
			_isPaused = true;
		}

		public virtual void Resume()
		{
			_isPaused = false;
		}

        public virtual void BindContext(AbstractContext context)
        {
            _context = context;   
        }
	}
}
using Simple.Service;
using System.Collections.Generic;
using UnityEngine;
using System;
using Simple.CustomType;

namespace Simple.Context
{
	public class AbstractContext : MonoBehaviour
	{
		private List<AbstractService>                       _services           = new List<AbstractService>();
        private Dictionary<Type, int>                       _typeToService      = new Dictionary<Type, int>();
        private Dictionary<Type, Action<AbstractService>>   _serviceToCallback  = new Dictionary<Type, Action<AbstractService>>();

        private void Awake()
        {
            Initialize();
        }

        public virtual void Initialize()
        {
        }

		private void Start()
		{
			BindServices();
			InitializeServices();

            StartServices();
		}

		public virtual void BindServices()
		{
		}

		private void InitializeServices()
		{
			for(int i = 0; i < _services.Count; i++)
			{
				_services[i].Initialize();
			}
		}

        private void StartServices()
        {
            for(int i = 0; i < _services.Count; i++)
            {
                _services[i].Start();
            }
        }

		private void Update()
		{
			for (int i = 0; i < _services.Count; i++)
			{
				if(_services[i].isPaused == false)
				{
					_services[i].Update();
				}
			}
		}

        protected void RegisterService<T>(AbstractService service)
        {
            if (_typeToService.ContainsKey(typeof(T)) == true)
            {
                throw new UnityException("Service of type " + typeof(T) + " is already registered");
            }

            service.BindContext(this);

            _services.Add(service);
            _typeToService.Add(typeof(T), _services.Count - 1);

            if (_serviceToCallback.ContainsKey(typeof(T)) == true)
            {
                _serviceToCallback[typeof(T)](service);
                _serviceToCallback.Remove(typeof(T));
            }
        }

        public T GetService<T>(Action<AbstractService> callback = null) where T : AbstractService
        {
            if (_typeToService.ContainsKey(typeof(T)) == true)
            {
                int index = _typeToService[typeof(T)];

                if (index < _services.Count)
                {
                    return (T)_services[index];
                }
            }

            if (callback != null)
            {
                _serviceToCallback.Add(typeof(T), callback);

                return null;
            }
            else
            {
                throw new UnityException("Can't find service of type " + typeof(T));
            }
        }
	}
}
using UnityEngine;
using Simple.CustomType;

namespace Simple.Behaviour
{
	/// <summary>
	/// Manage an instance of weapon
	/// Need to inherit MonoBehaviour to have editor edition
	/// </summary>
	public class Weapon : MonoBehaviour
    {
        [SerializeField]
        private WeaponType _type = WeaponType.NONE;
        [SerializeField]
        private GameObject _prefab = null;

		private int _ammo = 0;
		private int _ammoInClip = 0;

		[SerializeField]
		private int _ammoMax = 0;
		[SerializeField]
		private int _ammoByClip = 0;

		[Range(0f,1f)]
        [SerializeField]
        private float _aimingAngle = 0f;

        public WeaponType type { get { return _type; } }
        public GameObject prefab { get { return _prefab; } }

		public int ammoMax { get { return _ammoMax; }}
		public int ammoByClip { get { return _ammoByClip; }}
		public int ammo { get { return _ammo; } set { _ammo = Mathf.Clamp(value, 0, _ammoMax); } }
		public int ammoInClip { get { return _ammoInClip; } set { _ammoInClip = Mathf.Clamp(value, 0, _ammoByClip); } }

		public float aimingHeadAngle { get { return -_aimingAngle; } }
        public float aimingBodyAngle { get { return _aimingAngle; } }
    }
}
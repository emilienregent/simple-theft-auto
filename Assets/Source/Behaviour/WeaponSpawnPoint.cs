using Simple.CustomType;
using UnityEngine;

namespace Simple.Behaviour
{
	/// <summary>
	/// Handle behaviour of weapon spawn point
	/// </summary>
    public class WeaponSpawnPoint : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _weapons = new GameObject[0];
		[Range(0f, 60f)]
		[SerializeField]
		private float _cooldown = 0f;

		private GameObject _body = null;
        private Weapon _weapon = null;
		private Timer _timer = null;

        public GameObject body { get { return _body; } }
        public Weapon weapon { get { return _weapon; } }

		/// <summary>
		/// Initialize this monobehaviour
		/// </summary>
        private void Awake()
        {
            CreateWeapon();
        }

		/// <summary>
		/// Update this monobehaviour
		/// </summary>
		private void Update()
		{
			if(_timer != null && _timer.isFinished == true)
			{
				CreateWeapon();

				_timer = null;
			}
		}

		/// <summary>
		/// Create a new weapon for the spawn point
		/// </summary>
		/// <param name="prefab">The prefab used to create the weapon</param>
		/// <returns>The gameobject created</returns>
        public GameObject CreateWeapon(GameObject prefab = null)
        {
            if (prefab == null)
            {
                prefab = _weapons[Random.Range(0, _weapons.Length)];
            }

            if (_body != null)
            {
                GameObject.Destroy(_body);
            }

            _body = GameObject.Instantiate(prefab);

            _body.transform.position = transform.position;
            _body.transform.SetParent(transform);

            _weapon = _body.GetComponent<Weapon>();

            return _body;
        }

		/// <summary>
		/// Grab the current weapon for a specific player
		/// </summary>
		/// <param name="player">The player who grabbed the weapon</param>
		private void GrabWeapon(PlayerGame player)
		{
			player.AddWeapon(_weapon);

			GameObject.Destroy(_body);

			_body = null;
			_weapon = null;

			_timer = new Timer(_cooldown);
		}

		/// <summary>
		/// Detect when something collide with this trigger
		/// </summary>
		/// <param name="other">The other collider which collides</param>
        private void OnTriggerEnter(Collider other) {
            if (_weapon != null)
            {
                PlayerGame player = other.GetComponent<PlayerGame>();

               if(player != null && player)
				{
					GrabWeapon(player);
				}
            }
        }

		/// <summary>
		/// Draw a gizmo to see spawn points on scene
		/// </summary>
        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawCube(transform.position, new Vector3(1f, 2f, 1f));
        }
    }
}
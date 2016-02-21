using UnityEngine;

namespace Simple.Behaviour
{
    public class WeaponSpawnPoint : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _weapons = new GameObject[0];

        private GameObject _body = null;
        private Weapon _weapon = null;

        public GameObject body { get { return _body; } }
        public Weapon weapon { get { return _weapon; } }

        private void Awake()
        {
            CreateWeapon();
        }

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

        private void OnTriggerEnter(Collider other) {
            if (_weapon != null)
            {
                PlayerGame player = other.GetComponent<PlayerGame>();

                player.AddWeapon(_weapon);

                GameObject.Destroy(_body);

                _body = null;
                _weapon = null;
            }
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawCube(transform.position, new Vector3(1f, 2f, 1f));
        }
    }
}
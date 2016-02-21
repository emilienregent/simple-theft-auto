using UnityEngine;
using Simple.CustomType;

namespace Simple.Behaviour
{
    public class CharacterSpawnPoint : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _characters = new GameObject[0];

        private GameObject _body = null;
        private Character _character = null;

        public GameObject body { get { return _body; } }
        public Character character { get { return _character; } }

        private void Awake()
        {
            CreateCharacter();
        }

        public GameObject CreateCharacter(GameObject prefab = null)
        {
            if (prefab == null)
            {
                prefab = _characters[Random.Range(0, _characters.Length)];
            }

            if (_body != null)
            {
                GameObject.Destroy(_body);
            }

            _body = GameObject.Instantiate(prefab);

            _body.transform.position = transform.position;
            _body.transform.SetParent(transform);

            _character = _body.GetComponent<Character>();

            return _body;
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;

            Gizmos.DrawCube(transform.position, new Vector3(1f, 2f, 1f));
        }
    }
}
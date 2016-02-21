using UnityEngine;

namespace Simple.Behaviour
{
    public class CameraController : MonoBehaviour
    {
        private PlayerGame _player = null;
        private Transform _target = null;
        private Camera _camera = null;
        private Quaternion _offsetRotation = Quaternion.identity;
        private Vector3 _offsetPosition = Vector3.zero;

        [SerializeField]
        private float _fieldOfViewMin = 0f;
        [SerializeField]
        private float _fieldOfViewMax = 0f;
        [SerializeField]
        private AnimationCurve _fieldOfViewCurve = new AnimationCurve();

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _target = transform.parent;
            _player = _target.GetComponent<PlayerGame>();
            _offsetRotation = transform.localRotation;
            _offsetPosition = transform.localPosition;

            transform.SetParent(_target.parent, false);
            transform.position = _target.position + transform.rotation * _offsetPosition;
        }

        private void Update()
        {
            Quaternion rotation = Quaternion.Euler(_offsetRotation.eulerAngles.x, _target.transform.rotation.eulerAngles.y, _offsetRotation.eulerAngles.z);

            transform.position = _target.position + transform.rotation * _offsetPosition;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10f * Time.smoothDeltaTime);

            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, _fieldOfViewMin + (_fieldOfViewMax - _fieldOfViewMin) * _fieldOfViewCurve.Evaluate(_player.targetSpeed / _player.speedMax), Time.smoothDeltaTime);
        }
    }
}
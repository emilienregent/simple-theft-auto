using System;
using UnityEngine;
using Simple.CustomType;

namespace Simple.Behaviour
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField]
        private WeaponType _type = WeaponType.NONE;
        [SerializeField]
        private GameObject _prefab = null;
        [SerializeField]
        private int _ammo = 0;

        [Range(0f,1f)]
        [SerializeField]
        private float _aimingAngle = 0f;

        public WeaponType type { get { return _type; } }
        public GameObject prefab { get { return _prefab; } }
        public int ammo { get { return _ammo; } set { _ammo = value; } }
        public float aimingHeadAngle { get { return -_aimingAngle; } }
        public float aimingBodyAngle { get { return _aimingAngle; } }
    }
}
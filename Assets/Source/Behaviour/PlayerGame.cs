﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Simple.CustomType;
using Simple.Manager;

namespace Simple.Behaviour
{
    public class PlayerGame : AbstractPlayer
    {
        private static readonly float SPEED = 5f;
        private static readonly float ANGULAR_SPEED = 5f;

        private PlayerJumpingWatcher _jumpingWatcher = null;
        private CharacterAnimation _characterAnimation = null;

        private float _targetSpeed = 0f;
        private float _speedMax = 10f;
        private float _angle = 0f;

        private bool _isMoving = false;
        private bool _isAiming = false;
        private bool _isShooting = false;

        private Dictionary<WeaponType, Weapon> _weapons = new Dictionary<WeaponType, Weapon>();
        private WeaponType _currentWeaponType = WeaponType.NONE;
        private Transform _weaponRoot = null;

        [SerializeField]
        private AnimationCurve _acceleration = new AnimationCurve();

        public float targetSpeed { get { return _targetSpeed; } }
        public float speedMax { get { return _speedMax; } }
        public bool isJumping { get { return _animator.GetBool(PlayerAnimation.JUMP); }  set { _animator.SetBool(PlayerAnimation.JUMP, value); } }
        public Weapon currentWeapon { get { return _weapons.ContainsKey(_currentWeaponType) ? _weapons[_currentWeaponType] : null; } }

        public override void SetRoot(GameObject root)
        {
            base.SetRoot(root);

            _jumpingWatcher = _animator.GetBehaviour<PlayerJumpingWatcher>();
            _characterAnimation = _animator.GetComponent<CharacterAnimation>();

            _jumpingWatcher.player = this;
            _characterAnimation.player = this;

            _weaponRoot = _root.transform.FindChild(PlayerAnimation.RIGHT_HAND_ROOT);
        }

        public CharacterType LoadCharacterType()
        {
            string key = string.Format(PREF_KEY_ROOT, _id) + PREF_KEY_CHARACTER;

            return (CharacterType) PlayerPrefs.GetInt(key);
        }

        public void MoveForward()
        {
            _targetSpeed = _speedMax * _acceleration.Evaluate((_targetSpeed + SPEED) / _speedMax);

            _isMoving = true;
        }

        public void MoveBackward()
        {
            _targetSpeed = -_speedMax * _acceleration.Evaluate((_targetSpeed - SPEED) / _speedMax);

            _isMoving = true;
        }

        public void TurnLeft()
        {
            _angle -= ANGULAR_SPEED;
        }

        public void TurnRight()
        {
            _angle += ANGULAR_SPEED;
        }

        public void Jump()
        {
            if (isJumping == false)
            {
                isJumping = true;
            }
        }

        public void Aim(bool aiming)
        {
            if (_currentWeaponType != WeaponType.NONE)
            {
                _isAiming = aiming;

                float headAngle = _isAiming == true ? currentWeapon.aimingHeadAngle : 0f;
                float bodyAngle = _isAiming == true ? currentWeapon.aimingBodyAngle : 0f;

                _animator.SetFloat(PlayerAnimation.HEAD_HORIZONTAL, headAngle);
                _animator.SetFloat(PlayerAnimation.BODY_HORIZONTAL, bodyAngle);
            }
        }

        public void Shoot(bool shooting)
        {
            if (shooting == false)
            {
                _isShooting = false;

                _animator.SetBool(WeaponAnimation.FULL, false);
                _animator.SetBool(WeaponAnimation.SHOOT, false);
            }
            else if (_currentWeaponType != WeaponType.NONE)
            {
                if (currentWeapon.ammo > 0)
                {
                    if (_isShooting == false)
                    {
                        _isShooting = true;
                        _animator.SetBool(WeaponAnimation.SHOOT, true);
                    }
                    else
                    {
                        _animator.SetBool(WeaponAnimation.FULL, true);
                    }
                }
                else
                {
                    UnityEngine.Debug.Log("Reload !");
                }
            }
        }

        public void AddWeapon(Weapon weapon)
        {
            if (_weapons.ContainsKey(weapon.type) == false)
            {
                _weapons.Add(weapon.type, weapon);

                EquipWeapon(weapon.type);
            }
            else
            {
                _weapons[weapon.type].ammo += weapon.ammo;
            }

            UpdateGUIWeaponAmmo();
        }

        public void UpdateGUIWeaponAmmo()
        {
            Text weaponAmmoCount = GUIManager.instance.GetUniqueTextForPlayer(this, UniqueTextType.WEAPON_AMMO_COUNT);

            weaponAmmoCount.text = currentWeapon.ammo.ToString();
        }

        private void EquipWeapon(WeaponType type)
        {
            if (_weapons.ContainsKey(type) == true)
            {
                _currentWeaponType = type;

                if (_weaponRoot.childCount > 0)
                {
                    Transform child = _weaponRoot.GetChild(0);

                    GameObject.Destroy(child.gameObject);
                }

                GameObject weaponGO = GameObject.Instantiate<GameObject>(currentWeapon.prefab);

                weaponGO.transform.rotation = WeaponAnimation.INITIAL_ROTATION;
                weaponGO.transform.SetParent(_weaponRoot, false);

                _animator.SetInteger(WeaponAnimation.TYPE, (int)_currentWeaponType);
            }
        }

        private void FixedUpdate()
        {
            Move();
            Animate();
        }

        private void Move()
        {
            transform.position += transform.forward * _targetSpeed * Time.fixedDeltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(Vector3.up * _angle), ANGULAR_SPEED * Time.smoothDeltaTime);

            if (_isMoving == false)
            {
                _targetSpeed = Mathf.Lerp(_targetSpeed, 0f, SPEED * 5f * Time.smoothDeltaTime);
            }

            _isMoving = false;
        }

        private void Animate()
        {
            _animator.SetFloat(PlayerAnimation.SPEED, Mathf.Abs(_targetSpeed));
        }
    }
}
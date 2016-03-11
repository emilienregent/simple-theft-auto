using System;
using UnityEngine;

namespace Simple.CustomType
{
    public class WeaponAnimation
    {
        public static readonly Quaternion INITIAL_ROTATION = Quaternion.Euler(90f, 0f, 0f);
        public static readonly string TYPE = "WeaponType_int";
		public static readonly string RELOAD = "Reload_b";
        public static readonly string SHOOT = "Shoot_b";
        public static readonly string FULL = "FullAuto_b";
        public static readonly string TAG_CONSUME_ROUND = "Consume_Round";
    }
}
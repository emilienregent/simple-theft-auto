using System;
using UnityEngine;
using Simple.Service;

namespace Simple.Behaviour
{
    public abstract class AbstractPlayer : MonoBehaviour
    {
        protected static readonly string PREF_KEY_ROOT = "player-{0}-";
        protected static readonly string PREF_KEY_CHARACTER = "character";

        protected GameObject  _root       = null;
        protected Animator    _animator   = null;

        protected int _id = -1;
        public int id { get { return _id; } set { _id = value; } }

        public virtual void SetRoot(GameObject root)
        {
            if (_root != null)
            {
                GameObject.Destroy(_root);
            }

            _root       = root;
            _animator   = root.GetComponent<Animator>();

            transform.position = _root.transform.position;
            _root.transform.SetParent(gameObject.transform, false);
        }
    }
}
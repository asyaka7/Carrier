using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Scripts
{
    internal class Singletone<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = gameObject.GetComponent<T>();
                DontDestroyOnLoad(gameObject.transform.root);
            }
            else
            {
                DestroyIfDuplicate(gameObject);
            }
        }

        public bool DestroyIfDuplicate(GameObject gameObject)
        {
            T singleComponent = gameObject.GetComponent<T>();
            if (Instance != singleComponent)
            {
                Destroy(gameObject);
                return true;
            }
            return false;
        }
    }
}

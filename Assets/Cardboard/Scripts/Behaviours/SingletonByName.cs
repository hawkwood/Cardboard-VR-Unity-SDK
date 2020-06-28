using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SingletonByName : MonoBehaviour
{
    public UnityEvent OnSingletonAwake;

    private static Dictionary<string, SingletonByName> _Instances;
    private static Dictionary<string, SingletonByName> Instances
    {
        get
        {
            if (_Instances == null)
            {
                _Instances = new Dictionary<string, SingletonByName>();
            }
            return _Instances;
        }
    }

    private void Awake()
    {
        if (Instances.TryGetValue(name, out SingletonByName instance))
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instances.Add(name, this);
            OnSingletonAwake?.Invoke();
        }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

/// <summary>
/// Singleton behaviour.
/// 
/// Helper class for implementing singleton functionality on a MonoBehaviour
/// </summary>
public abstract class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
{
    private static T _instance = null;
    private static bool _isAwake = false;
    private static Object _handle = new Object();

    [SerializeField]
    private bool _warnDestruction = true;
    public bool WarnDestruction
    {
        get
        {
            return _warnDestruction;
        }
    }

    /// <summary>
    /// Indicates whether an instance exists.
    /// </summary>
    public static bool Exists
    {
        get
        {
            return _instance != null;
        }
    }

    /// <summary>
    /// Indicates whether an instance is awake.
    /// </summary>
    /// <returns><c>true</c> if is awake; otherwise, <c>false</c>.</returns>
    public static bool IsAwake
    {
        get
        {
            return _isAwake;
        }
    }

    /// <summary>
    /// Primary accessor for the instance of the singleton.
    /// </summary>
    public static T Instance
    {
        get
        {
            lock (_handle)
            {
                if (_instance == null)
                {
                    // When a scene loads, the singleton might not have been awaken yet. Some object awaken first might be
                    // calling this accessor. We need to find if a singleton is available.
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        string instanceName = typeof(T).ToString();
                        var go = new GameObject();
                        go.name = instanceName;
                        go.AddComponent<T>();
                        Assert.IsNotNull(_instance, "Awake() did not assign the Singleton instance.");
                    }
                    else if (!_isAwake)
                    {
                        _instance.Awake();
                    }
                }
                return _instance;
            }
        }
    }

    public static T GetInstance()
    {
        return Instance;
    }

    /// <summary>
    /// Implements the Unity "Awake" message. 
    /// Use OnSingletonAwake in derived classes.
    /// </summary>
    protected virtual void Awake()
    {
        bool destroy = false;
        string message = "";

        if (_instance == null || _instance == this)
        {
            if (this is T)
            {
                _instance = (T)this;
                if (!_isAwake)
                {
                    _isAwake = true;
                    OnSingletonAwake();
                }
            }
            else
            {
                destroy = true;
                message = "Error in the inheritance tree: the instance must be of type or derive from T.";
            }
        }
        else
        {
            destroy = true;
            message = "Multiple instances of '" + typeof(T).ToString() + "' were found, Self destructing.";
        }

        if (destroy)
        {
            if (_warnDestruction)
            {
                Debug.LogWarning(message);
            }
#if UNITY_EDITOR
            DestroyImmediate(gameObject);
#else
                Destroy(gameObject);
#endif
        }
    }

    /// <summary>
    /// Implements the Unity "OnDestroy" message. 
    /// Use OnSingletonDestroy in derived classes.
    /// </summary>
    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            try
            {
                _instance.OnSingletonDestroy();
            }
            finally
            {
                _instance = null;
                _isAwake = false;
            }
        }
    }

    /// <summary>
    /// This method should be used instead of MonoBehaviour.Awake()
    /// </summary>
    protected virtual void OnSingletonAwake()
    {
    }

    /// <summary>
    /// This method should be used instead of MonoBehaviour.OnDestroy()
    /// </summary>
    protected virtual void OnSingletonDestroy()
    {
    }
}

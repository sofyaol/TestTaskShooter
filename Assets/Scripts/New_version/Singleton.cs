using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool _isInstatiate = false;
    static bool IsInstatiate { get; }

    private static T _instance;

    public static T Instance
    {
        get
        {
            if (IsInstatiate) return _instance;
            
            _instance = FindObjectOfType<T>();
            if (_instance == null)
            {
                _instance = new GameObject("OBJECT OF TYPE", typeof(T)).GetComponent<T>();
            }

            _isInstatiate = true;

            return _instance;
        }
    }
    
}

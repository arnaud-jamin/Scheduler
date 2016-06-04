using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : class
{
    private static T m_instance;

    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType(typeof(T)) as T;

                if (m_instance == null)
                {
                    Debug.LogError("There needs to be one active " + typeof(T).Name + " script on a GameObject in your scene.");
                }
            }
            return m_instance;
        }
    }
}

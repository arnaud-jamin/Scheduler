using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //-----------------------------------------------------------------------------------------
    private List<GameObject> m_instances = new List<GameObject>();

    //-----------------------------------------------------------------------------------------
    [SerializeField]
    private GameObject m_prefab = null;

    [SerializeField]
    private Transform m_container = null;

    //-----------------------------------------------------------------------------------------
    public List<GameObject> Instances { get { return m_instances; } }

    //-----------------------------------------------------------------------------------------
    void Start()
    {
        Spawn();
    }

    //-----------------------------------------------------------------------------------------
    public void Spawn()
    {
        var instance = Instantiate(m_prefab);
        instance.transform.SetParent(m_container);
        instance.transform.position = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        m_instances.Add(instance);
    }

    //-----------------------------------------------------------------------------------------
    public void Unspawn()
    {
        if (m_instances.Count > 0)
        {
            var instance = m_instances[m_instances.Count - 1];
            m_instances.RemoveAt(m_instances.Count - 1);
            Destroy(instance);
        }
    }
}

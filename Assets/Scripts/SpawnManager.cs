using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //-----------------------------------------------------------------------------------------
    private List<GameObject> m_instances = new List<GameObject>();
    private bool m_repeat = true;

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
    void Spawn()
    {
        var instance = Instantiate(m_prefab);
        instance.transform.SetParent(m_container);
        instance.transform.position = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        m_instances.Add(instance);
    }

    //-----------------------------------------------------------------------------------------
    void Unspawn()
    {
        if (m_instances.Count > 0)
        {
            var instance = m_instances[m_instances.Count - 1];
            m_instances.RemoveAt(m_instances.Count - 1);
            Destroy(instance);
        }
    }

    //-----------------------------------------------------------------------------------------
    public void Process()
    {
        if (Input.GetButton("Spawn") && m_repeat)
        {
            Spawn();
            StartCoroutine(WaitAndRepeat());
        }

        if (Input.GetButton("Unspawn") && m_repeat)
        {
            Unspawn();
            StartCoroutine(WaitAndRepeat());
        }
    }

    //-----------------------------------------------------------------------------------------
    IEnumerator WaitAndRepeat()
    {
        m_repeat = false;
        yield return new WaitForSeconds(0.05f);
        m_repeat = true;
    }
}

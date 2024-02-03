using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToCopy;
    [SerializeField] private Transform  spawnPoint;
    [SerializeField] private float spawnInterval = 1f;
    private float m_Timer;

    // Update is called once per frame
    void Update()
    {
        m_Timer += Time.deltaTime;
        if(m_Timer >= spawnInterval)
        {
            m_Timer = 0;
            Instantiate(objectToCopy, spawnPoint.position, Quaternion.identity);
        }
    }
}

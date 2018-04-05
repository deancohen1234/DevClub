using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullGameManager : MonoBehaviour {

    public int m_PlayerHitPoints = 2;
    public int m_EnemyCount = 10;

    public Transform[] m_SpawnPoints;
    public GameObject m_EnemyPrefab;
    public Transform m_Player;

    public GameObject m_GameWinScreen;

    private int m_CurrentPlayerHitPoints = 0;
    private bool m_GameIsOver = false;
	// Use this for initialization
	void Start ()
    {
        InvokeRepeating("SpawnEnemy", 0, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(m_EnemyPrefab);
        enemy.GetComponent<Enemy>().OnPlayerHit += OnPlayerHit;
        enemy.GetComponent<Enemy>().OnEnemyDeath += OnDeathHit;
        enemy.GetComponent<Enemy>().m_Target = m_Player;
        enemy.transform.position = m_SpawnPoints[Random.Range(0, m_SpawnPoints.Length - 1)].position;
    }

    private void OnPlayerHit()
    {
        m_CurrentPlayerHitPoints++;

        if (m_CurrentPlayerHitPoints >= m_PlayerHitPoints)
        {
            GameOver();
        }
    }

    private void OnDeathHit()
    {
        m_EnemyCount--;
        
        if (m_EnemyCount <= 0)
        {
            Debug.Log("Game Over");
            CancelInvoke("SpawnEnemy");
            m_GameWinScreen.SetActive(true);
        }
    }

    private void GameOver()
    {
        m_Player.gameObject.SetActive(false);
    }
}

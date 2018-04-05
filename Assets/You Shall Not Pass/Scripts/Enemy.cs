using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public delegate void PlayerHit();
    public PlayerHit OnPlayerHit;

    public delegate void EnemyDeath();
    public EnemyDeath OnEnemyDeath;

    public Transform m_Target;
    public float m_Speed = 2f;

    private float m_Height;
	// Use this for initialization
	void Start () {
        m_Height = transform.position.y;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(m_Target);
        Vector3 dir = (transform.position - m_Target.position).normalized;
        //dir.y = m_Height;
        transform.Translate(dir * Time.deltaTime * m_Speed);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DestructiveWall")
        {
            Destroy(this.gameObject);
            OnEnemyDeath.Invoke();
        }

        else if (other.gameObject.tag == "Player")
        {
            OnPlayerHit.Invoke();
            Destroy(this.gameObject);
        }
    }
}

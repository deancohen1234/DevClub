using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour {

    public PlayerController m_PlayerController;
    public Material m_DefaultMaterial;
    public Material m_DeathMaterial;
    // Use this for initialization

    private void Update()
    {
        PlayerController.Direction direction = m_PlayerController.m_Direction;

        RaycastWall(-m_PlayerController.transform.forward);
    }

    private void RaycastWall(Vector3 direction)
    {
        RaycastHit hit;

        if (Physics.Raycast(m_PlayerController.transform.position, direction, out hit, 1f))
        {
            Debug.Log("Hit the wall");
            GameObject g = hit.transform.gameObject;

            g.GetComponent<MeshRenderer>().material.color = Color.red;

            if (g.layer == LayerMask.NameToLayer("UnPassable"))
            {
                return;
            }
            g.layer = LayerMask.NameToLayer("PassableWall");
            g.tag = "DestructiveWall";

            StartCoroutine(StartCloseWallTimer(g));
        }
    }

    private IEnumerator StartCloseWallTimer(GameObject wall)
    {
        yield return new WaitForSeconds(2.5f);
        Debug.Log("Close Wall");
        wall.GetComponent<MeshRenderer>().material.color = Color.white;

        wall.layer = LayerMask.NameToLayer("Default");
        wall.tag = "Untagged";
    }

}

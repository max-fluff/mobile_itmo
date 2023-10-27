using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlat : MonoBehaviour
{
    public float fallTime = 0.5f;
    public List<GameObject> disablableObjects;

    void OnTriggerEnter(Collider collider)
    {
        //Debug.DrawRay(contact.point, contact.normal, Color.white);
        if (collider.gameObject.tag == "Player")
        {
            StartCoroutine(Fall(fallTime));
        }
    }

    IEnumerator Fall(float time)
    {
        yield return new WaitForSeconds(time);

        foreach (var disablableObject in disablableObjects)
            disablableObject.SetActive(false);

        yield return new WaitForSeconds(5);

        foreach (var disablableObject in disablableObjects)
            disablableObject.SetActive(true);
    }
}
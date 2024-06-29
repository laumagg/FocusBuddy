using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform player;
    private void Init()
    {
        player = Camera.main.transform;
    }

    private void Update()
    {
        if (player == null) 
            Init();

        if (player != null)
            transform.LookAt(player);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardBehaviour : MonoBehaviour
{ 
   public GameObject rewardObjPrefab;
  public Transform spawnPoint;
    public void GiveReward(int count)
  {
    Vector3 originalPosition = spawnPoint.position;

    for (int i = 0; i < count; i++) 
    {
        Vector3 spawnPosition = new Vector3(originalPosition.x + (i * 0.1f), originalPosition.y, originalPosition.z);
        Instantiate(rewardObjPrefab, spawnPosition, Quaternion.identity, spawnPoint);
    }
  }
}

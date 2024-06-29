using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardBehaviour : MonoBehaviour
{

  public GameObject rewardObj;
  public Transform prefabObj;
    public void GiveReward(){
      GameObject obj = Instantiate(rewardObj, prefabObj);
      obj.SetActive(true);

    }
}

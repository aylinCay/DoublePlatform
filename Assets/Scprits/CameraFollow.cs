using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  public Transform player;

  private void Update()
  {
    
     var pos = Vector3.Lerp(transform.position, player.transform.position, 1.25f);

     transform.position = new Vector3(transform.position.x, transform.position.y, pos.z - 12f);
  }
}

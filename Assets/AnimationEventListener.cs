using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventListener : MonoBehaviour
{
   public PlayerController player;
   public void StandUp()
   {
     player.StandUp();
   }
}

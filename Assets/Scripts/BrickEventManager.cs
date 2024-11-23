using System;
using UnityEngine;

public class BrickEventManager : MonoBehaviour
{
  // Define the event (brick position and reference)
  public static event Action<Vector3> OnBrickCrashed;

  public static void BrickCrashed(Vector3 position)
  {
    OnBrickCrashed?.Invoke(position);
  }
}

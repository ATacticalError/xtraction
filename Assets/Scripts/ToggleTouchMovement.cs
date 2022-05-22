using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTouchMovement : MonoBehaviour
{
    public Mapbox.Examples.ImmediatePositionWithLocationProvider location;
    public Mapbox.Examples.RotateWithLocationProvider rotation;

    public Player player;

    public void ToggleMovement(){
        location.enabled = !location.isActiveAndEnabled;
        rotation.enabled = !rotation.isActiveAndEnabled;
        player.canMove = !player.canMove;
    }
}

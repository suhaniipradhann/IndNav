using UnityEngine;
using System.Collections.Generic;
public class Waypoint : MonoBehaviour
{
// List of neighboring waypoints connected to this one
public List<Waypoint> neighbors = new List<Waypoint>();
// Color for visualizing connections in the scene view
public Color gizmoColor = Color.yellow;
// Draw lines in the editor between this waypoint and its neighbors
private void OnDrawGizmos()
{
Gizmos.color = gizmoColor;
// Ensure the list is not null or empty
if (neighbors == null || neighbors.Count == 0)
return;
foreach (Waypoint neighbor in neighbors)
{
if (neighbor != null)
{
Gizmos.DrawLine(transform.position, neighbor.transform.position);
}
}
}
}
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;
public class NavigationManager : MonoBehaviour
{
public TMP_Dropdown destinationDropdown;
public LineRenderer pathRenderer;
public GameObject userMarker;
public WaypointNavigator navigator;
private Waypoint[] allWaypoints;
void Start()
{
allWaypoints = FindObjectsByType<Waypoint>(FindObjectsSortMode.None);
PopulateDropdown();
}
void PopulateDropdown()
{
if (destinationDropdown == null) return;
destinationDropdown.ClearOptions();
List<string> waypointNames = allWaypoints.Select(wp => wp.name).ToList();
List<string> dropdownOptions = new List<string> { "Select Destination" };
dropdownOptions.AddRange(waypointNames);
destinationDropdown.AddOptions(dropdownOptions);
destinationDropdown.value = 0;
}
public void NavigateToSelected()
{
if (destinationDropdown == null || destinationDropdown.options.Count <= 1) return;
int selectedIndex = destinationDropdown.value;
if (selectedIndex == 0) return;
string selectedDestination = destinationDropdown.options[selectedIndex].text;
Waypoint start = FindClosestWaypoint(userMarker.transform.position);

Debug.Log($"Auto-detected source: {start.name}");
HighlightSource(start);
Waypoint end = allWaypoints.FirstOrDefault(wp => wp.name == selectedDestination);
if (start != null && end != null)
{
List<Vector3> path = FindPath(start, end);
DrawPath(path);
if (navigator != null)
{
navigator.FollowPath(path);
}
}
else
{
Debug.LogWarning("Start or destination waypoint not found.");
}
}
Waypoint FindClosestWaypoint(Vector3 position)
{
Waypoint closest = null;
float minDist = Mathf.Infinity;
foreach (Waypoint wp in allWaypoints)
{
float dist = Vector3.Distance(position, wp.transform.position);
if (dist < minDist)
{
minDist = dist;
closest = wp;
}
}
return closest;
}
List<Vector3> FindPath(Waypoint start, Waypoint end)
{
Queue<Waypoint> queue = new Queue<Waypoint>();
Dictionary<Waypoint, Waypoint> cameFrom = new Dictionary<Waypoint, Waypoint>();
queue.Enqueue(start);
cameFrom[start] = null;

while (queue.Count > 0)
{
Waypoint current = queue.Dequeue();
if (current == end) break;
foreach (Waypoint neighbor in current.neighbors)
{
if (!cameFrom.ContainsKey(neighbor))
{
queue.Enqueue(neighbor);
cameFrom[neighbor] = current;
}
}
}
List<Vector3> path = new List<Vector3>();
Waypoint step = end;
while (step != null)
{
path.Insert(0, step.transform.position);
step = cameFrom.ContainsKey(step) ? cameFrom[step] : null;
}
return path;
}
void DrawPath(List<Vector3> path)
{
if (pathRenderer == null)
{
Debug.LogError("PathRenderer not assigned!");
return;
}
pathRenderer.positionCount = path.Count;
pathRenderer.SetPositions(path.ToArray());
}
void HighlightSource(Waypoint source)
{
GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
marker.transform.position = source.transform.position;
marker.transform.localScale = Vector3.one * 0.2f;

marker.GetComponent<Renderer>().material.color = Color.green;
Destroy(marker.GetComponent<Collider>());
Destroy(marker, 3f);
}
public void CancelNavigation()
{
if (navigator != null)
{
navigator.StopNavigation();
}
if (pathRenderer != null)
{
pathRenderer.positionCount = 0;
}
destinationDropdown.value = 0;
Debug.Log("Navigation cancelled.");
}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WaypointNavigator : MonoBehaviour
{
[Header("Movement Settings")]
public float moveSpeed = 2f;
public float rotationSpeed = 5f;
[Header("UI")]
public TextMeshProUGUI destinationMessage;
private Coroutine movementCoroutine;
public void FollowPath(List<Vector3> path)
{
if (path == null || path.Count == 0) return;
if (destinationMessage != null)
destinationMessage.gameObject.SetActive(false);
if (movementCoroutine != null)
StopCoroutine(movementCoroutine);
movementCoroutine = StartCoroutine(MoveAlongPath(path));
}
private IEnumerator MoveAlongPath(List<Vector3> path)
{
foreach (Vector3 target in path)
{
while (Vector3.Distance(transform.position, target) > 0.1f)
{
transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed *
Time.deltaTime);
Vector3 dir = (target - transform.position).normalized;
if (dir != Vector3.zero)
{
Quaternion lookRotation = Quaternion.LookRotation(dir);
transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation,

rotationSpeed * Time.deltaTime);

}
yield return null;
}
}
if (destinationMessage != null)
{
destinationMessage.text = "You have arrived at your destination!";
destinationMessage.gameObject.SetActive(true);
}
}
public void StopNavigation()
{
if (movementCoroutine != null)
{
StopCoroutine(movementCoroutine);
movementCoroutine = null;
}
if (destinationMessage != null)
{
destinationMessage.gameObject.SetActive(false);
}
}
}
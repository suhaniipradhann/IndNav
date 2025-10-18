# Indoor AR Waypoint Navigation System

This Unity-based project demonstrates a **real-time indoor waypoint navigation system** built using C#.  
It automatically detects the user's starting position and navigates them to a selected destination via connected waypoints.
It is offline, does not use Wi-fi or 

## 🧭 Features
- Visual waypoint connections using Gizmos
- Automatic shortest path selection (BFS)
- Smooth movement and rotation animations
- UI integration with dropdown destination selection
- Arrival and cancellation messages

## 📁 Script Overview
| Script | Description |
|---------|-------------|
| `Waypoint.cs` | Defines waypoints and their neighbors |
| `WaypointNavigator.cs` | Handles movement along the computed path |
| `NavigationManager.cs` | Computes path and connects UI with waypoints |
| `NavigationUI.cs` | Manages user interface (dropdown, button, and messages) |


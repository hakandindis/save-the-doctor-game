using UnityEditor;
using UnityEngine;

namespace Worq
{
	public class AWSEditor : EditorWindow
	{
		[MenuItem ("Tools/Worq/Waypoint System/Create New Waypoint Route")]
		public static void createWaypointRoute ()
		{
			SetupManager ();
			new WaypointRoute ().CreateRoute ();
		}

		[MenuItem ("Tools/Worq/Waypoint System/Setup Patrol Entity")]
		public static void setupEntity ()
		{
			SetupManager ();
			foreach (GameObject go in Selection.gameObjects) {
				if (go.GetComponent <WaypointRouteIdentifier> () ||
				    go.GetComponent <WaypointIdentifier> () ||
				    go.GetComponent <AWSManager> () ||
				    go.GetComponent <AWSPatrol> ()) {
					return;
				}

				go.AddComponent <AWSEntityIdentifier> ();
				go.AddComponent <AWSPatrol> ();
				go.name = "_AWSEntity" + "_" + go.name;
			}
		}

		private static void SetupManager ()
		{
			if (GameObject.FindObjectOfType<AWSManager> ()) {
				return;
			} else {
				GameObject go = new GameObject ();
				go.AddComponent <AWSManager> ();
				go.name = "_AWSManager";
				go.transform.position = Vector3.zero;
			}
		}

		void OnGui ()
		{

		}
	}
}
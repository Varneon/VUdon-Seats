using System.Linq;
using UdonSharpEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Varneon.VUdon.Seats.Abstract;

namespace Varneon.VUdon.Seats.Editor
{
    public class SeatPostProcessor
    {
        [PostProcessScene(-1)] // Postprocess seats before UdonSharp nukes all proxies
        public static void PostProcessSeats()
        {
            // Get all scene roots
            GameObject[] sceneRoots = SceneManager.GetActiveScene().GetRootGameObjects();

            // Find all seats
            Seat[] seats = FindSceneComponentsOfTypeAll<Seat>(sceneRoots);

            // If there are no seats, return early
            if(seats.Length == 0) { return; }

            // Try to find runtime manager from the scene
            SeatRuntimeManager runtimeManager = FindSceneComponentOfType<SeatRuntimeManager>(sceneRoots);

            // Try to find all event receivers from the scene
            SeatEventReceiver[] eventReceivers = FindSceneComponentsOfTypeAll<SeatEventReceiver>(sceneRoots);

            // If a runtime manager couldn't be found, create one
            if (runtimeManager == null)
            {
                // Create a new runtime manager
                runtimeManager = new GameObject(nameof(SeatRuntimeManager)).AddUdonSharpComponent<SeatRuntimeManager>();

                // Enforce sync mode due to it not being initialized properly when getting added on build
                UdonSharpEditorUtility.GetBackingUdonBehaviour(runtimeManager).SyncMethod = VRC.SDKBase.Networking.SyncType.None;
            }

            // Assing the runtime manager to all of the seats in the scene
            foreach(Seat seat in seats)
            {
                seat.runtimeManager = runtimeManager;
            }

            // Assign all of the seats to the runtime manager
            runtimeManager.seats = seats;

            // Assign all of the event receivers to the runtime manager
            runtimeManager.eventReceivers = eventReceivers;
        }

        /// <summary>
        /// Finds all components of type from scene roots
        /// </summary>
        /// <typeparam name="T">Component type</typeparam>
        /// <param name="roots">Scene roots</param>
        /// <returns>All found components of type</returns>
        private static T[] FindSceneComponentsOfTypeAll<T>(GameObject[] roots) where T : Component
        {
            return roots.SelectMany(r => r.GetComponentsInChildren<T>(true)).ToArray();
        }

        /// <summary>
        /// Finds the first component of type from scene roots
        /// </summary>
        /// <typeparam name="T">Component type</typeparam>
        /// <param name="roots">Scene roots</param>
        /// <returns>First found components of type</returns>
        private static T FindSceneComponentOfType<T>(GameObject[] roots) where T : Component
        {
            return roots.Select(r => r.GetComponentInChildren<T>(true)).FirstOrDefault(c => c != null);
        }
    }
}

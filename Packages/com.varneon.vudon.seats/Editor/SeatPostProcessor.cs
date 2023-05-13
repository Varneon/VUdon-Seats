using System;
using UdonSharpEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using Varneon.VUdon.Seats.Abstract;

namespace Varneon.VUdon.Seats.Editor
{
    public class SeatPostProcessor
    {
        [PostProcessScene(-1)]
        public static void PostProcessSeats()
        {
            SeatRuntimeManager runtimeManager = UnityEngine.Object.FindObjectOfType<SeatRuntimeManager>();

            SeatEventReceiver[] eventReceivers = UnityEngine.Object.FindObjectsOfType<SeatEventReceiver>();

            if (runtimeManager == null)
            {
                runtimeManager = new GameObject(nameof(SeatRuntimeManager)).AddUdonSharpComponent<SeatRuntimeManager>();

                UdonSharpEditorUtility.GetBackingUdonBehaviour(runtimeManager).SyncMethod = VRC.SDKBase.Networking.SyncType.None;

                if (eventReceivers.Length == 0) { return; }
            }

            Seat[] seats = UnityEngine.Object.FindObjectsOfType<Seat>();

            Type seatRuntimeManagerType = typeof(SeatRuntimeManager);

            foreach(Seat seat in seats)
            {
                seat.runtimeManager = runtimeManager;
            }

            runtimeManager.seats = seats;

            runtimeManager.eventReceivers = eventReceivers;
        }
    }
}

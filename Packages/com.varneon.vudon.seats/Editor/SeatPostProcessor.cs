using System;
using System.Reflection;
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
                if(eventReceivers.Length > 0)
                {
                    runtimeManager = new GameObject(nameof(SeatRuntimeManager)).AddUdonSharpComponent<SeatRuntimeManager>();
                }
                else { return; }
            }

            Seat[] seats = UnityEngine.Object.FindObjectsOfType<Seat>();

            Type seatRuntimeManagerType = typeof(SeatRuntimeManager);

            FieldInfo runtimeManagerField = typeof(Seat).GetField("runtimeManager", BindingFlags.Instance | BindingFlags.NonPublic);

            foreach(Seat seat in seats)
            {
                runtimeManagerField.SetValue(seat, runtimeManager);
            }

            FieldInfo seatsField = seatRuntimeManagerType.GetField("seats", BindingFlags.Instance | BindingFlags.NonPublic);

            seatsField.SetValue(runtimeManager, seats);

            FieldInfo eventReceiversField = seatRuntimeManagerType.GetField("eventReceivers", BindingFlags.Instance | BindingFlags.NonPublic);

            eventReceiversField.SetValue(runtimeManager, eventReceivers);
        }
    }
}

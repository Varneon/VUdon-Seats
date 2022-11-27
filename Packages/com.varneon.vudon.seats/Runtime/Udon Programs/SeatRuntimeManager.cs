using UdonSharp;
using UnityEngine;
using Varneon.VUdon.Seats.Abstract;
using VRC.SDKBase;

namespace Varneon.VUdon.Seats
{
    [AddComponentMenu("")] // This component is automatically added by scene postprocessor
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class SeatRuntimeManager : UdonSharpBehaviour
    {
        [SerializeField, HideInInspector]
        private Seat[] seats;

        [SerializeField, HideInInspector]
        private SeatEventReceiver[] eventReceivers;

        public void OnPlayerEnteredSeat(VRCPlayerApi player, Seat seat)
        {
            foreach (SeatEventReceiver receiver in eventReceivers)
            {
                receiver.OnPlayerEnteredSeat(player, seat);
            }
        }

        public void OnPlayerExitedSeat(VRCPlayerApi player, Seat seat)
        {
            foreach (SeatEventReceiver receiver in eventReceivers)
            {
                receiver.OnPlayerExitedSeat(player, seat);
            }
        }
    }
}

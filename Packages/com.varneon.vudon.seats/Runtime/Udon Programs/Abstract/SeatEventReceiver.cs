using UdonSharp;
using VRC.SDKBase;

namespace Varneon.VUdon.Seats.Abstract
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public abstract class SeatEventReceiver : UdonSharpBehaviour
    {
        public virtual void OnPlayerEnteredSeat(VRCPlayerApi player, Seat seat) { }

        public virtual void OnPlayerExitedSeat(VRCPlayerApi player, Seat seat) { }
    }
}

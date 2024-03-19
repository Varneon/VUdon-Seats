using UdonSharp;
using UnityEngine;
using Varneon.VUdon.Seats.Abstract;
using VRC.SDKBase;
using VRC.Udon.Common;

namespace Varneon.VUdon.Seats
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class SeatRuntimeManager : UdonSharpBehaviour
    {
        [SerializeField, HideInInspector]
        internal Seat[] seats;

        [SerializeField, HideInInspector]
        internal SeatEventReceiver[] eventReceivers;

        private Seat activeSeat;

        private bool vrEnabled;

        private bool manualCalibrationActive;

        private float
            moveHorizontal,
            moveVertical,
            lookHorizontal,
            lookVertical;

        private void Start()
        {
            vrEnabled = Networking.LocalPlayer.IsUserInVR();
        }

        public void OnManualCalibrationUpdate()
        {
            if (!manualCalibrationActive || activeSeat == null) { return; }

            SetSeatInputData();

            SendCustomEventDelayedFrames(nameof(OnManualCalibrationUpdate), 0);
        }

        public void OnPlayerEnteredSeat(VRCPlayerApi player, Seat seat)
        {
            if (player.isLocal) { activeSeat = seat; }

            foreach (SeatEventReceiver receiver in eventReceivers)
            {
                receiver.OnPlayerEnteredSeat(player, seat);
            }
        }

        public void OnPlayerExitedSeat(VRCPlayerApi player, Seat seat)
        {
            if (player.isLocal) { activeSeat = null; }

            foreach (SeatEventReceiver receiver in eventReceivers)
            {
                receiver.OnPlayerExitedSeat(player, seat);
            }
        }

        public void RunAutomaticCalibration()
        {
            if (activeSeat)
            {
                activeSeat._CalibrateSeatPosition();
            }
        }

        public void BeginManualCalibration()
        {
            if(activeSeat == null) { return; }

            manualCalibrationActive = true;

            OnManualCalibrationUpdate();
        }

        public void EndManualCalibration()
        {
            manualCalibrationActive = false;

            if (activeSeat == null) { return; }

            activeSeat._EndManualCalibration();
        }

        public override void InputMoveHorizontal(float value, UdonInputEventArgs args)
        {
            if (manualCalibrationActive)
            {
                moveHorizontal = value;
            }
        }

        public override void InputMoveVertical(float value, UdonInputEventArgs args)
        {
            if (manualCalibrationActive)
            {
                moveVertical = value;
            }        
        }

        public override void InputLookHorizontal(float value, UdonInputEventArgs args)
        {
            if (manualCalibrationActive && vrEnabled)
            {
                lookHorizontal = value;
            }
        }

        public override void InputLookVertical(float value, UdonInputEventArgs args)
        {
            if (manualCalibrationActive && vrEnabled)
            {
                lookVertical = value;
            }
        }

        private void SetSeatInputData()
        {
            if (manualCalibrationActive && activeSeat)
            {
                float deltaTime = Time.deltaTime;

                activeSeat._TranslateSeatPosition(new Vector3(
                    0f,
                    (moveHorizontal + lookHorizontal) * deltaTime,
                    (moveVertical + lookVertical) * deltaTime
                    ));
            }
        }

        public void SetActiveSeatZPosition(float zPosition)
        {
            if (activeSeat)
            {
                activeSeat._SetSeatZPosition(zPosition);
            }
        }

        public void SetActiveSeatYPosition(float yPosition)
        {
            if (activeSeat)
            {
                activeSeat._SetSeatYPosition(yPosition);
            }
        }

        public void SyncActiveSeatPosition()
        {
            if (activeSeat)
            {
                activeSeat._SyncPosition();
            }
        }

        public Vector2 GetActiveSeatPosition()
        {
            if (activeSeat) { return activeSeat._GetSeatPosition(); }
            else { return Vector2.zero; }
        }

        public override void InputUse(bool value, UdonInputEventArgs args)
        {
            if (manualCalibrationActive)
            {
                EndManualCalibration();
            }
        }

        public void EjectFromActiveSeat()
        {
            if (activeSeat)
            {
                activeSeat._Eject();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityCore
{

    namespace Audio
    {
        public class AudioTrigger : MonoBehaviour
        {
            public enum TriggerType
            {
                transition, 
                audio_event,
            }
            public bool manualTrigger = false; 
            public TriggerType triggerType;
            public AudioType audioEventSound;

            public AudioManager audioManager;


            public void TriggerSound()
            {
                AudioEvent();
            }

            private void OnTriggerExit(Collider other)
            {
                if (manualTrigger || other.gameObject.tag != "Player") return;

                if (triggerType == TriggerType.transition)
                {
                    Transition();
                } else
                {
                    AudioEvent();
                }
            } 
            private void Transition() { 
                //hardcode for now but change later
                switch (LocationController.currentLocation)
                {
                    case LocationController.Location.bathRoom:
                        audioManager.EnterBathroom();

                          break;

                    case LocationController.Location.hallway:
                        HallwayTransition();
                        break;

                    case LocationController.Location.livingRoom:
                        audioManager.EnterLivingRoom();
                        break;

                    case LocationController.Location.corridors:
                        audioManager.EnterCorridors();

                        break;


                }
            }

            private void HallwayTransition()
            {
                switch (LocationController.previousLocation)
                {
                    case LocationController.Location.bathRoom:
                        audioManager.LeaveBathroom();
                        break;
                  
                    case LocationController.Location.livingRoom:
                        audioManager.LeaveLivingRoom();
                        break;
                    case LocationController.Location.corridors:
                        audioManager.LeaveCorridors();
                        break;
                }
            }

            private void AudioEvent()
            {
                AudioAction.PlaySound(audioEventSound);
            }
        }
    }
}

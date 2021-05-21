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

            private AudioManager audioManager;

            private void Awake()
            {
                audioManager = GameObject.FindObjectOfType<AudioManager>();
            }
            public void TriggerSound()
            {
                AudioEvent();
            }

            private void OnTriggerExit(Collider other)
            {
                if (manualTrigger) return;// || other.gameObject.tag != "Player") return;

                if (other.gameObject.tag == "Player")
                {
                    Debug.Log(other.gameObject.name);
                    if (triggerType == TriggerType.transition)
                    {
                        Transition();
                    }
                    else
                    {
                        AudioEvent();
                    }
                }

            } 
            private void Transition() { 
                //hardcode for now but change later
                switch (LocationController.currentLocation)
                {
                    case LocationController.Location.bathRoom:
                        audioManager.EnterBathroom();

                          break;
                    case LocationController.Location.bathRoom_before_after:
                        audioManager.EnterNormalBathroom();
                        break;
                    case LocationController.Location.hallway:
                        HallwayTransition();
                        break;

                    case LocationController.Location.livingRoom:
                        audioManager.EnterLivingRoom();
                        break;

                    case LocationController.Location.corridors:
                        audioManager.EnterCorridors(1);

                        break;
                    case LocationController.Location.corridors2:
                        audioManager.EnterCorridors(2);

                        break;
                    case LocationController.Location.corridors3:
                        audioManager.EnterCorridors(3);

                        break;
                    case LocationController.Location.corridors4:
                        audioManager.EnterCorridors(4);

                        break;
                    case LocationController.Location.corridors5:
                        audioManager.EnterCorridors(5);

                        break;
                    case LocationController.Location.endOfLevel:
                        audioManager.EndOfLevel();
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
                    case LocationController.Location.bathRoom_before_after:
                        audioManager.LeaveBathroom();
                        break;
                    case LocationController.Location.livingRoom:
                        audioManager.LeaveLivingRoom();
                        break;
                    case LocationController.Location.corridors:
                        break;
                    case LocationController.Location.corridors2:

                        break;
                    case LocationController.Location.corridors3:

                        break;
                    case LocationController.Location.corridors4:
                        audioManager.StopSound(AudioType.Grandfather_thumping);
                        audioManager.StopSound(AudioType.SecondClockNoise);
                        audioManager.StopSound(AudioType.ThirdClockNoise);
                        audioManager.StopSound(AudioType.ForthClockNoise);
                        break;
                    case LocationController.Location.corridors5:
                       // audioManager.LeaveCorridors(5);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCore
{

    namespace Audio
    {


        public class AudioTransition : MonoBehaviour
        {

            public AudioController audioController;

            private void OnTriggerExit(Collider other)
            {

                switch (LocationController.previousLocation)
                {
                    case LocationController.Location.bathRoom:
                        audioController.StopAudio(AudioType.SFX_Bathroom_Tap, true, 0.0f);
                        break;
                    case LocationController.Location.hallway:
                        audioController.StopAudio(AudioType.SFX_Into_Hallway_Transition, true, 0.0f);
                        audioController.StopAudio(AudioType.Overall_Atmosphere, true, 0.0f);

                        break;
                    case LocationController.Location.livingRoom:
                        audioController.StopAudio(AudioType.ST_LivingRoom_Atmosphere, true, 0.0f);

                        break;
                    case LocationController.Location.corridors:
                        audioController.StopAudio(AudioType.Grandfather_thumping, true, 0.0f);

                        break;
                }

                //hardcode for now but change later
                switch (LocationController.currentLocation)
                {
                    case LocationController.Location.bathRoom:
                        audioController.PlayAudio(AudioType.Overall_Atmosphere, true, 0.0f);
                        audioController.PlayAudio(AudioType.SFX_Bathroom_Tap, true, 0.00f);

                        //audioController.StopAudio(AudioType.ST_01, true, 1.0f);

                        // audioController.RestartAudio(AudioType.ST_01, true, 1.0f);


                        break;

                    case LocationController.Location.hallway:
                        audioController.PlayAudio(AudioType.SFX_Into_Hallway_Transition, true, 0.0f);

                        break;

                    case LocationController.Location.livingRoom:
                        audioController.PlayAudio(AudioType.ST_LivingRoom_Atmosphere, true, 0.0f);
                        audioController.PlayAudio(AudioType.Overall_Atmosphere, true, 0.0f);



                        break;

                    case LocationController.Location.corridors:
                        audioController.PlayAudio(AudioType.Overall_Atmosphere, true, 0.0f);
                        audioController.PlayAudio(AudioType.Grandfather_thumping, true, 0.0f);

                        break;





                }
            }
        }
    }
}
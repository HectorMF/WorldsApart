using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WorldsApart.Handlers
{
    public class PlaySoundHandler : Handler
    {
        public AudioSource audio;

        public override void innerDelegate()
        {
            audio.Play();
        }
    }
}

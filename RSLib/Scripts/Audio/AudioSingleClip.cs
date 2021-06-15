﻿namespace RSLib.Audio
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Single Clip", menuName = "RSLib/Audio/Playlist/Single")]
    public class AudioSingleClip : AudioPlaylist
    {
        [SerializeField] private AudioClipPlayDatas _clipPlayDatas = null;

        public override AudioClipPlayDatas GetNextClipDatas()
        {
            return _clipPlayDatas;
        }
    }
}
using System;
using Unity.Collections;
using UnityEngine;

namespace RpDev.Level.Data
{
    [Serializable]
    public class LevelInfo
    {
        [SerializeField][ReadOnly] private string _levelId = Guid.NewGuid().ToString();

        public string LevelId => _levelId;
    }
}

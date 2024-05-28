using UnityEngine;

namespace RpDev.Level.Data
{
    [CreateAssetMenu(menuName = "Resources/LevelContainer")]
    public class LevelContainer : ScriptableObject
    {
        [SerializeField] private LevelInfo[] _levels;

        public LevelInfo[] Levels => _levels;
    }
}

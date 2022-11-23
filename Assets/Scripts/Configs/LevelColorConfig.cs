using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "LevelColorConfig", menuName = "ScriptableObjects/LevelColorConfig", order = 1)]
    public class LevelColorConfig : ScriptableObject
    {
        [SerializeField] private List<Color> _gradeColors;

        public Color GetColorForLevel(int level)
        {
            if (level <= _gradeColors.Count && level > 0)
            {
                return _gradeColors[level - 1];
            }
            else
            {
                Debug.LogError("Can't load grade color for level " + level);
                return Color.white;
            }
        }
    }
}

using UnityEditor;
using UnityEngine;

namespace Zindeaxx.SoundSystem
{

#if UNITY_EDITOR
    [InitializeOnLoad]
    public class SimpleSavingInitializer
    {
        static SimpleSavingInitializer()
        {
            if (!PlayerPrefs.HasKey("ZINDEAXX_SAVEGAME_INFOSHOW"))
            {
                SaveGameTutorialWindow.ShowWindow();
            }
        }
    }
#endif
}
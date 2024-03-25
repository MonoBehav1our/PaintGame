
using UnityEngine;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public byte[][] pictures = new byte[50][];
        public bool[] savesPreviously = new bool[50];
        public int interfaceColor = 0;


        public SavesYG()
        {
        }
    }
}

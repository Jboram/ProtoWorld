using UnityEngine;

namespace ProtoWorld
{
    [System.Serializable]
    public struct DialogData
    {
        public int speakerIndex;    // 현재 speakers 배열 순번
        [TextArea(3, 5)]
        public string dialogue;     // 대사
    }

}
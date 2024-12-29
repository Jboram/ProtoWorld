using UnityEngine;

namespace ProtoWorld
{
    [System.Serializable]
    public struct DialogData
    {
        public int speakerIndex;    // ���� speakers �迭 ����
        [TextArea(3, 5)]
        public string dialogue;     // ���
    }

}
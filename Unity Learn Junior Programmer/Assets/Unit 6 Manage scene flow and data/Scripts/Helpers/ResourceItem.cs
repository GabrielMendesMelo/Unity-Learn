using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit6
{
    [CreateAssetMenu(fileName = "ResourceItem", menuName = "Tutorial/Resource Item")]
    public class ResourceItem : ScriptableObject
    {
        public string Name;
        public string Id;
        public Sprite Icone;
    }

}

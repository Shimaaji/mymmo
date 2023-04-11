using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Managers
{
    class MinimapManager:Singleton<MinimapManager>
    {
        public UIMinimap minimap;

        private Collider minimapBoundingBox;
        public Collider MinimapBondingBox
        {
            get
            {
                return this.minimapBoundingBox;
            }
        }
        public Transform PlayerTransform
        {
            get
            {
                if (User.Instance.CurrentCharacterObject == null)
                    return null;
                return User.Instance.CurrentCharacterObject.transform;
            }
        }
        public Sprite LoadCurrentMinimap()
        {
            return Resloader.Load<Sprite>("UI/Minimap/" + User.Instance.CurrentMapData.Minimap);
        }

        public void UpdateMiniMap(Collider minimapBoundingBox)
        {
            this.minimapBoundingBox = minimapBoundingBox;
            if (this.minimap != null)
            {
                this.minimap.UpdateMap();
            }
        }
    }
}

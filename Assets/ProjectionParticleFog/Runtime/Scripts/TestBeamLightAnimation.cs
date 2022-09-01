using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectionParticleFog
{
    [ExecuteAlways]
    public class TestBeamLightAnimation : MonoBehaviour
    {
    
        public Vector3 offsetPosition;
        public Vector2 moveRangeXZ = new Vector2(2,2);
        public float speed = 2f;

        public Vector2 offsetXZ = new Vector2(1.5f, 3f);
    
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            var i = 0;
            foreach (Transform child in this.transform)
            {
                var lookAtPosition = new Vector3(
                    Mathf.Sin(Time.time * this.speed+this.offsetXZ.x*i) * this.moveRangeXZ.x + this.offsetPosition.x,
                    this.offsetPosition.y,
                    Mathf.Cos(Time.time *this.speed+this.offsetXZ.y*i) * this.moveRangeXZ.y + this.offsetPosition.z);
                i++;
            
                child.LookAt(lookAtPosition);
            }
        }
    }
}

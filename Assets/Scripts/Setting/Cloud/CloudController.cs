/*
 * CLOUD
 * 
 * Script controlling the clouds movement. The Init method is used instead of the Start method in order
 * to assign the proper value in the Xmax variable.
 * Init is invoked by the cloudSpawner only after the Camera has been properly scaled
 * 
 * */

using UnityEngine;

namespace HanoiTower.Setting.Cloud
{
    public class CloudController : MonoBehaviour
    {
        CloudSpawner spawner;
        CloudDescription description;

        SpriteRenderer cloudRenderer;

        bool active;

        // Maximum X of the screen in world coordinates
        float Xmax;

        public void Init(CloudSpawner cloudSpawner)
        {
            spawner = cloudSpawner;

            Xmax = spawner.GetXMax();

            cloudRenderer = GetComponent<SpriteRenderer>();   
        }

        public void MorphAs( CloudDescription cloudDescription )
        {
            description = cloudDescription;

            cloudRenderer.sprite = description.sprite;

            transform.localScale = new Vector2(description.scale, description.scale);

        }

        public void SetPosition(Vector2 position, bool inGameSpawn = true)
        {
            active = true;

            if (!inGameSpawn)
            {
                transform.position = position.ToVector3(0);
            }
            else
            {
                transform.position = position.ToVector3(0) + Vector3.left * description.sprite.bounds.extents.x * description.scale;
            }
        }

        public bool GetActiveStatus()
        {
            return active;
        }

        void Update()
        {
            if (active)
            {
                if (transform.position.x < Xmax + description.sprite.bounds.extents.x)
                {
                    transform.position = transform.position + Vector3.right * description.speed * Time.deltaTime;
                }
                else
                {
                    active = false;
                }
            }
        }

    }
}
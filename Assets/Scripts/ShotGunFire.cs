using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    class ShotGunFire : MonoBehaviour
    {
        private Test_Player player;
        private GameObject bulletPrefab;
        public ShotGunFire(Test_Player player, GameObject bulletPrefab)
        {
            this.player = player;
            this.bulletPrefab = bulletPrefab;
        }

        public void fire(int numb)
        {
            for(int i = 0; i < numb; i++)
            {
                Instantiate(bulletPrefab, new Vector3(
                  player.transform.position.x, player.transform.position.y + 1, player.transform.position.z),
                  new Quaternion(player.transform.rotation.x, player.transform.rotation.y + Random.Range(-0.12f, 0.12f), player.transform.rotation.z, player.transform.rotation.w),//player.transform.rotation, 
                  null
                  );
            }
        }
    }
}

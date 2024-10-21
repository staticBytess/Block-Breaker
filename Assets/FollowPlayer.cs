using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private float offset;
    private Player player;

    public void setOffsetPlayer(float xOffset, Player followMe) {
        offset = xOffset;
        player = followMe;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void moveSides() {
        this.transform.position = new Vector3(player.transform.position.x + offset, player.transform.position.y, player.transform.position.z);
        
    }

    void Update() {
        moveSides();
    }
}

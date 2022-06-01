using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GamePacket
{
	public long timestamp;
	public int packetType;
	public int playerX;
	public int playerY;
	public int destX;
	public int destY;
	public int uuid;
}

public class NetworkHandler : MonoBehaviour
{
	private Queue<GamePacket> packets;
	private Dictionary<int, PlayerController> players;
	
	[SerializeField] private GameObject localPlayerPrefab;
	[SerializeField] private GameObject playerPrefab;
	[SerializeField] private Vector2Int initPosition;
	[SerializeField] private int localUuid;

	public static NetworkHandler instance { get; private set; }

	private void Awake()
	{
		packets = new Queue<GamePacket>();
		players = new Dictionary<int, PlayerController>();
	}

	private void Start()
	{
		instance = this;

		SpawnLocalPlayer();
	}

	public void SpawnLocalPlayer()
	{
		GameObject instance = Instantiate(localPlayerPrefab);
		instance.GetComponent<FixedPosTransform>().position = initPosition;
		PlayerController newPlayer = instance.GetComponent<PlayerController>();
		players.Add(localUuid, newPlayer);

		SendDatagram(2, initPosition, initPosition);
	}

    public void ReceiveDatagram(byte[] bytes)
	{
		for (int i = 0; i <= bytes.Length - 32; i += 32)
		{
			GamePacket p = new GamePacket();
			p.timestamp = System.BitConverter.ToInt64(bytes, i + 0);
			p.packetType = System.BitConverter.ToInt32(bytes, i + 8);
			p.playerX = System.BitConverter.ToInt32(bytes, i + 12);
			p.playerY = System.BitConverter.ToInt32(bytes, i + 16);
			p.destX = System.BitConverter.ToInt32(bytes, i + 20);
			p.destY = System.BitConverter.ToInt32(bytes, i + 24);
			p.uuid = System.BitConverter.ToInt32(bytes, i + 28);

			if (p.packetType == 1)
			{
				RunQueue();
			}
			else
			{
				packets.Enqueue(p);
			}
		}
	}

	public void SendDatagram(int type, Vector2Int player, Vector2Int dest)
	{
        byte[] dgram = new byte[32];

		System.BitConverter.GetBytes(UnixTimeNow()).CopyTo(dgram, 0);
        System.BitConverter.GetBytes(type).CopyTo(dgram, 8);
        System.BitConverter.GetBytes(player.x).CopyTo(dgram, 12);
        System.BitConverter.GetBytes(player.y).CopyTo(dgram, 16);
        System.BitConverter.GetBytes(dest.x).CopyTo(dgram, 20);
        System.BitConverter.GetBytes(dest.y).CopyTo(dgram, 24);
        System.BitConverter.GetBytes(localUuid).CopyTo(dgram, 28);

		Debug.Log(dgram);

		// 테스트용: 바로 송신 처리
		ReceiveDatagram(dgram);

		System.BitConverter.GetBytes(UnixTimeNow()).CopyTo(dgram, 0);
        System.BitConverter.GetBytes(1).CopyTo(dgram, 8);
        System.BitConverter.GetBytes(0).CopyTo(dgram, 12);
        System.BitConverter.GetBytes(0).CopyTo(dgram, 16);
        System.BitConverter.GetBytes(0).CopyTo(dgram, 20);
        System.BitConverter.GetBytes(0).CopyTo(dgram, 24);
        System.BitConverter.GetBytes(0).CopyTo(dgram, 28);
		ReceiveDatagram(dgram);
	}

	private void RunQueue()
	{
		while (packets.Count > 0)
		{
			GamePacket p = packets.Dequeue();
			Vector2Int pos = new Vector2Int(p.playerX, p.playerY);
			Vector2Int dest = new Vector2Int(p.destX, p.destY);

			if (!players.ContainsKey(p.uuid))
			{
				GameObject instance = Instantiate(playerPrefab);
				instance.GetComponent<FixedPosTransform>().position = initPosition;
				PlayerController newPlayer = instance.GetComponent<PlayerController>();
				players.Add(p.uuid, newPlayer);
			}

			PlayerController player = players[p.uuid];

			if (p.packetType == 2)
				player.Move(pos, dest);
			else if (p.packetType == 3)
				player.Shoot(pos, dest);
			else if (p.packetType == 4)
				player.Jump(pos, dest);
			else if (p.packetType == 5)
			{
				// 죽음
				players.Remove(p.uuid);
			}
		}
	}
	
	private static long UnixTimeNow()
	{
        var timeSpan = System.DateTime.UtcNow - new System.DateTime(1970, 1, 1, 0, 0, 0);
        return timeSpan.Ticks;
    }
}

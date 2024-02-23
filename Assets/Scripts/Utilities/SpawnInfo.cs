using Unity.Netcode;
using UnityEngine;

namespace Assets.Scripts.Utilities {
	public struct SpawnInfo : INetworkSerializable {
		public string Name;
		public Vector3 Position;
		public Vector3 Euler;

		public SpawnInfo(string name, Vector3 position, Vector3 euler) {
			Name = name;
			Position = position;
			Euler = euler;
		}

		public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter {
			serializer.SerializeValue(ref Name);
			serializer.SerializeValue(ref Position);
			serializer.SerializeValue(ref Euler);
		}
	}
}
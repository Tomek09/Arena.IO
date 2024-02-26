using Unity.Netcode;

namespace Assets.Scripts.Damage {
	public struct DamageInfo : INetworkSerializable {

		public int Damage;

		public DamageInfo(int damage) {
			Damage = damage;
		}

		public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter {
			serializer.SerializeValue(ref Damage);
		}
	}
}
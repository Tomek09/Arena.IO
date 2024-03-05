using Unity.Netcode;

namespace Assets.Scripts.Damage {
	public struct DamageInfo : INetworkSerializable {

		public ulong DamageDealer;
		public int Damage;

		public DamageInfo(ulong damageDealer, int damage) {
			DamageDealer = damageDealer;
			Damage = damage;
		}

		public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter {
			serializer.SerializeValue(ref DamageDealer);
			serializer.SerializeValue(ref Damage);
		}
	}
}
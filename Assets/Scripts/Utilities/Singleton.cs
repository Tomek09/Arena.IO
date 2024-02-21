﻿using Unity.Netcode;
using UnityEngine;

namespace Assets.Scripts.Utility {
	public class Singleton<T> : MonoBehaviour where T : Component {
		public static T Instance { get; private set; }

		public virtual void Awake() {
			if (Instance == null) {
				Instance = this as T;
			} else {
				Destroy(gameObject);
			}
		}
	}

	public class SingletonPersistent<T> : MonoBehaviour where T : Component {
		public static T Instance { get; private set; }

		public virtual void Awake() {
			if (Instance == null) {
				Instance = this as T;
				DontDestroyOnLoad(this);
			} else {
				Destroy(gameObject);
			}
		}
	}

	public class SingletonNetwork<T> : NetworkBehaviour where T : Component {
		public static T Instance { get; private set; }

		public virtual void Awake() {
			if (Instance == null) {
				Instance = this as T;
			} else {
				Destroy(gameObject);
			}
		}
	}

	public class SingletonNetworkPersistent<T> : NetworkBehaviour where T : Component {
		public static T Instance { get; private set; }

		public virtual void Awake() {
			if (Instance == null) {
				Instance = this as T;
			} else {
				Destroy(gameObject);
			}
		}

		public override void OnNetworkSpawn() {
			DontDestroyOnLoad(this);
		}

		public override void OnNetworkDespawn() {
			Destroy(gameObject);
		}
	}
}
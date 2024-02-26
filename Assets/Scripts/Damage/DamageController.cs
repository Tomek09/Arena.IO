using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Damage {
	public static class DamageController {

		public static IDamagable[] GetDamagables(Vector3 position, float radius) {
			Collider[] collisions = new Collider[99];
			int totalHits = Physics.OverlapSphereNonAlloc(position, radius, collisions);

			return collisions.Take(totalHits)
					.Select(x => x.GetComponent<IDamagable>())
					.Where(x => x != null)
					.ToArray();
		}
	}
}
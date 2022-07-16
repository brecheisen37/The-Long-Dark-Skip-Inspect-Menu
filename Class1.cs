using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MelonLoader;

namespace SkipInspection
{
	[HarmonyPatch(typeof(PlayerManager), "InteractiveObjectsProcessInteraction")]
	public class Patch
	{
		public static bool Prefix(ref PlayerManager __instance)
		{
			if (__instance.m_InteractiveObjectUnderCrosshair != null)
			{
					GearItem gearitem = __instance.m_InteractiveObjectUnderCrosshair.GetComponent<GearItem>();
					if (__instance.m_InteractiveObjectUnderCrosshair.name.Contains("GEAR_") && !gearitem.IsAttachedToPlacePoint())
					{
						GameManager.GetPlayerManagerComponent().ProcessPickupItemInteraction(gearitem, false, false);
						return false;
					}
			}
			return true;
		}
	}

	public class SkipInspection_Melon : MelonMod
	{
		public override void OnApplicationStart()
		{
			MelonDebug.Msg($"[{Info.Name}] Version {Info.Version} loaded!");
		}

	}

}

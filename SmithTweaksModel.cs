﻿using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;
using TaleWorlds.Core;

namespace SmithTweaks
{
	public class SmithTweaksModel : DefaultSmithingModel
	{
		public override int GetEnergyCostForRefining(ref Crafting.RefiningFormula refineFormula, Hero hero)
		{
			return 0;
		}

		public override int GetEnergyCostForSmithing(ItemObject item, Hero hero)
		{
			return 0;
		}

		public override int GetEnergyCostForSmelting(ItemObject item, Hero hero)
		{
			return 0;
		}
		
		public override IEnumerable<TaleWorlds.Core.Crafting.RefiningFormula> GetRefiningFormulas(
			Hero weaponsmith)
		{
			yield return new TaleWorlds.Core.Crafting.RefiningFormula(CraftingMaterials.Wood, 2, CraftingMaterials.Iron1, 0, CraftingMaterials.Charcoal, 1, CraftingMaterials.IronOre, 0);
			if (weaponsmith.GetPerkValue(DefaultPerks.Crafting.CharcoalMaker))
				yield return new TaleWorlds.Core.Crafting.RefiningFormula(CraftingMaterials.Wood, 3, CraftingMaterials.Iron1, 0, CraftingMaterials.Charcoal, 2, CraftingMaterials.IronOre, 0);
			yield return new TaleWorlds.Core.Crafting.RefiningFormula(CraftingMaterials.IronOre, 1, CraftingMaterials.Charcoal, 1, CraftingMaterials.Iron1, weaponsmith.GetPerkValue(DefaultPerks.Crafting.IronMaker) ? 3 : 2, CraftingMaterials.IronOre, 0);
			yield return new TaleWorlds.Core.Crafting.RefiningFormula(CraftingMaterials.Iron1, 1, CraftingMaterials.Charcoal, 1, CraftingMaterials.Iron2, 1, CraftingMaterials.IronOre, 0);
			yield return new TaleWorlds.Core.Crafting.RefiningFormula(CraftingMaterials.Iron2, 2, CraftingMaterials.Charcoal, 1, CraftingMaterials.Iron3, 1, CraftingMaterials.Iron1, 1);
			yield return new TaleWorlds.Core.Crafting.RefiningFormula(CraftingMaterials.Iron1, 1, CraftingMaterials.Charcoal, 1, CraftingMaterials.Iron6, 1, CraftingMaterials.Iron1, 1);
			if (weaponsmith.GetPerkValue(DefaultPerks.Crafting.SteelMaker))
				yield return new TaleWorlds.Core.Crafting.RefiningFormula(CraftingMaterials.Iron3, 2, CraftingMaterials.Charcoal, 1, CraftingMaterials.Iron4, 1, CraftingMaterials.Iron1, 1);
			if (weaponsmith.GetPerkValue(DefaultPerks.Crafting.SteelMaker2))
				yield return new TaleWorlds.Core.Crafting.RefiningFormula(CraftingMaterials.Iron4, 2, CraftingMaterials.Charcoal, 1, CraftingMaterials.Iron5, 1, CraftingMaterials.Iron1, 1);
			if (weaponsmith.GetPerkValue(DefaultPerks.Crafting.SteelMaker3))
				yield return new TaleWorlds.Core.Crafting.RefiningFormula(CraftingMaterials.Iron5, 2, CraftingMaterials.Charcoal, 1, CraftingMaterials.Iron6, 1, CraftingMaterials.Iron1, 1);
		}
	}
}

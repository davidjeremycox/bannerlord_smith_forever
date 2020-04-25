using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors.Towns;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SmithTweaks
{
	public class SmithTweaksSubModule : MBSubModuleBase
	{
		protected static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

		protected override void OnSubModuleLoad()
		{
			NLog.Config.LoggingConfiguration logConfig = new NLog.Config.LoggingConfiguration();
			NLog.Targets.FileTarget logFile = new NLog.Targets.FileTarget(LogFileTarget()) { FileName = LogFilePath() };

			logConfig.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, logFile);
			NLog.LogManager.Configuration = logConfig;
		}

		protected virtual string LogFileTarget()
		{
			return "SmithTweakLogFile";
		}

		protected virtual string LogFilePath()
		{
			// The default, relative path will place the log in $(GameFolder)\bin\Win64_Shipping_Client\
			return "SmithTweakLog.txt";
		}

		protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
		{
			if (!(game.GameType is Campaign))
			{
				return;
			}
			Log.Info("OnGameStart");
			AddModels(gameStarterObject);
		}

		protected virtual void AddModels(IGameStarter gameStarterObject)
		{
			//ReplaceModel<DefaultSmithingModel, SmithTweaksModel>(gameStarterObject);
			CampaignGameStarter campaignGameStarter = (CampaignGameStarter) gameStarterObject;
			gameStarterObject.AddGameMenuOption("town", "town_bulk_smithy", "{=McHsHbH8}Enter bulk smithy", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_craft_item_on_condition), (GameMenuOption.OnConsequenceDelegate) (x => CampaignCraftingManager.OpenCrafting(CraftingTemplate.All.First<CraftingTemplate>(), (CraftingState) null)), false, -1, false);
		}

		protected void ReplaceModel<TBaseType, TChildType>(IGameStarter gameStarterObject)
			where TBaseType : GameModel
			where TChildType : TBaseType
		{
			if (!(gameStarterObject.Models is IList<GameModel> models))
			{
				Log.Error("Models was not a list");
				return;
			}

			bool found = false;
			for (int index = 0; index < models.Count; ++index)
			{
				if (models[index] is TBaseType)
				{
					found = true;
					if (models[index] is TChildType)
					{
						Log.Info($"Child model {typeof(TChildType).Name} found, skipping.");
					}
					else
					{
						Log.Info($"Base model {typeof(TBaseType).Name} found. Replacing with child model {typeof(TChildType).Name}");
						models[index] = Activator.CreateInstance<TChildType>();
					}
				}
			}

			if (!found)
			{
				Log.Info($"Base model {typeof(TBaseType).Name} was not found. Adding child model {typeof(TChildType).Name}");
				gameStarterObject.AddModel(Activator.CreateInstance<TChildType>());
			}
		}
	}
}

using DRsoft.Modeling.AppServices;
using DRsoft.Modeling.Metadata.Models.Config;
using EngineConfig = DRsoft.Modeling.Metadata.Models.Config.EngineConfig;

// ReSharper disable All

namespace Engine.Transfer
{
    public class TransferConfig
    {
        /// <summary>
        /// 读取配置文件到引擎
        /// </summary>
        /// <param name="engineConfig"></param>
        /// <returns>
        /// result=0 读取正常
        /// result=10000 读取失败,数据结构错误
        /// </returns>
        public int UpdateEngConfig(out DRsoft.Engine.Core.Engine.EngineConfig engineConfig)
        {
            engineConfig = new DRsoft.Engine.Core.Engine.EngineConfig();
            int result;
            try
            {
                result = RecipeConfigRead();
                result = EngineConfigRead(engineConfig); //最先加载
                result = ControllerConfigRead(engineConfig);
                result = CalibrationConfigRead(engineConfig);

            }
            catch (Exception)
            {
                result = 10000;
            }

            return result;
        }

        RecipeAppService appService = new RecipeAppService();
        private RecipeConfig _recipeConfig = new RecipeConfig();
        private int RecipeConfigRead()
        {
            int result = 0;
            try
            {
                _recipeConfig = appService.Read() ?? new RecipeConfig();
                Method.RecipeConfigUse = _recipeConfig;
                Method.RecipeAppService = appService;
                //配方列表
                Method.LisRecipeName = new List<string?>();
                Method.LisRecipes = _recipeConfig.LisRecipeNote;
                Method.SelectRecipe = _recipeConfig.SelectRecipeName;
                for (int i = 0; i < Method.LisRecipes.Count; i++)
                {
                    Method.LisRecipeName.Add(Method.LisRecipes[i].Name);
                }
                Method.GuidRecipeConfig.Clear();
            }
            catch (Exception)
            {
                result = -1;
            }

            return result;
        }

        EngineAppService engineAppService = new EngineAppService();
        EngineConfig _engConfig = new EngineConfig();

        private int EngineConfigRead(DRsoft.Engine.Core.Engine.EngineConfig config)
        {
            int result = 0;
            try
            {
                Method.DicEngineConfig.Clear();
                if (Method.LisRecipes.Count > 0)
                {
                    for (int j = 0; j < Method.LisRecipes.Count; j++)
                    {
                        engineAppService.metadataGuid = Method.LisRecipes[j].Id[0];
                        _engConfig = engineAppService.Read() ?? new EngineConfig();
                        Method.DicEngineConfig.Add($"{Method.LisRecipes[j].Name}0", _engConfig);
                        Method.GuidRecipeConfig.Add($"{Method.LisRecipes[j].Name}0", Method.LisRecipes[j].Id[0]);
                        if (Method.SelectRecipe != null && Method.SelectRecipe.Equals($"{Method.LisRecipes[j].Name}"))
                        {
                            #region 元素
                            config.IsVerbose = _engConfig.IsVerbose;
                            config.RefreshRate = _engConfig.RefreshRate;
                            config.PcParamConfig.ProductionType = _engConfig.PcParam.ProductionType;
                            config.PcParamConfig.MarkingPath = _engConfig.PcParam.MarkingPath;
                            config.PcParamConfig.MarkingNamePrefix = _engConfig.PcParam.MarkingNamePrefix;
                            config.PcParamConfig.LogOutTime = _engConfig.PcParam.LogOutTime;
                            config.PcParamConfig.PowerMeterMeasurePos1X = _engConfig.PcParam.PowerMeterMeasurePos1X;
                            config.PcParamConfig.PowerMeterMeasurePos1Y = _engConfig.PcParam.PowerMeterMeasurePos1Y;
                            config.PcParamConfig.PowerMeterMeasurePos2X = _engConfig.PcParam.PowerMeterMeasurePos2X;
                            config.PcParamConfig.PowerMeterMeasurePos2Y = _engConfig.PcParam.PowerMeterMeasurePos2Y;
                            config.PcParamConfig.PowerMeterMeasurePos3X = _engConfig.PcParam.PowerMeterMeasurePos3X;
                            config.PcParamConfig.PowerMeterMeasurePos3Y = _engConfig.PcParam.PowerMeterMeasurePos3Y;
                            config.PcParamConfig.PowerMeterMeasurePos4X = _engConfig.PcParam.PowerMeterMeasurePos4X;
                            config.PcParamConfig.PowerMeterMeasurePos4Y = _engConfig.PcParam.PowerMeterMeasurePos4Y;
                            config.PcParamConfig.PowerMeterMeasurePos5X = _engConfig.PcParam.PowerMeterMeasurePos5X;
                            config.PcParamConfig.PowerMeterMeasurePos5Y = _engConfig.PcParam.PowerMeterMeasurePos5Y;
                            config.PcParamConfig.PowerMeterMeasurePos6X = _engConfig.PcParam.PowerMeterMeasurePos6X;
                            config.PcParamConfig.PowerMeterMeasurePos6Y = _engConfig.PcParam.PowerMeterMeasurePos6Y;
                            config.PcParamConfig.PowerMeterMeasurePos7X = _engConfig.PcParam.PowerMeterMeasurePos7X;
                            config.PcParamConfig.PowerMeterMeasurePos7Y = _engConfig.PcParam.PowerMeterMeasurePos7Y;
                            config.PcParamConfig.PowerMeterMeasurePos8X = _engConfig.PcParam.PowerMeterMeasurePos8X;
                            config.PcParamConfig.PowerMeterMeasurePos8Y = _engConfig.PcParam.PowerMeterMeasurePos8Y;
                            config.PcParamConfig.PowerMeterMeasurePos9X = _engConfig.PcParam.PowerMeterMeasurePos9X;
                            config.PcParamConfig.PowerMeterMeasurePos9Y = _engConfig.PcParam.PowerMeterMeasurePos9Y;
                            config.PcParamConfig.PowerMeterMeasurePos10X = _engConfig.PcParam.PowerMeterMeasurePos10X;
                            config.PcParamConfig.PowerMeterMeasurePos10Y = _engConfig.PcParam.PowerMeterMeasurePos10Y;
                            config.PcParamConfig.PowerMeterMeasurePos11X = _engConfig.PcParam.PowerMeterMeasurePos11X;
                            config.PcParamConfig.PowerMeterMeasurePos11Y = _engConfig.PcParam.PowerMeterMeasurePos11Y;
                            config.PcParamConfig.PowerMeterMeasurePos12X = _engConfig.PcParam.PowerMeterMeasurePos12X;
                            config.PcParamConfig.PowerMeterMeasurePos12Y = _engConfig.PcParam.PowerMeterMeasurePos12Y;
                            config.PcParamConfig.PowerMeterInterval = _engConfig.PcParam.PowerMeterInterval;
                            config.PcParamConfig.Laser1Power = _engConfig.PcParam.Laser1Power;
                            config.PcParamConfig.Laser1Freq = _engConfig.PcParam.Laser1Freq;
                            config.PcParamConfig.Laser2Power = _engConfig.PcParam.Laser2Power;
                            config.PcParamConfig.Laser2Freq = _engConfig.PcParam.Laser2Freq;
                            config.PcParamConfig.Laser3Power = _engConfig.PcParam.Laser3Power;
                            config.PcParamConfig.Laser3Freq = _engConfig.PcParam.Laser3Freq;
                            config.PcParamConfig.Laser4Power = _engConfig.PcParam.Laser4Power;
                            config.PcParamConfig.Laser4Freq = _engConfig.PcParam.Laser4Freq;
                            config.PcParamConfig.Laser5Power = _engConfig.PcParam.Laser5Power;
                            config.PcParamConfig.Laser5Freq = _engConfig.PcParam.Laser5Freq;
                            config.PcParamConfig.Laser6Power = _engConfig.PcParam.Laser6Power;
                            config.PcParamConfig.Laser6Freq = _engConfig.PcParam.Laser6Freq;
                            config.PcParamConfig.Laser7Power = _engConfig.PcParam.Laser7Power;
                            config.PcParamConfig.Laser7Freq = _engConfig.PcParam.Laser7Freq;
                            config.PcParamConfig.Laser8Power = _engConfig.PcParam.Laser8Power;
                            config.PcParamConfig.Laser8Freq = _engConfig.PcParam.Laser8Freq;
                            config.PcParamConfig.Laser9Power = _engConfig.PcParam.Laser9Power;
                            config.PcParamConfig.Laser9Freq = _engConfig.PcParam.Laser9Freq;
                            config.PcParamConfig.Laser10Power = _engConfig.PcParam.Laser10Power;
                            config.PcParamConfig.Laser10Freq = _engConfig.PcParam.Laser10Freq;
                            config.PcParamConfig.Laser11Power = _engConfig.PcParam.Laser11Power;
                            config.PcParamConfig.Laser11Freq = _engConfig.PcParam.Laser11Freq;
                            config.PcParamConfig.Laser12Power = _engConfig.PcParam.Laser12Power;
                            config.PcParamConfig.Laser12Freq = _engConfig.PcParam.Laser12Freq;
                            config.PcParamConfig.PowerMeterMeasureHl = _engConfig.PcParam.PowerMeterMeasureHl;
                            config.PcParamConfig.PowerMeterMeasureLl = _engConfig.PcParam.PowerMeterMeasureLl;
                            config.PcParamConfig.PowerMeterRatio = _engConfig.PcParam.PowerMeterRatio;
                            config.PcParamConfig.PowerMeterPercent = _engConfig.PcParam.PowerMeterPercent;
                            config.PcParamConfig.IsSilicaWashed = _engConfig.PcParam.IsSilicaWashed;
                            config.PcParamConfig.IsDirtyPosMarked = _engConfig.PcParam.IsDirtyPosMarked;
                            config.PcParamConfig.VibraOfs1X = _engConfig.PcParam.VibraOfs1X;
                            config.PcParamConfig.VibraOfs1Y = _engConfig.PcParam.VibraOfs1Y;
                            config.PcParamConfig.VibraOfs1A = _engConfig.PcParam.VibraOfs1A;
                            config.PcParamConfig.VibraOfs2X = _engConfig.PcParam.VibraOfs2X;
                            config.PcParamConfig.VibraOfs2Y = _engConfig.PcParam.VibraOfs2Y;
                            config.PcParamConfig.VibraOfs2A = _engConfig.PcParam.VibraOfs2A;
                            config.PcParamConfig.VibraOfs3X = _engConfig.PcParam.VibraOfs3X;
                            config.PcParamConfig.VibraOfs3Y = _engConfig.PcParam.VibraOfs3Y;
                            config.PcParamConfig.VibraOfs3A = _engConfig.PcParam.VibraOfs3A;
                            config.PcParamConfig.VibraOfs4X = _engConfig.PcParam.VibraOfs4X;
                            config.PcParamConfig.VibraOfs4Y = _engConfig.PcParam.VibraOfs4Y;
                            config.PcParamConfig.VibraOfs4A = _engConfig.PcParam.VibraOfs4A;
                            config.PcParamConfig.VibraOfs5X = _engConfig.PcParam.VibraOfs5X;
                            config.PcParamConfig.VibraOfs5Y = _engConfig.PcParam.VibraOfs5Y;
                            config.PcParamConfig.VibraOfs5A = _engConfig.PcParam.VibraOfs5A;
                            config.PcParamConfig.VibraOfs6X = _engConfig.PcParam.VibraOfs6X;
                            config.PcParamConfig.VibraOfs6Y = _engConfig.PcParam.VibraOfs6Y;
                            config.PcParamConfig.VibraOfs6A = _engConfig.PcParam.VibraOfs6A;
                            config.PcParamConfig.VibraOfs7X = _engConfig.PcParam.VibraOfs7X;
                            config.PcParamConfig.VibraOfs7Y = _engConfig.PcParam.VibraOfs7Y;
                            config.PcParamConfig.VibraOfs7A = _engConfig.PcParam.VibraOfs7A;
                            config.PcParamConfig.VibraOfs8X = _engConfig.PcParam.VibraOfs8X;
                            config.PcParamConfig.VibraOfs8Y = _engConfig.PcParam.VibraOfs8Y;
                            config.PcParamConfig.VibraOfs8A = _engConfig.PcParam.VibraOfs8A;
                            config.PcParamConfig.VibraOfs9X = _engConfig.PcParam.VibraOfs9X;
                            config.PcParamConfig.VibraOfs9Y = _engConfig.PcParam.VibraOfs9Y;
                            config.PcParamConfig.VibraOfs9A = _engConfig.PcParam.VibraOfs9A;
                            config.PcParamConfig.VibraOfs10X = _engConfig.PcParam.VibraOfs10X;
                            config.PcParamConfig.VibraOfs10Y = _engConfig.PcParam.VibraOfs10Y;
                            config.PcParamConfig.VibraOfs10A = _engConfig.PcParam.VibraOfs10A;
                            config.PcParamConfig.VibraOfs11X = _engConfig.PcParam.VibraOfs11X;
                            config.PcParamConfig.VibraOfs11Y = _engConfig.PcParam.VibraOfs11Y;
                            config.PcParamConfig.VibraOfs11A = _engConfig.PcParam.VibraOfs11A;
                            config.PcParamConfig.VibraOfs12X = _engConfig.PcParam.VibraOfs12X;
                            config.PcParamConfig.VibraOfs12Y = _engConfig.PcParam.VibraOfs12Y;
                            config.PcParamConfig.VibraOfs12A = _engConfig.PcParam.VibraOfs12A;
                            config.PcParamConfig.CameraShootFailThresX = _engConfig.PcParam.CameraShootFailThresX;
                            config.PcParamConfig.CameraShootFailThresY = _engConfig.PcParam.CameraShootFailThresY;
                            config.PcParamConfig.CameraShootFailThresA = _engConfig.PcParam.CameraShootFailThresA;

                            #endregion
                        }
                    }
                }
                else
                {
                    _engConfig = engineAppService.Read() ?? new EngineConfig();
                    config.IsVerbose = _engConfig.IsVerbose;
                    config.RefreshRate = _engConfig.RefreshRate;
                    config.PcParamConfig.ProductionType = _engConfig.PcParam.ProductionType;
                    config.PcParamConfig.MarkingPath = _engConfig.PcParam.MarkingPath;
                    config.PcParamConfig.MarkingNamePrefix = _engConfig.PcParam.MarkingNamePrefix;
                    config.PcParamConfig.LogOutTime = _engConfig.PcParam.LogOutTime;
                    config.PcParamConfig.PowerMeterMeasurePos1X = _engConfig.PcParam.PowerMeterMeasurePos1X;
                    config.PcParamConfig.PowerMeterMeasurePos1Y = _engConfig.PcParam.PowerMeterMeasurePos1Y;
                    config.PcParamConfig.PowerMeterMeasurePos2X = _engConfig.PcParam.PowerMeterMeasurePos2X;
                    config.PcParamConfig.PowerMeterMeasurePos2Y = _engConfig.PcParam.PowerMeterMeasurePos2Y;
                    config.PcParamConfig.PowerMeterMeasurePos3X = _engConfig.PcParam.PowerMeterMeasurePos3X;
                    config.PcParamConfig.PowerMeterMeasurePos3Y = _engConfig.PcParam.PowerMeterMeasurePos3Y;
                    config.PcParamConfig.PowerMeterMeasurePos4X = _engConfig.PcParam.PowerMeterMeasurePos4X;
                    config.PcParamConfig.PowerMeterMeasurePos4Y = _engConfig.PcParam.PowerMeterMeasurePos4Y;
                    config.PcParamConfig.PowerMeterMeasurePos5X = _engConfig.PcParam.PowerMeterMeasurePos5X;
                    config.PcParamConfig.PowerMeterMeasurePos5Y = _engConfig.PcParam.PowerMeterMeasurePos5Y;
                    config.PcParamConfig.PowerMeterMeasurePos6X = _engConfig.PcParam.PowerMeterMeasurePos6X;
                    config.PcParamConfig.PowerMeterMeasurePos6Y = _engConfig.PcParam.PowerMeterMeasurePos6Y;
                    config.PcParamConfig.PowerMeterMeasurePos7X = _engConfig.PcParam.PowerMeterMeasurePos7X;
                    config.PcParamConfig.PowerMeterMeasurePos7Y = _engConfig.PcParam.PowerMeterMeasurePos7Y;
                    config.PcParamConfig.PowerMeterMeasurePos8X = _engConfig.PcParam.PowerMeterMeasurePos8X;
                    config.PcParamConfig.PowerMeterMeasurePos8Y = _engConfig.PcParam.PowerMeterMeasurePos8Y;
                    config.PcParamConfig.PowerMeterMeasurePos9X = _engConfig.PcParam.PowerMeterMeasurePos9X;
                    config.PcParamConfig.PowerMeterMeasurePos9Y = _engConfig.PcParam.PowerMeterMeasurePos9Y;
                    config.PcParamConfig.PowerMeterMeasurePos10X = _engConfig.PcParam.PowerMeterMeasurePos10X;
                    config.PcParamConfig.PowerMeterMeasurePos10Y = _engConfig.PcParam.PowerMeterMeasurePos10Y;
                    config.PcParamConfig.PowerMeterMeasurePos11X = _engConfig.PcParam.PowerMeterMeasurePos11X;
                    config.PcParamConfig.PowerMeterMeasurePos11Y = _engConfig.PcParam.PowerMeterMeasurePos11Y;
                    config.PcParamConfig.PowerMeterMeasurePos12X = _engConfig.PcParam.PowerMeterMeasurePos12X;
                    config.PcParamConfig.PowerMeterMeasurePos12Y = _engConfig.PcParam.PowerMeterMeasurePos12Y;
                    config.PcParamConfig.PowerMeterInterval = _engConfig.PcParam.PowerMeterInterval;
                    config.PcParamConfig.Laser1Power = _engConfig.PcParam.Laser1Power;
                    config.PcParamConfig.Laser1Freq = _engConfig.PcParam.Laser1Freq;
                    config.PcParamConfig.Laser2Power = _engConfig.PcParam.Laser2Power;
                    config.PcParamConfig.Laser2Freq = _engConfig.PcParam.Laser2Freq;
                    config.PcParamConfig.Laser3Power = _engConfig.PcParam.Laser3Power;
                    config.PcParamConfig.Laser3Freq = _engConfig.PcParam.Laser3Freq;
                    config.PcParamConfig.Laser4Power = _engConfig.PcParam.Laser4Power;
                    config.PcParamConfig.Laser4Freq = _engConfig.PcParam.Laser4Freq;
                    config.PcParamConfig.Laser5Power = _engConfig.PcParam.Laser5Power;
                    config.PcParamConfig.Laser5Freq = _engConfig.PcParam.Laser5Freq;
                    config.PcParamConfig.Laser6Power = _engConfig.PcParam.Laser6Power;
                    config.PcParamConfig.Laser6Freq = _engConfig.PcParam.Laser6Freq;
                    config.PcParamConfig.Laser7Power = _engConfig.PcParam.Laser7Power;
                    config.PcParamConfig.Laser7Freq = _engConfig.PcParam.Laser7Freq;
                    config.PcParamConfig.Laser8Power = _engConfig.PcParam.Laser8Power;
                    config.PcParamConfig.Laser8Freq = _engConfig.PcParam.Laser8Freq;
                    config.PcParamConfig.Laser9Power = _engConfig.PcParam.Laser9Power;
                    config.PcParamConfig.Laser9Freq = _engConfig.PcParam.Laser9Freq;
                    config.PcParamConfig.Laser10Power = _engConfig.PcParam.Laser10Power;
                    config.PcParamConfig.Laser10Freq = _engConfig.PcParam.Laser10Freq;
                    config.PcParamConfig.Laser11Power = _engConfig.PcParam.Laser11Power;
                    config.PcParamConfig.Laser11Freq = _engConfig.PcParam.Laser11Freq;
                    config.PcParamConfig.Laser12Power = _engConfig.PcParam.Laser12Power;
                    config.PcParamConfig.Laser12Freq = _engConfig.PcParam.Laser12Freq;
                    config.PcParamConfig.PowerMeterMeasureHl = _engConfig.PcParam.PowerMeterMeasureHl;
                    config.PcParamConfig.PowerMeterMeasureLl = _engConfig.PcParam.PowerMeterMeasureLl;
                    config.PcParamConfig.PowerMeterRatio = _engConfig.PcParam.PowerMeterRatio;
                    config.PcParamConfig.PowerMeterPercent = _engConfig.PcParam.PowerMeterPercent;
                    config.PcParamConfig.IsSilicaWashed = _engConfig.PcParam.IsSilicaWashed;
                    config.PcParamConfig.IsDirtyPosMarked = _engConfig.PcParam.IsDirtyPosMarked;
                    config.PcParamConfig.VibraOfs1X = _engConfig.PcParam.VibraOfs1X;
                    config.PcParamConfig.VibraOfs1Y = _engConfig.PcParam.VibraOfs1Y;
                    config.PcParamConfig.VibraOfs1A = _engConfig.PcParam.VibraOfs1A;
                    config.PcParamConfig.VibraOfs2X = _engConfig.PcParam.VibraOfs2X;
                    config.PcParamConfig.VibraOfs2Y = _engConfig.PcParam.VibraOfs2Y;
                    config.PcParamConfig.VibraOfs2A = _engConfig.PcParam.VibraOfs2A;
                    config.PcParamConfig.VibraOfs3X = _engConfig.PcParam.VibraOfs3X;
                    config.PcParamConfig.VibraOfs3Y = _engConfig.PcParam.VibraOfs3Y;
                    config.PcParamConfig.VibraOfs3A = _engConfig.PcParam.VibraOfs3A;
                    config.PcParamConfig.VibraOfs4X = _engConfig.PcParam.VibraOfs4X;
                    config.PcParamConfig.VibraOfs4Y = _engConfig.PcParam.VibraOfs4Y;
                    config.PcParamConfig.VibraOfs4A = _engConfig.PcParam.VibraOfs4A;
                    config.PcParamConfig.VibraOfs5X = _engConfig.PcParam.VibraOfs5X;
                    config.PcParamConfig.VibraOfs5Y = _engConfig.PcParam.VibraOfs5Y;
                    config.PcParamConfig.VibraOfs5A = _engConfig.PcParam.VibraOfs5A;
                    config.PcParamConfig.VibraOfs6X = _engConfig.PcParam.VibraOfs6X;
                    config.PcParamConfig.VibraOfs6Y = _engConfig.PcParam.VibraOfs6Y;
                    config.PcParamConfig.VibraOfs6A = _engConfig.PcParam.VibraOfs6A;
                    config.PcParamConfig.VibraOfs7X = _engConfig.PcParam.VibraOfs7X;
                    config.PcParamConfig.VibraOfs7Y = _engConfig.PcParam.VibraOfs7Y;
                    config.PcParamConfig.VibraOfs7A = _engConfig.PcParam.VibraOfs7A;
                    config.PcParamConfig.VibraOfs8X = _engConfig.PcParam.VibraOfs8X;
                    config.PcParamConfig.VibraOfs8Y = _engConfig.PcParam.VibraOfs8Y;
                    config.PcParamConfig.VibraOfs8A = _engConfig.PcParam.VibraOfs8A;
                    config.PcParamConfig.VibraOfs9X = _engConfig.PcParam.VibraOfs9X;
                    config.PcParamConfig.VibraOfs9Y = _engConfig.PcParam.VibraOfs9Y;
                    config.PcParamConfig.VibraOfs9A = _engConfig.PcParam.VibraOfs9A;
                    config.PcParamConfig.VibraOfs10X = _engConfig.PcParam.VibraOfs10X;
                    config.PcParamConfig.VibraOfs10Y = _engConfig.PcParam.VibraOfs10Y;
                    config.PcParamConfig.VibraOfs10A = _engConfig.PcParam.VibraOfs10A;
                    config.PcParamConfig.VibraOfs11X = _engConfig.PcParam.VibraOfs11X;
                    config.PcParamConfig.VibraOfs11Y = _engConfig.PcParam.VibraOfs11Y;
                    config.PcParamConfig.VibraOfs11A = _engConfig.PcParam.VibraOfs11A;
                    config.PcParamConfig.VibraOfs12X = _engConfig.PcParam.VibraOfs12X;
                    config.PcParamConfig.VibraOfs12Y = _engConfig.PcParam.VibraOfs12Y;
                    config.PcParamConfig.VibraOfs12A = _engConfig.PcParam.VibraOfs12A;
                    config.PcParamConfig.CameraShootFailThresX = _engConfig.PcParam.CameraShootFailThresX;
                    config.PcParamConfig.CameraShootFailThresY = _engConfig.PcParam.CameraShootFailThresY;
                    config.PcParamConfig.CameraShootFailThresA = _engConfig.PcParam.CameraShootFailThresA;
                }

            }
            catch
            {
                result = 10;
            }

            return result;
        }

        ControllerAppService controllAppService = new ControllerAppService();
        ControlConfig _controlConfig = new ControlConfig();

        private int ControllerConfigRead(DRsoft.Engine.Core.Engine.EngineConfig config)
        {
            int result = 0;
            try
            {


                Method.DicControlConfig.Clear();
                if (Method.LisRecipes.Count > 0)
                {
                    for (int j = 0; j < Method.LisRecipes.Count; j++)
                    {
                        controllAppService.metadataGuid = Method.LisRecipes[j].Id[1];
                        _controlConfig = controllAppService.Read() ?? new ControlConfig();
                        Method.DicControlConfig.Add($"{Method.LisRecipes[j].Name}1", _controlConfig);
                        Method.GuidRecipeConfig.Add($"{Method.LisRecipes[j].Name}1", Method.LisRecipes[j].Id[1]);
                        if (Method.SelectRecipe != null && Method.SelectRecipe.Equals($"{Method.LisRecipes[j].Name}"))
                        {
                            #region 数据映射

                            config.ControllerConfig.ControllerParam.Ruler11BasePos = _controlConfig.ControllerParam.Z11_RulerPos;
                            config.ControllerConfig.ControllerParam.Ruler12BasePos = _controlConfig.ControllerParam.Z12_RulerPos;
                            config.ControllerConfig.ControllerParam.Gantry1WaitPos = _controlConfig.ControllerParam.Gantry1WaitPos;
                            config.ControllerConfig.ControllerParam.Gantry1StationAGrabPos = _controlConfig.ControllerParam.Gantry1StationAGrabPos;
                            config.ControllerConfig.ControllerParam.Gantry2StationAGrabPos = _controlConfig.ControllerParam.Gantry2StationAGrabPos;
                            config.ControllerConfig.ControllerParam.Gantry1StationAMark1Pos = _controlConfig.ControllerParam.Gantry1StationAMark1Pos;
                            config.ControllerConfig.ControllerParam.Gantry1StationAMark2Pos = _controlConfig.ControllerParam.Gantry1StationAMark2Pos;
                            config.ControllerConfig.ControllerParam.Gantry1StationAMark3Pos = _controlConfig.ControllerParam.Gantry1StationAMark3Pos;
                            config.ControllerConfig.ControllerParam.Gantry1StationAMark4Pos = _controlConfig.ControllerParam.Gantry1StationAMark4Pos;
                            config.ControllerConfig.ControllerParam.Gantry1StationAMark5Pos = _controlConfig.ControllerParam.Gantry1StationAMark5Pos;
                            config.ControllerConfig.ControllerParam.Gantry1StationAMark6Pos = _controlConfig.ControllerParam.Gantry1StationAMark6Pos;
                            config.ControllerConfig.ControllerParam.Gantry1StationAMark7Pos = _controlConfig.ControllerParam.Gantry1StationAMark7Pos;
                            config.ControllerConfig.ControllerParam.Gantry1StationAMark8Pos = _controlConfig.ControllerParam.Gantry1StationAMark8Pos;
                            config.ControllerConfig.ControllerParam.Gantry2StationAMark1Pos = _controlConfig.ControllerParam.Gantry2StationAMark1Pos;
                            config.ControllerConfig.ControllerParam.Gantry2StationAMark2Pos = _controlConfig.ControllerParam.Gantry2StationAMark2Pos;
                            config.ControllerConfig.ControllerParam.Gantry2StationAMark3Pos = _controlConfig.ControllerParam.Gantry2StationAMark3Pos;
                            config.ControllerConfig.ControllerParam.Gantry2StationAMark4Pos = _controlConfig.ControllerParam.Gantry2StationAMark4Pos;
                            config.ControllerConfig.ControllerParam.Gantry2StationAMark5Pos = _controlConfig.ControllerParam.Gantry2StationAMark5Pos;
                            config.ControllerConfig.ControllerParam.Gantry2StationAMark6Pos = _controlConfig.ControllerParam.Gantry2StationAMark6Pos;
                            config.ControllerConfig.ControllerParam.Gantry2StationAMark7Pos = _controlConfig.ControllerParam.Gantry2StationAMark7Pos;
                            config.ControllerConfig.ControllerParam.Gantry2StationAMark8Pos = _controlConfig.ControllerParam.Gantry2StationAMark8Pos;
                            config.ControllerConfig.ControllerParam.Peeling1StartPos = _controlConfig.ControllerParam.Peeling1StartPos;
                            config.ControllerConfig.ControllerParam.Peeling1EndPos = _controlConfig.ControllerParam.Peeling1EndPos;
                            config.ControllerConfig.ControllerParam.Z1DownPos = _controlConfig.ControllerParam.Z1_DownPos;
                            config.ControllerConfig.ControllerParam.Z1UpPos = _controlConfig.ControllerParam.Z1_UpPos;
                            config.ControllerConfig.ControllerParam.Ruler21BasePos = _controlConfig.ControllerParam.Z21_RulerPos;
                            config.ControllerConfig.ControllerParam.Ruler22BasePos = _controlConfig.ControllerParam.Z22_RulerPos;
                            config.ControllerConfig.ControllerParam.Gantry2WaitPos = _controlConfig.ControllerParam.Gantry2WaitPos;
                            config.ControllerConfig.ControllerParam.Gantry1StationBGrabPos = _controlConfig.ControllerParam.Gantry1StationBGrabPos;
                            config.ControllerConfig.ControllerParam.Gantry2StationBGrabPos = _controlConfig.ControllerParam.Gantry2StationBGrabPos;
                            config.ControllerConfig.ControllerParam.Gantry1StationBMark1Pos = _controlConfig.ControllerParam.Gantry1StationBMark1Pos;
                            config.ControllerConfig.ControllerParam.Gantry1StationBMark2Pos = _controlConfig.ControllerParam.Gantry1StationBMark2Pos;
                            config.ControllerConfig.ControllerParam.Gantry1StationBMark3Pos = _controlConfig.ControllerParam.Gantry1StationBMark3Pos;
                            config.ControllerConfig.ControllerParam.Gantry1StationBMark4Pos = _controlConfig.ControllerParam.Gantry1StationBMark4Pos;
                            config.ControllerConfig.ControllerParam.Gantry1StationBMark5Pos = _controlConfig.ControllerParam.Gantry1StationBMark5Pos;
                            config.ControllerConfig.ControllerParam.Gantry1StationBMark6Pos = _controlConfig.ControllerParam.Gantry1StationBMark6Pos;
                            config.ControllerConfig.ControllerParam.Gantry1StationBMark6Pos = _controlConfig.ControllerParam.Gantry1StationBMark6Pos;
                            config.ControllerConfig.ControllerParam.Gantry1StationBMark7Pos = _controlConfig.ControllerParam.Gantry1StationBMark7Pos;
                            config.ControllerConfig.ControllerParam.Gantry1StationBMark7Pos = _controlConfig.ControllerParam.Gantry1StationBMark7Pos;
                            config.ControllerConfig.ControllerParam.Gantry1StationBMark8Pos = _controlConfig.ControllerParam.Gantry1StationBMark8Pos;
                            config.ControllerConfig.ControllerParam.Gantry2StationBMark1Pos = _controlConfig.ControllerParam.Gantry2StationBMark1Pos;
                            config.ControllerConfig.ControllerParam.Gantry2StationBMark2Pos = _controlConfig.ControllerParam.Gantry2StationBMark2Pos;
                            config.ControllerConfig.ControllerParam.Gantry2StationBMark3Pos = _controlConfig.ControllerParam.Gantry2StationBMark3Pos;
                            config.ControllerConfig.ControllerParam.Gantry2StationBMark4Pos = _controlConfig.ControllerParam.Gantry2StationBMark4Pos;
                            config.ControllerConfig.ControllerParam.Gantry2StationBMark5Pos = _controlConfig.ControllerParam.Gantry2StationBMark5Pos;
                            config.ControllerConfig.ControllerParam.Gantry2StationBMark6Pos = _controlConfig.ControllerParam.Gantry2StationBMark6Pos;
                            config.ControllerConfig.ControllerParam.Gantry2StationBMark7Pos = _controlConfig.ControllerParam.Gantry2StationBMark7Pos;
                            config.ControllerConfig.ControllerParam.Gantry2StationBMark8Pos = _controlConfig.ControllerParam.Gantry2StationBMark8Pos;
                            config.ControllerConfig.ControllerParam.Peeling2StartPos = _controlConfig.ControllerParam.Peeling2StartPos;
                            config.ControllerConfig.ControllerParam.Peeling2EndPos = _controlConfig.ControllerParam.Peeling2EndPos;
                            config.ControllerConfig.ControllerParam.Z2DownPos = _controlConfig.ControllerParam.Z2_DownPos;
                            config.ControllerConfig.ControllerParam.Z2UpPos = _controlConfig.ControllerParam.Z2_UpPos;
                            config.ControllerConfig.ControllerParam.CamShutter1Pos0 = _controlConfig.ControllerParam.CamShutter1Pos0;
                            config.ControllerConfig.ControllerParam.CamShutter1Pos1 = _controlConfig.ControllerParam.CamShutter1Pos1;
                            config.ControllerConfig.ControllerParam.CamShutter1Pos2 = _controlConfig.ControllerParam.CamShutter1Pos2;
                            config.ControllerConfig.ControllerParam.CamShutter1Pos3 = _controlConfig.ControllerParam.CamShutter1Pos3;
                            config.ControllerConfig.ControllerParam.CamShutter2Pos0 = _controlConfig.ControllerParam.CamShutter2Pos0;
                            config.ControllerConfig.ControllerParam.CamShutter2Pos1 = _controlConfig.ControllerParam.CamShutter2Pos1;
                            config.ControllerConfig.ControllerParam.CamShutter2Pos2 = _controlConfig.ControllerParam.CamShutter2Pos2;
                            config.ControllerConfig.ControllerParam.CamShutter2Pos3 = _controlConfig.ControllerParam.CamShutter2Pos3;
                            config.ControllerConfig.ControllerParam.UwLiftUpPos = _controlConfig.ControllerParam.UwLiftUpPos;
                            config.ControllerConfig.ControllerParam.RwLiftUpPos = _controlConfig.ControllerParam.RwLiftUpPos;
                            config.ControllerConfig.ControllerParam.ProcessTimes = _controlConfig.ControllerParam.ProcessTimes;
                            config.ControllerConfig.ControllerParam.GrabTimeOutSet = _controlConfig.ControllerParam.GrabTimeOutSet;
                            config.ControllerConfig.ControllerParam.StationAVacOkDelay = _controlConfig.ControllerParam.StationA_VacOkDelay;
                            config.ControllerConfig.ControllerParam.StationBVacOkDelay = _controlConfig.ControllerParam.StationB_VacOkDelay;
                            config.ControllerConfig.ControllerParam.StationABlowDelay = _controlConfig.ControllerParam.StationA_BlowDelay;
                            config.ControllerConfig.ControllerParam.StationBBlowDelay = _controlConfig.ControllerParam.StationB_BlowDelay;
                            config.ControllerConfig.ControllerParam.AutoLeaserMeasureNum = _controlConfig.ControllerParam.AutoLeaserMeasureNum;
                            config.ControllerConfig.ControllerParam.Gantry1PowerMeterPos = _controlConfig.ControllerParam.Gantry1PowerMeterPos;
                            config.ControllerConfig.ControllerParam.Gantry2PowerMeterPos = _controlConfig.ControllerParam.Gantry2PowerMeterPos;
                            config.ControllerConfig.ControllerParam.LeftOffset = _controlConfig.ControllerParam.LeftOffset;
                            config.ControllerConfig.ControllerParam.MidOffset = _controlConfig.ControllerParam.MidOffset;
                            config.ControllerConfig.ControllerParam.RightOffset = _controlConfig.ControllerParam.RightOffset;
                            config.ControllerConfig.ControllerParam.PowerMeterMeasurePos1 = _controlConfig.ControllerParam.PowerMeterMeasurePos1;
                            config.ControllerConfig.ControllerParam.PowerMeterMeasurePos2 = _controlConfig.ControllerParam.PowerMeterMeasurePos2;
                            config.ControllerConfig.ControllerParam.PowerMeterMeasurePos3 = _controlConfig.ControllerParam.PowerMeterMeasurePos3;
                            config.ControllerConfig.ControllerParam.PowerMeterMeasurePos4 = _controlConfig.ControllerParam.PowerMeterMeasurePos4;
                            config.ControllerConfig.ControllerParam.PowerMeterMeasurePos5 = _controlConfig.ControllerParam.PowerMeterMeasurePos5;
                            config.ControllerConfig.ControllerParam.PowerMeterMeasurePos6 = _controlConfig.ControllerParam.PowerMeterMeasurePos6;
                            config.ControllerConfig.ControllerParam.UwTorqueSet = _controlConfig.ControllerParam.UwTorqueSet;
                            config.ControllerConfig.ControllerParam.RwTorqueSet = _controlConfig.ControllerParam.RwTorqueSet;
                            config.ControllerConfig.ControllerParam.TapeLength = _controlConfig.ControllerParam.TapeLength;
                            config.ControllerConfig.ControllerParam.StationPosADelay = _controlConfig.ControllerParam.StationPosADelay;
                            config.ControllerConfig.ControllerParam.StationPosBDelay = _controlConfig.ControllerParam.StationPosBDelay;

                            config.ControllerConfig.ControllerParam.UwTorqueModeVeloLimt = _controlConfig.ControllerParam.UwTorqueModeVeloLimt;
                            config.ControllerConfig.ControllerParam.RwTorqueModeVeloLimt = _controlConfig.ControllerParam.RwTorqueModeVeloLimt;
                            config.ControllerConfig.ControllerParam.UwRadius_AnalogMax = _controlConfig.ControllerParam.UwRadius_AnalogMax;
                            config.ControllerConfig.ControllerParam.UwRadius_AnalogMin = _controlConfig.ControllerParam.UwRadius_AnalogMin;
                            config.ControllerConfig.ControllerParam.UwRadius_MeasurementMax = _controlConfig.ControllerParam.UwRadius_MeasurementMax;
                            config.ControllerConfig.ControllerParam.UwRadius_MeasurementMin = _controlConfig.ControllerParam.UwRadius_MeasurementMin;
                            config.ControllerConfig.ControllerParam.RwRadius_AnalogMax = _controlConfig.ControllerParam.RwRadius_AnalogMax;
                            config.ControllerConfig.ControllerParam.RwRadius_AnalogMin = _controlConfig.ControllerParam.RwRadius_AnalogMin;
                            config.ControllerConfig.ControllerParam.RwRadius_MeasurementMax = _controlConfig.ControllerParam.RwRadius_MeasurementMax;
                            config.ControllerConfig.ControllerParam.RwRadius_MeasurementMin = _controlConfig.ControllerParam.RwRadius_MeasurementMin;
                            config.ControllerConfig.ControllerParam.UwSteer_AnalogMax = _controlConfig.ControllerParam.UwSteer_AnalogMax;
                            config.ControllerConfig.ControllerParam.UwSteer_AnalogMin = _controlConfig.ControllerParam.UwSteer_AnalogMin;
                            config.ControllerConfig.ControllerParam.UwSteer_MeasurementMax = _controlConfig.ControllerParam.UwSteer_MeasurementMax;
                            config.ControllerConfig.ControllerParam.UwSteer_MeasurementMin = _controlConfig.ControllerParam.UwSteer_MeasurementMin;
                            config.ControllerConfig.ControllerParam.RwSteer_AnalogMax = _controlConfig.ControllerParam.RwSteer_AnalogMax;
                            config.ControllerConfig.ControllerParam.RwSteer_AnalogMin = _controlConfig.ControllerParam.RwSteer_AnalogMin;
                            config.ControllerConfig.ControllerParam.RwSteer_MeasurementMax = _controlConfig.ControllerParam.RwSteer_MeasurementMax;
                            config.ControllerConfig.ControllerParam.RwSteer_MeasurementMin = _controlConfig.ControllerParam.RwSteer_MeasurementMin;
                            config.ControllerConfig.ControllerParam.Ruler11_AnalogMax = _controlConfig.ControllerParam.Ruler11_AnalogMax;
                            config.ControllerConfig.ControllerParam.Ruler11_AnalogMin = _controlConfig.ControllerParam.Ruler11_AnalogMin;
                            config.ControllerConfig.ControllerParam.Ruler11_MeasurementMax = _controlConfig.ControllerParam.Ruler11_MeasurementMax;
                            config.ControllerConfig.ControllerParam.Ruler11_MeasurementMin = _controlConfig.ControllerParam.Ruler11_MeasurementMin;
                            config.ControllerConfig.ControllerParam.Ruler12_AnalogMax = _controlConfig.ControllerParam.Ruler12_AnalogMax;
                            config.ControllerConfig.ControllerParam.Ruler12_AnalogMin = _controlConfig.ControllerParam.Ruler12_AnalogMin;
                            config.ControllerConfig.ControllerParam.Ruler12_MeasurementMax = _controlConfig.ControllerParam.Ruler12_MeasurementMax;
                            config.ControllerConfig.ControllerParam.Ruler12_MeasurementMin = _controlConfig.ControllerParam.Ruler12_MeasurementMin;
                            config.ControllerConfig.ControllerParam.Ruler21_AnalogMax = _controlConfig.ControllerParam.Ruler21_AnalogMax;
                            config.ControllerConfig.ControllerParam.Ruler21_AnalogMin = _controlConfig.ControllerParam.Ruler21_AnalogMin;
                            config.ControllerConfig.ControllerParam.Ruler21_MeasurementMax = _controlConfig.ControllerParam.Ruler21_MeasurementMax;
                            config.ControllerConfig.ControllerParam.Ruler21_MeasurementMin = _controlConfig.ControllerParam.Ruler21_MeasurementMin;
                            config.ControllerConfig.ControllerParam.Ruler22_AnalogMax = _controlConfig.ControllerParam.Ruler22_AnalogMax;
                            config.ControllerConfig.ControllerParam.Ruler22_AnalogMin = _controlConfig.ControllerParam.Ruler22_AnalogMin;
                            config.ControllerConfig.ControllerParam.Ruler22_MeasurementMax = _controlConfig.ControllerParam.Ruler22_MeasurementMax;
                            config.ControllerConfig.ControllerParam.Ruler22_MeasurementMin = _controlConfig.ControllerParam.Ruler22_MeasurementMin;
                            config.ControllerConfig.ControllerParam.Align11WaitPos = _controlConfig.ControllerParam.Align11WaitPos;
                            config.ControllerConfig.ControllerParam.Align12WaitPos = _controlConfig.ControllerParam.Align12WaitPos;
                            config.ControllerConfig.ControllerParam.Align21WaitPos = _controlConfig.ControllerParam.Align21WaitPos;
                            config.ControllerConfig.ControllerParam.Align22WaitPos = _controlConfig.ControllerParam.Align22WaitPos;
                            config.ControllerConfig.ControllerParam.Z1_PeelingPos = _controlConfig.ControllerParam.Z1_PeelingPos;
                            config.ControllerConfig.ControllerParam.Z2_PeelingPos = _controlConfig.ControllerParam.Z2_PeelingPos;
                            config.ControllerConfig.ControllerParam.Gantry11BasePos = _controlConfig.ControllerParam.Gantry11BasePos;
                            config.ControllerConfig.ControllerParam.Gantry12BasePos = _controlConfig.ControllerParam.Gantry12BasePos;
                            config.ControllerConfig.ControllerParam.Gantry21BasePos = _controlConfig.ControllerParam.Gantry21BasePos;
                            config.ControllerConfig.ControllerParam.Gantry22BasePos = _controlConfig.ControllerParam.Gantry22BasePos;

                            config.ControllerConfig.ParaAxisGantry11.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisGantry11.HomeOffset;
                            config.ControllerConfig.ParaAxisGantry11.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisGantry11.RelDistance;
                            config.ControllerConfig.ParaAxisGantry11.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisGantry11.AbsPosition1;
                            config.ControllerConfig.ParaAxisGantry11.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisGantry11.AbsPosition2;
                            config.ControllerConfig.ParaAxisGantry11.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisGantry11.HomeVelo;
                            config.ControllerConfig.ParaAxisGantry11.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisGantry11.ManualVelo;
                            config.ControllerConfig.ParaAxisGantry11.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisGantry11.WorkVelo;
                            config.ControllerConfig.ParaAxisGantry11.sT_AxisParameter.Acc = _controlConfig.ParaAxisGantry11.Acc;
                            config.ControllerConfig.ParaAxisGantry11.sT_AxisParameter.Dec = _controlConfig.ParaAxisGantry11.Dec;
                            config.ControllerConfig.ParaAxisGantry12.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisGantry12.HomeOffset;
                            config.ControllerConfig.ParaAxisGantry12.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisGantry12.RelDistance;
                            config.ControllerConfig.ParaAxisGantry12.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisGantry12.AbsPosition1;
                            config.ControllerConfig.ParaAxisGantry12.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisGantry12.AbsPosition2;
                            config.ControllerConfig.ParaAxisGantry12.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisGantry12.HomeVelo;
                            config.ControllerConfig.ParaAxisGantry12.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisGantry12.ManualVelo;
                            config.ControllerConfig.ParaAxisGantry12.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisGantry12.WorkVelo;
                            config.ControllerConfig.ParaAxisGantry12.sT_AxisParameter.Acc = _controlConfig.ParaAxisGantry12.Acc;
                            config.ControllerConfig.ParaAxisGantry12.sT_AxisParameter.Dec = _controlConfig.ParaAxisGantry12.Dec;
                            config.ControllerConfig.ParaAxisGantry21.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisGantry21.HomeOffset;
                            config.ControllerConfig.ParaAxisGantry21.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisGantry21.RelDistance;
                            config.ControllerConfig.ParaAxisGantry21.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisGantry21.AbsPosition1;
                            config.ControllerConfig.ParaAxisGantry21.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisGantry21.AbsPosition2;
                            config.ControllerConfig.ParaAxisGantry21.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisGantry21.HomeVelo;
                            config.ControllerConfig.ParaAxisGantry21.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisGantry21.ManualVelo;
                            config.ControllerConfig.ParaAxisGantry21.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisGantry21.WorkVelo;
                            config.ControllerConfig.ParaAxisGantry21.sT_AxisParameter.Acc = _controlConfig.ParaAxisGantry21.Acc;
                            config.ControllerConfig.ParaAxisGantry21.sT_AxisParameter.Dec = _controlConfig.ParaAxisGantry21.Dec;
                            config.ControllerConfig.ParaAxisGantry22.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisGantry22.HomeOffset;
                            config.ControllerConfig.ParaAxisGantry22.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisGantry22.RelDistance;
                            config.ControllerConfig.ParaAxisGantry22.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisGantry22.AbsPosition1;
                            config.ControllerConfig.ParaAxisGantry22.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisGantry22.AbsPosition2;
                            config.ControllerConfig.ParaAxisGantry22.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisGantry22.HomeVelo;
                            config.ControllerConfig.ParaAxisGantry22.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisGantry22.ManualVelo;
                            config.ControllerConfig.ParaAxisGantry22.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisGantry22.WorkVelo;
                            config.ControllerConfig.ParaAxisGantry22.sT_AxisParameter.Acc = _controlConfig.ParaAxisGantry22.Acc;
                            config.ControllerConfig.ParaAxisGantry22.sT_AxisParameter.Dec = _controlConfig.ParaAxisGantry22.Dec;
                            config.ControllerConfig.ParaAxisAlign11.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisAlign11.HomeOffset;
                            config.ControllerConfig.ParaAxisAlign11.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisAlign11.RelDistance;
                            config.ControllerConfig.ParaAxisAlign11.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisAlign11.AbsPosition1;
                            config.ControllerConfig.ParaAxisAlign11.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisAlign11.AbsPosition2;
                            config.ControllerConfig.ParaAxisAlign11.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisAlign11.HomeVelo;
                            config.ControllerConfig.ParaAxisAlign11.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisAlign11.ManualVelo;
                            config.ControllerConfig.ParaAxisAlign11.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisAlign11.WorkVelo;
                            config.ControllerConfig.ParaAxisAlign11.sT_AxisParameter.Acc = _controlConfig.ParaAxisAlign11.Acc;
                            config.ControllerConfig.ParaAxisAlign11.sT_AxisParameter.Dec = _controlConfig.ParaAxisAlign11.Dec;
                            config.ControllerConfig.ParaAxisAlign12.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisAlign12.HomeOffset;
                            config.ControllerConfig.ParaAxisAlign12.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisAlign12.RelDistance;
                            config.ControllerConfig.ParaAxisAlign12.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisAlign12.AbsPosition1;
                            config.ControllerConfig.ParaAxisAlign12.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisAlign12.AbsPosition2;
                            config.ControllerConfig.ParaAxisAlign12.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisAlign12.HomeVelo;
                            config.ControllerConfig.ParaAxisAlign12.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisAlign12.ManualVelo;
                            config.ControllerConfig.ParaAxisAlign12.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisAlign12.WorkVelo;
                            config.ControllerConfig.ParaAxisAlign12.sT_AxisParameter.Acc = _controlConfig.ParaAxisAlign12.Acc;
                            config.ControllerConfig.ParaAxisAlign12.sT_AxisParameter.Dec = _controlConfig.ParaAxisAlign12.Dec;
                            config.ControllerConfig.ParaAxisAlign21.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisAlign21.HomeOffset;
                            config.ControllerConfig.ParaAxisAlign21.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisAlign21.RelDistance;
                            config.ControllerConfig.ParaAxisAlign21.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisAlign21.AbsPosition1;
                            config.ControllerConfig.ParaAxisAlign21.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisAlign21.AbsPosition2;
                            config.ControllerConfig.ParaAxisAlign21.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisAlign21.HomeVelo;
                            config.ControllerConfig.ParaAxisAlign21.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisAlign21.ManualVelo;
                            config.ControllerConfig.ParaAxisAlign21.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisAlign21.WorkVelo;
                            config.ControllerConfig.ParaAxisAlign21.sT_AxisParameter.Acc = _controlConfig.ParaAxisAlign21.Acc;
                            config.ControllerConfig.ParaAxisAlign21.sT_AxisParameter.Dec = _controlConfig.ParaAxisAlign21.Dec;
                            config.ControllerConfig.ParaAxisAlign22.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisAlign22.HomeOffset;
                            config.ControllerConfig.ParaAxisAlign22.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisAlign22.RelDistance;
                            config.ControllerConfig.ParaAxisAlign22.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisAlign22.AbsPosition1;
                            config.ControllerConfig.ParaAxisAlign22.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisAlign22.AbsPosition2;
                            config.ControllerConfig.ParaAxisAlign22.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisAlign22.HomeVelo;
                            config.ControllerConfig.ParaAxisAlign22.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisAlign22.ManualVelo;
                            config.ControllerConfig.ParaAxisAlign22.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisAlign22.WorkVelo;
                            config.ControllerConfig.ParaAxisAlign22.sT_AxisParameter.Acc = _controlConfig.ParaAxisAlign22.Acc;
                            config.ControllerConfig.ParaAxisAlign22.sT_AxisParameter.Dec = _controlConfig.ParaAxisAlign22.Dec;
                            config.ControllerConfig.ParaAxisCamShutter1.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisCamShutter1.HomeOffset;
                            config.ControllerConfig.ParaAxisCamShutter1.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisCamShutter1.RelDistance;
                            config.ControllerConfig.ParaAxisCamShutter1.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisCamShutter1.AbsPosition1;
                            config.ControllerConfig.ParaAxisCamShutter1.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisCamShutter1.AbsPosition2;
                            config.ControllerConfig.ParaAxisCamShutter1.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisCamShutter1.HomeVelo;
                            config.ControllerConfig.ParaAxisCamShutter1.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisCamShutter1.ManualVelo;
                            config.ControllerConfig.ParaAxisCamShutter1.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisCamShutter1.WorkVelo;
                            config.ControllerConfig.ParaAxisCamShutter1.sT_AxisParameter.Acc = _controlConfig.ParaAxisCamShutter1.Acc;
                            config.ControllerConfig.ParaAxisCamShutter1.sT_AxisParameter.Dec = _controlConfig.ParaAxisCamShutter1.Dec;
                            config.ControllerConfig.ParaAxisCamShutter2.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisCamShutter2.HomeOffset;
                            config.ControllerConfig.ParaAxisCamShutter2.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisCamShutter2.RelDistance;
                            config.ControllerConfig.ParaAxisCamShutter2.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisCamShutter2.AbsPosition1;
                            config.ControllerConfig.ParaAxisCamShutter2.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisCamShutter2.AbsPosition2;
                            config.ControllerConfig.ParaAxisCamShutter2.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisCamShutter2.HomeVelo;
                            config.ControllerConfig.ParaAxisCamShutter2.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisCamShutter2.ManualVelo;
                            config.ControllerConfig.ParaAxisCamShutter2.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisCamShutter2.WorkVelo;
                            config.ControllerConfig.ParaAxisCamShutter2.sT_AxisParameter.Acc = _controlConfig.ParaAxisCamShutter2.Acc;
                            config.ControllerConfig.ParaAxisCamShutter2.sT_AxisParameter.Dec = _controlConfig.ParaAxisCamShutter2.Dec;
                            config.ControllerConfig.ParaAxisZ1.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisZ1.HomeOffset;
                            config.ControllerConfig.ParaAxisZ1.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisZ1.RelDistance;
                            config.ControllerConfig.ParaAxisZ1.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisZ1.AbsPosition1;
                            config.ControllerConfig.ParaAxisZ1.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisZ1.AbsPosition2;
                            config.ControllerConfig.ParaAxisZ1.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisZ1.HomeVelo;
                            config.ControllerConfig.ParaAxisZ1.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisZ1.ManualVelo;
                            config.ControllerConfig.ParaAxisZ1.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisZ1.WorkVelo;
                            config.ControllerConfig.ParaAxisZ1.sT_AxisParameter.Acc = _controlConfig.ParaAxisZ1.Acc;
                            config.ControllerConfig.ParaAxisZ1.sT_AxisParameter.Dec = _controlConfig.ParaAxisZ1.Dec;
                            config.ControllerConfig.ParaAxisZ2.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisZ2.HomeOffset;
                            config.ControllerConfig.ParaAxisZ2.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisZ2.RelDistance;
                            config.ControllerConfig.ParaAxisZ2.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisZ2.AbsPosition1;
                            config.ControllerConfig.ParaAxisZ2.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisZ2.AbsPosition2;
                            config.ControllerConfig.ParaAxisZ2.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisZ2.HomeVelo;
                            config.ControllerConfig.ParaAxisZ2.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisZ2.ManualVelo;
                            config.ControllerConfig.ParaAxisZ2.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisZ2.WorkVelo;
                            config.ControllerConfig.ParaAxisZ2.sT_AxisParameter.Acc = _controlConfig.ParaAxisZ2.Acc;
                            config.ControllerConfig.ParaAxisZ2.sT_AxisParameter.Dec = _controlConfig.ParaAxisZ2.Dec;
                            config.ControllerConfig.ParaAxisUwLift.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisUwLift.HomeOffset;
                            config.ControllerConfig.ParaAxisUwLift.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisUwLift.RelDistance;
                            config.ControllerConfig.ParaAxisUwLift.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisUwLift.AbsPosition1;
                            config.ControllerConfig.ParaAxisUwLift.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisUwLift.AbsPosition2;
                            config.ControllerConfig.ParaAxisUwLift.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisUwLift.HomeVelo;
                            config.ControllerConfig.ParaAxisUwLift.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisUwLift.ManualVelo;
                            config.ControllerConfig.ParaAxisUwLift.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisUwLift.WorkVelo;
                            config.ControllerConfig.ParaAxisUwLift.sT_AxisParameter.Acc = _controlConfig.ParaAxisUwLift.Acc;
                            config.ControllerConfig.ParaAxisUwLift.sT_AxisParameter.Dec = _controlConfig.ParaAxisUwLift.Dec;
                            config.ControllerConfig.ParaAxisUw.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisUw.HomeOffset;
                            config.ControllerConfig.ParaAxisUw.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisUw.RelDistance;
                            config.ControllerConfig.ParaAxisUw.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisUw.AbsPosition1;
                            config.ControllerConfig.ParaAxisUw.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisUw.AbsPosition2;
                            config.ControllerConfig.ParaAxisUw.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisUw.HomeVelo;
                            config.ControllerConfig.ParaAxisUw.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisUw.ManualVelo;
                            config.ControllerConfig.ParaAxisUw.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisUw.WorkVelo;
                            config.ControllerConfig.ParaAxisUw.sT_AxisParameter.Acc = _controlConfig.ParaAxisUw.Acc;
                            config.ControllerConfig.ParaAxisUw.sT_AxisParameter.Dec = _controlConfig.ParaAxisUw.Dec;
                            config.ControllerConfig.ParaAxisRwLift.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisRwLift.HomeOffset;
                            config.ControllerConfig.ParaAxisRwLift.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisRwLift.RelDistance;
                            config.ControllerConfig.ParaAxisRwLift.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisRwLift.AbsPosition1;
                            config.ControllerConfig.ParaAxisRwLift.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisRwLift.AbsPosition2;
                            config.ControllerConfig.ParaAxisRwLift.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisRwLift.HomeVelo;
                            config.ControllerConfig.ParaAxisRwLift.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisRwLift.ManualVelo;
                            config.ControllerConfig.ParaAxisRwLift.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisRwLift.WorkVelo;
                            config.ControllerConfig.ParaAxisRwLift.sT_AxisParameter.Acc = _controlConfig.ParaAxisRwLift.Acc;
                            config.ControllerConfig.ParaAxisRwLift.sT_AxisParameter.Dec = _controlConfig.ParaAxisRwLift.Dec;
                            config.ControllerConfig.ParaAxisRw.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisRw.HomeOffset;
                            config.ControllerConfig.ParaAxisRw.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisRw.RelDistance;
                            config.ControllerConfig.ParaAxisRw.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisRw.AbsPosition1;
                            config.ControllerConfig.ParaAxisRw.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisRw.AbsPosition2;
                            config.ControllerConfig.ParaAxisRw.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisRw.HomeVelo;
                            config.ControllerConfig.ParaAxisRw.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisRw.ManualVelo;
                            config.ControllerConfig.ParaAxisRw.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisRw.WorkVelo;
                            config.ControllerConfig.ParaAxisRw.sT_AxisParameter.Acc = _controlConfig.ParaAxisRw.Acc;
                            config.ControllerConfig.ParaAxisRw.sT_AxisParameter.Dec = _controlConfig.ParaAxisRw.Dec;
                            config.ControllerConfig.ParaAxisClean.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisClean.HomeOffset;
                            config.ControllerConfig.ParaAxisClean.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisClean.RelDistance;
                            config.ControllerConfig.ParaAxisClean.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisClean.AbsPosition1;
                            config.ControllerConfig.ParaAxisClean.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisClean.AbsPosition2;
                            config.ControllerConfig.ParaAxisClean.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisClean.HomeVelo;
                            config.ControllerConfig.ParaAxisClean.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisClean.ManualVelo;
                            config.ControllerConfig.ParaAxisClean.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisClean.WorkVelo;
                            config.ControllerConfig.ParaAxisClean.sT_AxisParameter.Acc = _controlConfig.ParaAxisClean.Acc;
                            config.ControllerConfig.ParaAxisClean.sT_AxisParameter.Dec = _controlConfig.ParaAxisClean.Dec;
                            config.ControllerConfig.ParaAxisPowerMeter.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisPowerMeter.HomeOffset;
                            config.ControllerConfig.ParaAxisPowerMeter.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisPowerMeter.RelDistance;
                            config.ControllerConfig.ParaAxisPowerMeter.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisPowerMeter.AbsPosition1;
                            config.ControllerConfig.ParaAxisPowerMeter.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisPowerMeter.AbsPosition2;
                            config.ControllerConfig.ParaAxisPowerMeter.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisPowerMeter.HomeVelo;
                            config.ControllerConfig.ParaAxisPowerMeter.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisPowerMeter.ManualVelo;
                            config.ControllerConfig.ParaAxisPowerMeter.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisPowerMeter.WorkVelo;
                            config.ControllerConfig.ParaAxisPowerMeter.sT_AxisParameter.Acc = _controlConfig.ParaAxisPowerMeter.Acc;
                            config.ControllerConfig.ParaAxisPowerMeter.sT_AxisParameter.Dec = _controlConfig.ParaAxisPowerMeter.Dec;
                            config.ControllerConfig.ParaAxisUwSteer.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisUwSteer.HomeOffset;
                            config.ControllerConfig.ParaAxisUwSteer.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisUwSteer.RelDistance;
                            config.ControllerConfig.ParaAxisUwSteer.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisUwSteer.AbsPosition1;
                            config.ControllerConfig.ParaAxisUwSteer.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisUwSteer.AbsPosition2;
                            config.ControllerConfig.ParaAxisUwSteer.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisUwSteer.HomeVelo;
                            config.ControllerConfig.ParaAxisUwSteer.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisUwSteer.ManualVelo;
                            config.ControllerConfig.ParaAxisUwSteer.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisUwSteer.WorkVelo;
                            config.ControllerConfig.ParaAxisUwSteer.sT_AxisParameter.Acc = _controlConfig.ParaAxisUwSteer.Acc;
                            config.ControllerConfig.ParaAxisUwSteer.sT_AxisParameter.Dec = _controlConfig.ParaAxisUwSteer.Dec;
                            config.ControllerConfig.ParaAxisPeeling1.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisPeeling1.HomeOffset;
                            config.ControllerConfig.ParaAxisPeeling1.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisPeeling1.RelDistance;
                            config.ControllerConfig.ParaAxisPeeling1.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisPeeling1.AbsPosition1;
                            config.ControllerConfig.ParaAxisPeeling1.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisPeeling1.AbsPosition2;
                            config.ControllerConfig.ParaAxisPeeling1.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisPeeling1.HomeVelo;
                            config.ControllerConfig.ParaAxisPeeling1.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisPeeling1.ManualVelo;
                            config.ControllerConfig.ParaAxisPeeling1.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisPeeling1.WorkVelo;
                            config.ControllerConfig.ParaAxisPeeling1.sT_AxisParameter.Acc = _controlConfig.ParaAxisPeeling1.Acc;
                            config.ControllerConfig.ParaAxisPeeling1.sT_AxisParameter.Dec = _controlConfig.ParaAxisPeeling1.Dec;
                            config.ControllerConfig.ParaAxisStationABelt.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisStationABelt.HomeOffset;
                            config.ControllerConfig.ParaAxisStationABelt.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisStationABelt.RelDistance;
                            config.ControllerConfig.ParaAxisStationABelt.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisStationABelt.AbsPosition1;
                            config.ControllerConfig.ParaAxisStationABelt.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisStationABelt.AbsPosition2;
                            config.ControllerConfig.ParaAxisStationABelt.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisStationABelt.HomeVelo;
                            config.ControllerConfig.ParaAxisStationABelt.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisStationABelt.ManualVelo;
                            config.ControllerConfig.ParaAxisStationABelt.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisStationABelt.WorkVelo;
                            config.ControllerConfig.ParaAxisStationABelt.sT_AxisParameter.Acc = _controlConfig.ParaAxisStationABelt.Acc;
                            config.ControllerConfig.ParaAxisStationABelt.sT_AxisParameter.Dec = _controlConfig.ParaAxisStationABelt.Dec;
                            config.ControllerConfig.ParaAxisPeeling2.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisPeeling2.HomeOffset;
                            config.ControllerConfig.ParaAxisPeeling2.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisPeeling2.RelDistance;
                            config.ControllerConfig.ParaAxisPeeling2.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisPeeling2.AbsPosition1;
                            config.ControllerConfig.ParaAxisPeeling2.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisPeeling2.AbsPosition2;
                            config.ControllerConfig.ParaAxisPeeling2.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisPeeling2.HomeVelo;
                            config.ControllerConfig.ParaAxisPeeling2.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisPeeling2.ManualVelo;
                            config.ControllerConfig.ParaAxisPeeling2.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisPeeling2.WorkVelo;
                            config.ControllerConfig.ParaAxisPeeling2.sT_AxisParameter.Acc = _controlConfig.ParaAxisPeeling2.Acc;
                            config.ControllerConfig.ParaAxisPeeling2.sT_AxisParameter.Dec = _controlConfig.ParaAxisPeeling2.Dec;
                            config.ControllerConfig.ParaAxisStationBBelt.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisStationBBelt.HomeOffset;
                            config.ControllerConfig.ParaAxisStationBBelt.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisStationBBelt.RelDistance;
                            config.ControllerConfig.ParaAxisStationBBelt.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisStationBBelt.AbsPosition1;
                            config.ControllerConfig.ParaAxisStationBBelt.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisStationBBelt.AbsPosition2;
                            config.ControllerConfig.ParaAxisStationBBelt.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisStationBBelt.HomeVelo;
                            config.ControllerConfig.ParaAxisStationBBelt.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisStationBBelt.ManualVelo;
                            config.ControllerConfig.ParaAxisStationBBelt.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisStationBBelt.WorkVelo;
                            config.ControllerConfig.ParaAxisStationBBelt.sT_AxisParameter.Acc = _controlConfig.ParaAxisStationBBelt.Acc;
                            config.ControllerConfig.ParaAxisStationBBelt.sT_AxisParameter.Dec = _controlConfig.ParaAxisStationBBelt.Dec;
                            config.ControllerConfig.ParaAxisRwSteer.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisRwSteer.HomeOffset;
                            config.ControllerConfig.ParaAxisRwSteer.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisRwSteer.RelDistance;
                            config.ControllerConfig.ParaAxisRwSteer.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisRwSteer.AbsPosition1;
                            config.ControllerConfig.ParaAxisRwSteer.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisRwSteer.AbsPosition2;
                            config.ControllerConfig.ParaAxisRwSteer.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisRwSteer.HomeVelo;
                            config.ControllerConfig.ParaAxisRwSteer.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisRwSteer.ManualVelo;
                            config.ControllerConfig.ParaAxisRwSteer.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisRwSteer.WorkVelo;
                            config.ControllerConfig.ParaAxisRwSteer.sT_AxisParameter.Acc = _controlConfig.ParaAxisRwSteer.Acc;
                            config.ControllerConfig.ParaAxisRwSteer.sT_AxisParameter.Dec = _controlConfig.ParaAxisRwSteer.Dec;

                            #endregion
                        }
                    }
                }
                else
                {
                    _controlConfig = controllAppService.Read() ?? new ControlConfig();
                    config.ControllerConfig.ControllerParam.Ruler11BasePos = _controlConfig.ControllerParam.Z11_RulerPos;
                    config.ControllerConfig.ControllerParam.Ruler12BasePos = _controlConfig.ControllerParam.Z12_RulerPos;
                    config.ControllerConfig.ControllerParam.Gantry1WaitPos = _controlConfig.ControllerParam.Gantry1WaitPos;
                    config.ControllerConfig.ControllerParam.Gantry1StationAGrabPos = _controlConfig.ControllerParam.Gantry1StationAGrabPos;
                    config.ControllerConfig.ControllerParam.Gantry2StationAGrabPos = _controlConfig.ControllerParam.Gantry2StationAGrabPos;
                    config.ControllerConfig.ControllerParam.Gantry1StationAMark1Pos = _controlConfig.ControllerParam.Gantry1StationAMark1Pos;
                    config.ControllerConfig.ControllerParam.Gantry1StationAMark2Pos = _controlConfig.ControllerParam.Gantry1StationAMark2Pos;
                    config.ControllerConfig.ControllerParam.Gantry1StationAMark3Pos = _controlConfig.ControllerParam.Gantry1StationAMark3Pos;
                    config.ControllerConfig.ControllerParam.Gantry1StationAMark4Pos = _controlConfig.ControllerParam.Gantry1StationAMark4Pos;
                    config.ControllerConfig.ControllerParam.Gantry1StationAMark5Pos = _controlConfig.ControllerParam.Gantry1StationAMark5Pos;
                    config.ControllerConfig.ControllerParam.Gantry1StationAMark6Pos = _controlConfig.ControllerParam.Gantry1StationAMark6Pos;
                    config.ControllerConfig.ControllerParam.Gantry1StationAMark7Pos = _controlConfig.ControllerParam.Gantry1StationAMark7Pos;
                    config.ControllerConfig.ControllerParam.Gantry1StationAMark8Pos = _controlConfig.ControllerParam.Gantry1StationAMark8Pos;
                    config.ControllerConfig.ControllerParam.Gantry2StationAMark1Pos = _controlConfig.ControllerParam.Gantry2StationAMark1Pos;
                    config.ControllerConfig.ControllerParam.Gantry2StationAMark2Pos = _controlConfig.ControllerParam.Gantry2StationAMark2Pos;
                    config.ControllerConfig.ControllerParam.Gantry2StationAMark3Pos = _controlConfig.ControllerParam.Gantry2StationAMark3Pos;
                    config.ControllerConfig.ControllerParam.Gantry2StationAMark4Pos = _controlConfig.ControllerParam.Gantry2StationAMark4Pos;
                    config.ControllerConfig.ControllerParam.Gantry2StationAMark5Pos = _controlConfig.ControllerParam.Gantry2StationAMark5Pos;
                    config.ControllerConfig.ControllerParam.Gantry2StationAMark6Pos = _controlConfig.ControllerParam.Gantry2StationAMark6Pos;
                    config.ControllerConfig.ControllerParam.Gantry2StationAMark7Pos = _controlConfig.ControllerParam.Gantry2StationAMark7Pos;
                    config.ControllerConfig.ControllerParam.Gantry2StationAMark8Pos = _controlConfig.ControllerParam.Gantry2StationAMark8Pos;
                    config.ControllerConfig.ControllerParam.Peeling1StartPos = _controlConfig.ControllerParam.Peeling1StartPos;
                    config.ControllerConfig.ControllerParam.Peeling1EndPos = _controlConfig.ControllerParam.Peeling1EndPos;
                    config.ControllerConfig.ControllerParam.Z1DownPos = _controlConfig.ControllerParam.Z1_DownPos;
                    config.ControllerConfig.ControllerParam.Z1UpPos = _controlConfig.ControllerParam.Z1_UpPos;
                    config.ControllerConfig.ControllerParam.Ruler21BasePos = _controlConfig.ControllerParam.Z21_RulerPos;
                    config.ControllerConfig.ControllerParam.Ruler22BasePos = _controlConfig.ControllerParam.Z22_RulerPos;
                    config.ControllerConfig.ControllerParam.Gantry2WaitPos = _controlConfig.ControllerParam.Gantry2WaitPos;
                    config.ControllerConfig.ControllerParam.Gantry1StationBGrabPos = _controlConfig.ControllerParam.Gantry1StationBGrabPos;
                    config.ControllerConfig.ControllerParam.Gantry2StationBGrabPos = _controlConfig.ControllerParam.Gantry2StationBGrabPos;
                    config.ControllerConfig.ControllerParam.Gantry1StationBMark1Pos = _controlConfig.ControllerParam.Gantry1StationBMark1Pos;
                    config.ControllerConfig.ControllerParam.Gantry1StationBMark2Pos = _controlConfig.ControllerParam.Gantry1StationBMark2Pos;
                    config.ControllerConfig.ControllerParam.Gantry1StationBMark3Pos = _controlConfig.ControllerParam.Gantry1StationBMark3Pos;
                    config.ControllerConfig.ControllerParam.Gantry1StationBMark4Pos = _controlConfig.ControllerParam.Gantry1StationBMark4Pos;
                    config.ControllerConfig.ControllerParam.Gantry1StationBMark5Pos = _controlConfig.ControllerParam.Gantry1StationBMark5Pos;
                    config.ControllerConfig.ControllerParam.Gantry1StationBMark6Pos = _controlConfig.ControllerParam.Gantry1StationBMark6Pos;
                    config.ControllerConfig.ControllerParam.Gantry1StationBMark6Pos = _controlConfig.ControllerParam.Gantry1StationBMark6Pos;
                    config.ControllerConfig.ControllerParam.Gantry1StationBMark7Pos = _controlConfig.ControllerParam.Gantry1StationBMark7Pos;
                    config.ControllerConfig.ControllerParam.Gantry1StationBMark7Pos = _controlConfig.ControllerParam.Gantry1StationBMark7Pos;
                    config.ControllerConfig.ControllerParam.Gantry1StationBMark8Pos = _controlConfig.ControllerParam.Gantry1StationBMark8Pos;
                    config.ControllerConfig.ControllerParam.Gantry2StationBMark1Pos = _controlConfig.ControllerParam.Gantry2StationBMark1Pos;
                    config.ControllerConfig.ControllerParam.Gantry2StationBMark2Pos = _controlConfig.ControllerParam.Gantry2StationBMark2Pos;
                    config.ControllerConfig.ControllerParam.Gantry2StationBMark3Pos = _controlConfig.ControllerParam.Gantry2StationBMark3Pos;
                    config.ControllerConfig.ControllerParam.Gantry2StationBMark4Pos = _controlConfig.ControllerParam.Gantry2StationBMark4Pos;
                    config.ControllerConfig.ControllerParam.Gantry2StationBMark5Pos = _controlConfig.ControllerParam.Gantry2StationBMark5Pos;
                    config.ControllerConfig.ControllerParam.Gantry2StationBMark6Pos = _controlConfig.ControllerParam.Gantry2StationBMark6Pos;
                    config.ControllerConfig.ControllerParam.Gantry2StationBMark7Pos = _controlConfig.ControllerParam.Gantry2StationBMark7Pos;
                    config.ControllerConfig.ControllerParam.Gantry2StationBMark8Pos = _controlConfig.ControllerParam.Gantry2StationBMark8Pos;
                    config.ControllerConfig.ControllerParam.Peeling2StartPos = _controlConfig.ControllerParam.Peeling2StartPos;
                    config.ControllerConfig.ControllerParam.Peeling2EndPos = _controlConfig.ControllerParam.Peeling2EndPos;
                    config.ControllerConfig.ControllerParam.Z2DownPos = _controlConfig.ControllerParam.Z2_DownPos;
                    config.ControllerConfig.ControllerParam.Z2UpPos = _controlConfig.ControllerParam.Z2_UpPos;
                    config.ControllerConfig.ControllerParam.CamShutter1Pos0 = _controlConfig.ControllerParam.CamShutter1Pos0;
                    config.ControllerConfig.ControllerParam.CamShutter1Pos1 = _controlConfig.ControllerParam.CamShutter1Pos1;
                    config.ControllerConfig.ControllerParam.CamShutter1Pos2 = _controlConfig.ControllerParam.CamShutter1Pos2;
                    config.ControllerConfig.ControllerParam.CamShutter1Pos3 = _controlConfig.ControllerParam.CamShutter1Pos3;
                    config.ControllerConfig.ControllerParam.CamShutter2Pos0 = _controlConfig.ControllerParam.CamShutter2Pos0;
                    config.ControllerConfig.ControllerParam.CamShutter2Pos1 = _controlConfig.ControllerParam.CamShutter2Pos1;
                    config.ControllerConfig.ControllerParam.CamShutter2Pos2 = _controlConfig.ControllerParam.CamShutter2Pos2;
                    config.ControllerConfig.ControllerParam.CamShutter2Pos3 = _controlConfig.ControllerParam.CamShutter2Pos3;
                    config.ControllerConfig.ControllerParam.UwLiftUpPos = _controlConfig.ControllerParam.UwLiftUpPos;
                    config.ControllerConfig.ControllerParam.RwLiftUpPos = _controlConfig.ControllerParam.RwLiftUpPos;
                    config.ControllerConfig.ControllerParam.ProcessTimes = _controlConfig.ControllerParam.ProcessTimes;
                    config.ControllerConfig.ControllerParam.GrabTimeOutSet = _controlConfig.ControllerParam.GrabTimeOutSet;
                    config.ControllerConfig.ControllerParam.StationAVacOkDelay = _controlConfig.ControllerParam.StationA_VacOkDelay;
                    config.ControllerConfig.ControllerParam.StationBVacOkDelay = _controlConfig.ControllerParam.StationB_VacOkDelay;
                    config.ControllerConfig.ControllerParam.StationABlowDelay = _controlConfig.ControllerParam.StationA_BlowDelay;
                    config.ControllerConfig.ControllerParam.StationBBlowDelay = _controlConfig.ControllerParam.StationB_BlowDelay;
                    config.ControllerConfig.ControllerParam.AutoLeaserMeasureNum = _controlConfig.ControllerParam.AutoLeaserMeasureNum;
                    config.ControllerConfig.ControllerParam.Gantry1PowerMeterPos = _controlConfig.ControllerParam.Gantry1PowerMeterPos;
                    config.ControllerConfig.ControllerParam.Gantry2PowerMeterPos = _controlConfig.ControllerParam.Gantry2PowerMeterPos;
                    config.ControllerConfig.ControllerParam.LeftOffset = _controlConfig.ControllerParam.LeftOffset;
                    config.ControllerConfig.ControllerParam.MidOffset = _controlConfig.ControllerParam.MidOffset;
                    config.ControllerConfig.ControllerParam.RightOffset = _controlConfig.ControllerParam.RightOffset;
                    config.ControllerConfig.ControllerParam.PowerMeterMeasurePos1 = _controlConfig.ControllerParam.PowerMeterMeasurePos1;
                    config.ControllerConfig.ControllerParam.PowerMeterMeasurePos2 = _controlConfig.ControllerParam.PowerMeterMeasurePos2;
                    config.ControllerConfig.ControllerParam.PowerMeterMeasurePos3 = _controlConfig.ControllerParam.PowerMeterMeasurePos3;
                    config.ControllerConfig.ControllerParam.PowerMeterMeasurePos4 = _controlConfig.ControllerParam.PowerMeterMeasurePos4;
                    config.ControllerConfig.ControllerParam.PowerMeterMeasurePos5 = _controlConfig.ControllerParam.PowerMeterMeasurePos5;
                    config.ControllerConfig.ControllerParam.PowerMeterMeasurePos6 = _controlConfig.ControllerParam.PowerMeterMeasurePos6;
                    config.ControllerConfig.ControllerParam.UwTorqueSet = _controlConfig.ControllerParam.UwTorqueSet;
                    config.ControllerConfig.ControllerParam.RwTorqueSet = _controlConfig.ControllerParam.RwTorqueSet;
                    config.ControllerConfig.ControllerParam.TapeLength = _controlConfig.ControllerParam.TapeLength;
                    config.ControllerConfig.ControllerParam.StationPosADelay = _controlConfig.ControllerParam.StationPosADelay;
                    config.ControllerConfig.ControllerParam.StationPosBDelay = _controlConfig.ControllerParam.StationPosBDelay;

                    config.ControllerConfig.ParaAxisGantry11.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisGantry11.HomeOffset;
                    config.ControllerConfig.ParaAxisGantry11.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisGantry11.RelDistance;
                    config.ControllerConfig.ParaAxisGantry11.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisGantry11.AbsPosition1;
                    config.ControllerConfig.ParaAxisGantry11.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisGantry11.AbsPosition2;
                    config.ControllerConfig.ParaAxisGantry11.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisGantry11.HomeVelo;
                    config.ControllerConfig.ParaAxisGantry11.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisGantry11.ManualVelo;
                    config.ControllerConfig.ParaAxisGantry11.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisGantry11.WorkVelo;
                    config.ControllerConfig.ParaAxisGantry11.sT_AxisParameter.Acc = _controlConfig.ParaAxisGantry11.Acc;
                    config.ControllerConfig.ParaAxisGantry11.sT_AxisParameter.Dec = _controlConfig.ParaAxisGantry11.Dec;
                    config.ControllerConfig.ParaAxisGantry12.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisGantry12.HomeOffset;
                    config.ControllerConfig.ParaAxisGantry12.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisGantry12.RelDistance;
                    config.ControllerConfig.ParaAxisGantry12.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisGantry12.AbsPosition1;
                    config.ControllerConfig.ParaAxisGantry12.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisGantry12.AbsPosition2;
                    config.ControllerConfig.ParaAxisGantry12.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisGantry12.HomeVelo;
                    config.ControllerConfig.ParaAxisGantry12.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisGantry12.ManualVelo;
                    config.ControllerConfig.ParaAxisGantry12.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisGantry12.WorkVelo;
                    config.ControllerConfig.ParaAxisGantry12.sT_AxisParameter.Acc = _controlConfig.ParaAxisGantry12.Acc;
                    config.ControllerConfig.ParaAxisGantry12.sT_AxisParameter.Dec = _controlConfig.ParaAxisGantry12.Dec;
                    config.ControllerConfig.ParaAxisGantry21.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisGantry21.HomeOffset;
                    config.ControllerConfig.ParaAxisGantry21.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisGantry21.RelDistance;
                    config.ControllerConfig.ParaAxisGantry21.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisGantry21.AbsPosition1;
                    config.ControllerConfig.ParaAxisGantry21.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisGantry21.AbsPosition2;
                    config.ControllerConfig.ParaAxisGantry21.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisGantry21.HomeVelo;
                    config.ControllerConfig.ParaAxisGantry21.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisGantry21.ManualVelo;
                    config.ControllerConfig.ParaAxisGantry21.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisGantry21.WorkVelo;
                    config.ControllerConfig.ParaAxisGantry21.sT_AxisParameter.Acc = _controlConfig.ParaAxisGantry21.Acc;
                    config.ControllerConfig.ParaAxisGantry21.sT_AxisParameter.Dec = _controlConfig.ParaAxisGantry21.Dec;
                    config.ControllerConfig.ParaAxisGantry22.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisGantry22.HomeOffset;
                    config.ControllerConfig.ParaAxisGantry22.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisGantry22.RelDistance;
                    config.ControllerConfig.ParaAxisGantry22.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisGantry22.AbsPosition1;
                    config.ControllerConfig.ParaAxisGantry22.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisGantry22.AbsPosition2;
                    config.ControllerConfig.ParaAxisGantry22.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisGantry22.HomeVelo;
                    config.ControllerConfig.ParaAxisGantry22.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisGantry22.ManualVelo;
                    config.ControllerConfig.ParaAxisGantry22.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisGantry22.WorkVelo;
                    config.ControllerConfig.ParaAxisGantry22.sT_AxisParameter.Acc = _controlConfig.ParaAxisGantry22.Acc;
                    config.ControllerConfig.ParaAxisGantry22.sT_AxisParameter.Dec = _controlConfig.ParaAxisGantry22.Dec;
                    config.ControllerConfig.ParaAxisAlign11.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisAlign11.HomeOffset;
                    config.ControllerConfig.ParaAxisAlign11.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisAlign11.RelDistance;
                    config.ControllerConfig.ParaAxisAlign11.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisAlign11.AbsPosition1;
                    config.ControllerConfig.ParaAxisAlign11.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisAlign11.AbsPosition2;
                    config.ControllerConfig.ParaAxisAlign11.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisAlign11.HomeVelo;
                    config.ControllerConfig.ParaAxisAlign11.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisAlign11.ManualVelo;
                    config.ControllerConfig.ParaAxisAlign11.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisAlign11.WorkVelo;
                    config.ControllerConfig.ParaAxisAlign11.sT_AxisParameter.Acc = _controlConfig.ParaAxisAlign11.Acc;
                    config.ControllerConfig.ParaAxisAlign11.sT_AxisParameter.Dec = _controlConfig.ParaAxisAlign11.Dec;
                    config.ControllerConfig.ParaAxisAlign12.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisAlign12.HomeOffset;
                    config.ControllerConfig.ParaAxisAlign12.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisAlign12.RelDistance;
                    config.ControllerConfig.ParaAxisAlign12.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisAlign12.AbsPosition1;
                    config.ControllerConfig.ParaAxisAlign12.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisAlign12.AbsPosition2;
                    config.ControllerConfig.ParaAxisAlign12.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisAlign12.HomeVelo;
                    config.ControllerConfig.ParaAxisAlign12.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisAlign12.ManualVelo;
                    config.ControllerConfig.ParaAxisAlign12.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisAlign12.WorkVelo;
                    config.ControllerConfig.ParaAxisAlign12.sT_AxisParameter.Acc = _controlConfig.ParaAxisAlign12.Acc;
                    config.ControllerConfig.ParaAxisAlign12.sT_AxisParameter.Dec = _controlConfig.ParaAxisAlign12.Dec;
                    config.ControllerConfig.ParaAxisAlign21.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisAlign21.HomeOffset;
                    config.ControllerConfig.ParaAxisAlign21.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisAlign21.RelDistance;
                    config.ControllerConfig.ParaAxisAlign21.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisAlign21.AbsPosition1;
                    config.ControllerConfig.ParaAxisAlign21.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisAlign21.AbsPosition2;
                    config.ControllerConfig.ParaAxisAlign21.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisAlign21.HomeVelo;
                    config.ControllerConfig.ParaAxisAlign21.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisAlign21.ManualVelo;
                    config.ControllerConfig.ParaAxisAlign21.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisAlign21.WorkVelo;
                    config.ControllerConfig.ParaAxisAlign21.sT_AxisParameter.Acc = _controlConfig.ParaAxisAlign21.Acc;
                    config.ControllerConfig.ParaAxisAlign21.sT_AxisParameter.Dec = _controlConfig.ParaAxisAlign21.Dec;
                    config.ControllerConfig.ParaAxisAlign22.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisAlign22.HomeOffset;
                    config.ControllerConfig.ParaAxisAlign22.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisAlign22.RelDistance;
                    config.ControllerConfig.ParaAxisAlign22.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisAlign22.AbsPosition1;
                    config.ControllerConfig.ParaAxisAlign22.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisAlign22.AbsPosition2;
                    config.ControllerConfig.ParaAxisAlign22.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisAlign22.HomeVelo;
                    config.ControllerConfig.ParaAxisAlign22.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisAlign22.ManualVelo;
                    config.ControllerConfig.ParaAxisAlign22.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisAlign22.WorkVelo;
                    config.ControllerConfig.ParaAxisAlign22.sT_AxisParameter.Acc = _controlConfig.ParaAxisAlign22.Acc;
                    config.ControllerConfig.ParaAxisAlign22.sT_AxisParameter.Dec = _controlConfig.ParaAxisAlign22.Dec;
                    config.ControllerConfig.ParaAxisCamShutter1.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisCamShutter1.HomeOffset;
                    config.ControllerConfig.ParaAxisCamShutter1.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisCamShutter1.RelDistance;
                    config.ControllerConfig.ParaAxisCamShutter1.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisCamShutter1.AbsPosition1;
                    config.ControllerConfig.ParaAxisCamShutter1.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisCamShutter1.AbsPosition2;
                    config.ControllerConfig.ParaAxisCamShutter1.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisCamShutter1.HomeVelo;
                    config.ControllerConfig.ParaAxisCamShutter1.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisCamShutter1.ManualVelo;
                    config.ControllerConfig.ParaAxisCamShutter1.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisCamShutter1.WorkVelo;
                    config.ControllerConfig.ParaAxisCamShutter1.sT_AxisParameter.Acc = _controlConfig.ParaAxisCamShutter1.Acc;
                    config.ControllerConfig.ParaAxisCamShutter1.sT_AxisParameter.Dec = _controlConfig.ParaAxisCamShutter1.Dec;
                    config.ControllerConfig.ParaAxisCamShutter2.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisCamShutter2.HomeOffset;
                    config.ControllerConfig.ParaAxisCamShutter2.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisCamShutter2.RelDistance;
                    config.ControllerConfig.ParaAxisCamShutter2.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisCamShutter2.AbsPosition1;
                    config.ControllerConfig.ParaAxisCamShutter2.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisCamShutter2.AbsPosition2;
                    config.ControllerConfig.ParaAxisCamShutter2.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisCamShutter2.HomeVelo;
                    config.ControllerConfig.ParaAxisCamShutter2.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisCamShutter2.ManualVelo;
                    config.ControllerConfig.ParaAxisCamShutter2.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisCamShutter2.WorkVelo;
                    config.ControllerConfig.ParaAxisCamShutter2.sT_AxisParameter.Acc = _controlConfig.ParaAxisCamShutter2.Acc;
                    config.ControllerConfig.ParaAxisCamShutter2.sT_AxisParameter.Dec = _controlConfig.ParaAxisCamShutter2.Dec;
                    config.ControllerConfig.ParaAxisZ1.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisZ1.HomeOffset;
                    config.ControllerConfig.ParaAxisZ1.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisZ1.RelDistance;
                    config.ControllerConfig.ParaAxisZ1.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisZ1.AbsPosition1;
                    config.ControllerConfig.ParaAxisZ1.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisZ1.AbsPosition2;
                    config.ControllerConfig.ParaAxisZ1.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisZ1.HomeVelo;
                    config.ControllerConfig.ParaAxisZ1.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisZ1.ManualVelo;
                    config.ControllerConfig.ParaAxisZ1.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisZ1.WorkVelo;
                    config.ControllerConfig.ParaAxisZ1.sT_AxisParameter.Acc = _controlConfig.ParaAxisZ1.Acc;
                    config.ControllerConfig.ParaAxisZ1.sT_AxisParameter.Dec = _controlConfig.ParaAxisZ1.Dec;
                    config.ControllerConfig.ParaAxisZ2.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisZ2.HomeOffset;
                    config.ControllerConfig.ParaAxisZ2.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisZ2.RelDistance;
                    config.ControllerConfig.ParaAxisZ2.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisZ2.AbsPosition1;
                    config.ControllerConfig.ParaAxisZ2.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisZ2.AbsPosition2;
                    config.ControllerConfig.ParaAxisZ2.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisZ2.HomeVelo;
                    config.ControllerConfig.ParaAxisZ2.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisZ2.ManualVelo;
                    config.ControllerConfig.ParaAxisZ2.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisZ2.WorkVelo;
                    config.ControllerConfig.ParaAxisZ2.sT_AxisParameter.Acc = _controlConfig.ParaAxisZ2.Acc;
                    config.ControllerConfig.ParaAxisZ2.sT_AxisParameter.Dec = _controlConfig.ParaAxisZ2.Dec;
                    config.ControllerConfig.ParaAxisUwLift.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisUwLift.HomeOffset;
                    config.ControllerConfig.ParaAxisUwLift.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisUwLift.RelDistance;
                    config.ControllerConfig.ParaAxisUwLift.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisUwLift.AbsPosition1;
                    config.ControllerConfig.ParaAxisUwLift.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisUwLift.AbsPosition2;
                    config.ControllerConfig.ParaAxisUwLift.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisUwLift.HomeVelo;
                    config.ControllerConfig.ParaAxisUwLift.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisUwLift.ManualVelo;
                    config.ControllerConfig.ParaAxisUwLift.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisUwLift.WorkVelo;
                    config.ControllerConfig.ParaAxisUwLift.sT_AxisParameter.Acc = _controlConfig.ParaAxisUwLift.Acc;
                    config.ControllerConfig.ParaAxisUwLift.sT_AxisParameter.Dec = _controlConfig.ParaAxisUwLift.Dec;
                    config.ControllerConfig.ParaAxisUw.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisUw.HomeOffset;
                    config.ControllerConfig.ParaAxisUw.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisUw.RelDistance;
                    config.ControllerConfig.ParaAxisUw.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisUw.AbsPosition1;
                    config.ControllerConfig.ParaAxisUw.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisUw.AbsPosition2;
                    config.ControllerConfig.ParaAxisUw.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisUw.HomeVelo;
                    config.ControllerConfig.ParaAxisUw.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisUw.ManualVelo;
                    config.ControllerConfig.ParaAxisUw.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisUw.WorkVelo;
                    config.ControllerConfig.ParaAxisUw.sT_AxisParameter.Acc = _controlConfig.ParaAxisUw.Acc;
                    config.ControllerConfig.ParaAxisUw.sT_AxisParameter.Dec = _controlConfig.ParaAxisUw.Dec;
                    config.ControllerConfig.ParaAxisRwLift.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisRwLift.HomeOffset;
                    config.ControllerConfig.ParaAxisRwLift.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisRwLift.RelDistance;
                    config.ControllerConfig.ParaAxisRwLift.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisRwLift.AbsPosition1;
                    config.ControllerConfig.ParaAxisRwLift.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisRwLift.AbsPosition2;
                    config.ControllerConfig.ParaAxisRwLift.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisRwLift.HomeVelo;
                    config.ControllerConfig.ParaAxisRwLift.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisRwLift.ManualVelo;
                    config.ControllerConfig.ParaAxisRwLift.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisRwLift.WorkVelo;
                    config.ControllerConfig.ParaAxisRwLift.sT_AxisParameter.Acc = _controlConfig.ParaAxisRwLift.Acc;
                    config.ControllerConfig.ParaAxisRwLift.sT_AxisParameter.Dec = _controlConfig.ParaAxisRwLift.Dec;
                    config.ControllerConfig.ParaAxisRw.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisRw.HomeOffset;
                    config.ControllerConfig.ParaAxisRw.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisRw.RelDistance;
                    config.ControllerConfig.ParaAxisRw.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisRw.AbsPosition1;
                    config.ControllerConfig.ParaAxisRw.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisRw.AbsPosition2;
                    config.ControllerConfig.ParaAxisRw.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisRw.HomeVelo;
                    config.ControllerConfig.ParaAxisRw.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisRw.ManualVelo;
                    config.ControllerConfig.ParaAxisRw.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisRw.WorkVelo;
                    config.ControllerConfig.ParaAxisRw.sT_AxisParameter.Acc = _controlConfig.ParaAxisRw.Acc;
                    config.ControllerConfig.ParaAxisRw.sT_AxisParameter.Dec = _controlConfig.ParaAxisRw.Dec;
                    config.ControllerConfig.ParaAxisClean.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisClean.HomeOffset;
                    config.ControllerConfig.ParaAxisClean.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisClean.RelDistance;
                    config.ControllerConfig.ParaAxisClean.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisClean.AbsPosition1;
                    config.ControllerConfig.ParaAxisClean.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisClean.AbsPosition2;
                    config.ControllerConfig.ParaAxisClean.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisClean.HomeVelo;
                    config.ControllerConfig.ParaAxisClean.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisClean.ManualVelo;
                    config.ControllerConfig.ParaAxisClean.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisClean.WorkVelo;
                    config.ControllerConfig.ParaAxisClean.sT_AxisParameter.Acc = _controlConfig.ParaAxisClean.Acc;
                    config.ControllerConfig.ParaAxisClean.sT_AxisParameter.Dec = _controlConfig.ParaAxisClean.Dec;
                    config.ControllerConfig.ParaAxisPowerMeter.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisPowerMeter.HomeOffset;
                    config.ControllerConfig.ParaAxisPowerMeter.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisPowerMeter.RelDistance;
                    config.ControllerConfig.ParaAxisPowerMeter.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisPowerMeter.AbsPosition1;
                    config.ControllerConfig.ParaAxisPowerMeter.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisPowerMeter.AbsPosition2;
                    config.ControllerConfig.ParaAxisPowerMeter.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisPowerMeter.HomeVelo;
                    config.ControllerConfig.ParaAxisPowerMeter.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisPowerMeter.ManualVelo;
                    config.ControllerConfig.ParaAxisPowerMeter.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisPowerMeter.WorkVelo;
                    config.ControllerConfig.ParaAxisPowerMeter.sT_AxisParameter.Acc = _controlConfig.ParaAxisPowerMeter.Acc;
                    config.ControllerConfig.ParaAxisPowerMeter.sT_AxisParameter.Dec = _controlConfig.ParaAxisPowerMeter.Dec;
                    config.ControllerConfig.ParaAxisUwSteer.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisUwSteer.HomeOffset;
                    config.ControllerConfig.ParaAxisUwSteer.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisUwSteer.RelDistance;
                    config.ControllerConfig.ParaAxisUwSteer.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisUwSteer.AbsPosition1;
                    config.ControllerConfig.ParaAxisUwSteer.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisUwSteer.AbsPosition2;
                    config.ControllerConfig.ParaAxisUwSteer.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisUwSteer.HomeVelo;
                    config.ControllerConfig.ParaAxisUwSteer.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisUwSteer.ManualVelo;
                    config.ControllerConfig.ParaAxisUwSteer.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisUwSteer.WorkVelo;
                    config.ControllerConfig.ParaAxisUwSteer.sT_AxisParameter.Acc = _controlConfig.ParaAxisUwSteer.Acc;
                    config.ControllerConfig.ParaAxisUwSteer.sT_AxisParameter.Dec = _controlConfig.ParaAxisUwSteer.Dec;
                    config.ControllerConfig.ParaAxisPeeling1.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisPeeling1.HomeOffset;
                    config.ControllerConfig.ParaAxisPeeling1.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisPeeling1.RelDistance;
                    config.ControllerConfig.ParaAxisPeeling1.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisPeeling1.AbsPosition1;
                    config.ControllerConfig.ParaAxisPeeling1.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisPeeling1.AbsPosition2;
                    config.ControllerConfig.ParaAxisPeeling1.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisPeeling1.HomeVelo;
                    config.ControllerConfig.ParaAxisPeeling1.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisPeeling1.ManualVelo;
                    config.ControllerConfig.ParaAxisPeeling1.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisPeeling1.WorkVelo;
                    config.ControllerConfig.ParaAxisPeeling1.sT_AxisParameter.Acc = _controlConfig.ParaAxisPeeling1.Acc;
                    config.ControllerConfig.ParaAxisPeeling1.sT_AxisParameter.Dec = _controlConfig.ParaAxisPeeling1.Dec;
                    config.ControllerConfig.ParaAxisStationABelt.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisStationABelt.HomeOffset;
                    config.ControllerConfig.ParaAxisStationABelt.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisStationABelt.RelDistance;
                    config.ControllerConfig.ParaAxisStationABelt.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisStationABelt.AbsPosition1;
                    config.ControllerConfig.ParaAxisStationABelt.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisStationABelt.AbsPosition2;
                    config.ControllerConfig.ParaAxisStationABelt.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisStationABelt.HomeVelo;
                    config.ControllerConfig.ParaAxisStationABelt.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisStationABelt.ManualVelo;
                    config.ControllerConfig.ParaAxisStationABelt.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisStationABelt.WorkVelo;
                    config.ControllerConfig.ParaAxisStationABelt.sT_AxisParameter.Acc = _controlConfig.ParaAxisStationABelt.Acc;
                    config.ControllerConfig.ParaAxisStationABelt.sT_AxisParameter.Dec = _controlConfig.ParaAxisStationABelt.Dec;
                    config.ControllerConfig.ParaAxisPeeling2.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisPeeling2.HomeOffset;
                    config.ControllerConfig.ParaAxisPeeling2.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisPeeling2.RelDistance;
                    config.ControllerConfig.ParaAxisPeeling2.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisPeeling2.AbsPosition1;
                    config.ControllerConfig.ParaAxisPeeling2.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisPeeling2.AbsPosition2;
                    config.ControllerConfig.ParaAxisPeeling2.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisPeeling2.HomeVelo;
                    config.ControllerConfig.ParaAxisPeeling2.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisPeeling2.ManualVelo;
                    config.ControllerConfig.ParaAxisPeeling2.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisPeeling2.WorkVelo;
                    config.ControllerConfig.ParaAxisPeeling2.sT_AxisParameter.Acc = _controlConfig.ParaAxisPeeling2.Acc;
                    config.ControllerConfig.ParaAxisPeeling2.sT_AxisParameter.Dec = _controlConfig.ParaAxisPeeling2.Dec;
                    config.ControllerConfig.ParaAxisStationBBelt.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisStationBBelt.HomeOffset;
                    config.ControllerConfig.ParaAxisStationBBelt.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisStationBBelt.RelDistance;
                    config.ControllerConfig.ParaAxisStationBBelt.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisStationBBelt.AbsPosition1;
                    config.ControllerConfig.ParaAxisStationBBelt.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisStationBBelt.AbsPosition2;
                    config.ControllerConfig.ParaAxisStationBBelt.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisStationBBelt.HomeVelo;
                    config.ControllerConfig.ParaAxisStationBBelt.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisStationBBelt.ManualVelo;
                    config.ControllerConfig.ParaAxisStationBBelt.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisStationBBelt.WorkVelo;
                    config.ControllerConfig.ParaAxisStationBBelt.sT_AxisParameter.Acc = _controlConfig.ParaAxisStationBBelt.Acc;
                    config.ControllerConfig.ParaAxisStationBBelt.sT_AxisParameter.Dec = _controlConfig.ParaAxisStationBBelt.Dec;
                    config.ControllerConfig.ParaAxisRwSteer.sT_AxisParameter.HomeOffset = _controlConfig.ParaAxisRwSteer.HomeOffset;
                    config.ControllerConfig.ParaAxisRwSteer.sT_AxisParameter.RelDistance = _controlConfig.ParaAxisRwSteer.RelDistance;
                    config.ControllerConfig.ParaAxisRwSteer.sT_AxisParameter.AbsPosition1 = _controlConfig.ParaAxisRwSteer.AbsPosition1;
                    config.ControllerConfig.ParaAxisRwSteer.sT_AxisParameter.AbsPosition2 = _controlConfig.ParaAxisRwSteer.AbsPosition2;
                    config.ControllerConfig.ParaAxisRwSteer.sT_AxisParameter.HomeVelo = _controlConfig.ParaAxisRwSteer.HomeVelo;
                    config.ControllerConfig.ParaAxisRwSteer.sT_AxisParameter.ManualVelo = _controlConfig.ParaAxisRwSteer.ManualVelo;
                    config.ControllerConfig.ParaAxisRwSteer.sT_AxisParameter.WorkVelo = _controlConfig.ParaAxisRwSteer.WorkVelo;
                    config.ControllerConfig.ParaAxisRwSteer.sT_AxisParameter.Acc = _controlConfig.ParaAxisRwSteer.Acc;
                    config.ControllerConfig.ParaAxisRwSteer.sT_AxisParameter.Dec = _controlConfig.ParaAxisRwSteer.Dec;
                }
            }
            catch
            {
                result = 20;
            }

            return result;
        }

        VisionCalibrationAppService visionAppService = new VisionCalibrationAppService();
        VisionCalibrationConfig _visionCalibrationConfig = new VisionCalibrationConfig();

        /// <summary>
        /// 读取相机配方参数
        /// </summary>
        /// <returns>0正常，30读取出错</returns>
        private int CalibrationConfigRead(DRsoft.Engine.Core.Engine.EngineConfig config)
        {
            int result = 0;
            try
            {
                Method.DicVisionCalibrationConfig.Clear();
                if (Method.LisRecipes.Count > 0)
                {
                    for (int j = 0; j < Method.LisRecipes.Count; j++)
                    {
                        visionAppService.MetadataGuid = Method.LisRecipes[j].Id[2];
                        _visionCalibrationConfig = visionAppService.Read() ?? new VisionCalibrationConfig();
                        Method.DicVisionCalibrationConfig.Add($"{Method.LisRecipes[j].Name}2", _visionCalibrationConfig);
                        Method.GuidRecipeConfig.Add($"{Method.LisRecipes[j].Name}2", Method.LisRecipes[j].Id[2]);
                        if (Method.SelectRecipe != null && Method.SelectRecipe.Equals($"{Method.LisRecipes[j].Name}"))
                        {
                            #region

                            config.CalibrationConfig.IpAddress = _visionCalibrationConfig.IpAddress;
                            config.CalibrationConfig.Port = _visionCalibrationConfig.Port;
                            #endregion
                        }
                    }
                }
                else
                {
                    _visionCalibrationConfig = visionAppService.Read() ?? new VisionCalibrationConfig();
                    config.CalibrationConfig.IpAddress = _visionCalibrationConfig.IpAddress;
                    config.CalibrationConfig.Port = _visionCalibrationConfig.Port;
                }
            }
            catch (Exception ex)
            {
                result = 30;
                System.Windows.MessageBox.Show(ex.ToString());
            }

            return result;
        }
    }
}
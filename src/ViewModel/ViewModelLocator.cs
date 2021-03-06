using CSGO_Demos_Manager.Services;
using CSGO_Demos_Manager.Services.Excel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace CSGO_Demos_Manager.ViewModel
{
	public class ViewModelLocator
	{
		public ViewModelLocator()
		{
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			SimpleIoc.Default.Register<DialogService, DialogService>();

			if (ViewModelBase.IsInDesignModeStatic)
			{
				// Create design time view services and models
				SimpleIoc.Default.Register<IDemosService, DemosDesignService>();
				SimpleIoc.Default.Register<ISteamService, SteamServiceDesign>();
				SimpleIoc.Default.Register<ICacheService, CacheDesignService>();
				SimpleIoc.Default.Register<ExcelService, ExcelService>();
			}
			else
			{
				// Create run time view services and models
				SimpleIoc.Default.Register<IDemosService, DemosService>();
				SimpleIoc.Default.Register<ISteamService, SteamService>();
				SimpleIoc.Default.Register<ICacheService, CacheService>();
				SimpleIoc.Default.Register<ExcelService, ExcelService>();
			}

			SimpleIoc.Default.Register<MainViewModel>();
			SimpleIoc.Default.Register<HomeViewModel>();
			SimpleIoc.Default.Register<SettingsViewModel>();
			SimpleIoc.Default.Register<DetailsViewModel>();
			SimpleIoc.Default.Register<SuspectsViewModel>();
			SimpleIoc.Default.Register<HeatmapViewModel>();
			SimpleIoc.Default.Register<EntryKillsViewModel>();
		}

		public MainViewModel Main
		{
			get
			{
				return ServiceLocator.Current.GetInstance<MainViewModel>();
			}
		}

		public SettingsViewModel Settings
		{
			get
			{
				return ServiceLocator.Current.GetInstance<SettingsViewModel>();
			}
		}

		public DetailsViewModel Details
		{
			get
			{
				return ServiceLocator.Current.GetInstance<DetailsViewModel>();
			}
		}

		public HomeViewModel Home
		{
			get
			{
				return ServiceLocator.Current.GetInstance<HomeViewModel>();
			}
		}

		public SuspectsViewModel Suspects
		{
			get
			{
				return ServiceLocator.Current.GetInstance<SuspectsViewModel>();
			}
		}

		public HeatmapViewModel Heatmap
		{
			get
			{
				return ServiceLocator.Current.GetInstance<HeatmapViewModel>();
			}
		}

		public EntryKillsViewModel Kills
		{
			get
			{
				return ServiceLocator.Current.GetInstance<EntryKillsViewModel>();
			}
		}

		public static void Cleanup()
		{
			// TODO Clear the ViewModels
		}
	}
}
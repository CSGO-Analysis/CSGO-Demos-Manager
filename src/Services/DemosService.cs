﻿using CSGO_Demos_Manager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using CSGO_Demos_Manager.Models.Source;
using CSGO_Demos_Manager.Services.Analyzer;

namespace CSGO_Demos_Manager.Services
{
	public class DemosService : IDemosService
	{
		private readonly CacheService _cacheService = new CacheService();

		private async Task<Demo> GetDemoHeaderAsync(string demoFilePath)
		{
			if (!File.Exists(demoFilePath))
			{
				throw new Exception("Demo not found.");
			}

			var demo = await Task.Run(() => DemoAnalyzer.ParseDemoHeader(demoFilePath));
			if (demo == null) return null;

			// If demo is in cache we retrieve its data
			bool demosIsCached = _cacheService.HasDemoInCache(demo);
			if (demosIsCached)
			{
				demo = await _cacheService.GetDemoDataFromCache(demo);
				demo.Source = Source.Factory(demo.SourceName);
			}
			return demo;
		}

		public async Task<List<Demo>> GetDemosHeader(List<string> folders)
		{
			List<Demo> demos = new List<Demo>();

			if (folders.Count > 0)
			{
				foreach (string folder in folders)
				{
					if (Directory.Exists(folder))
					{
						string[] files = Directory.GetFiles(folder, "*.dem");
						foreach (string file in files)
						{
							var demo = await GetDemoHeaderAsync(file);
							if (demo != null && !demos.Contains(demo)) demos.Add(demo);
						}
					}
				}
			}
			return demos;
		}

		public async Task<Demo> AnalyzeDemo(Demo demo)
		{
			if (!File.Exists(demo.Path))
			{
				// Demo may be moved to an other folder, just clear cache
				await _cacheService.RemoveDemo(demo);
			}

			DemoAnalyzer analyzer = DemoAnalyzer.Factory(demo);
			
			demo = await analyzer.AnalyzeDemoAsync();

			await _cacheService.WriteDemoDataCache(demo);
	   
			return demo;
		}

		public async Task SaveComment(Demo demo, string comment)
		{
			demo.Comment = comment;
			await _cacheService.WriteDemoDataCache(demo);
		}

		public async Task SaveStatus(Demo demo, string status)
		{
			demo.Status = status;
			await _cacheService.WriteDemoDataCache(demo);
		}

		public async Task SetSource(ObservableCollection<Demo> demos, string source)
		{
			foreach (Demo demo in demos.Where(demo => demo.Type != "POV"))
			{
				switch (source)
				{
					case "valve":
						demo.Source = new Valve();
						break;
					case "esea":
						demo.Source = new Esea();
						break;
					case "ebot":
						demo.Source = new Ebot();
						break;
					default:
						demo.Source = new Valve();
						break;
				}
				await _cacheService.WriteDemoDataCache(demo);
			}
		}
	}
}

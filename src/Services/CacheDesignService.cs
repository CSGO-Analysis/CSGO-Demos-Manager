﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CSGO_Demos_Manager.Models;

namespace CSGO_Demos_Manager.Services
{
	public class CacheDesignService : ICacheService
	{
		public bool HasDemoInCache(Demo demo)
		{
			return true;
		}

		public Task<Demo> GetDemoDataFromCache(Demo demo)
		{
			demo.Id = "de_dust25445648778447878";
			demo.Name = "esea_nip_vs_titan.dem";
			demo.Tickrate = 128;
			demo.MapName = "de_dust2";
			demo.ClientName = "localhost";
			demo.Hostname = "local";
			demo.OneKillCount = 190;
			demo.TwoKillCount = 80;
			demo.ThreeKillCount = 25;
			demo.FourKillCount = 3;
			demo.FiveKillCount = 1;
			demo.BombExplodedCount = 5;
			demo.BombDefusedCount = 6;
			demo.TotalKillCount = 260;
			demo.Path = "C:\\mydemo.dem";
			demo.ScoreTeam1 = 16;
			demo.ScoreTeam2 = 6;
			demo.Type = "GOTV";

			return Task.FromResult(demo);
		}

		public Task WriteDemoDataCache(Demo demo)
		{
			return Task.FromResult(0);
		}

		public Task<bool> AddSuspectToCache(string suspectSteamCommunityId)
		{
			return Task.FromResult(true);
		}

		public Task<List<string>> GetSuspectsListFromCache()
		{
			List<string> suspecIdtList = new List<string>()
			{
				"121545454",
				"5455155"
			};

			return Task.FromResult(suspecIdtList);
		}

		public Task<bool> RemoveSuspectFromCache(string steamId)
		{
			return Task.FromResult(true);
		}

		public Task ClearData()
		{
			return Task.FromResult(true);
		}
	}
}
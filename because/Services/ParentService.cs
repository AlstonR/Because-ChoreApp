using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace because.Services
{
	public class ParentService
	{
		public async Task<List<ChoreModel>> GetChoreListAsync()
		{
			List<ChoreModel> choreList = new List<ChoreModel>();
			CollectionReference collection = AuthService.Database.Collection("Chores");

			QuerySnapshot querySnapshot = await collection
				.WhereEqualTo("Email", AuthService._currentUser.Email)
				.GetSnapshotAsync();

			var filteredDocuments = querySnapshot.Documents.Where(document =>
																  document.GetValue<string>("Status") == "Pending").ToList();

			foreach (var document in filteredDocuments)
			{
				ChoreModel chore = new ChoreModel();
				chore.Name = document.GetValue<string>("Name");
				chore.Coins = document.GetValue<int>("Coins");
				chore.ImageString = document.GetValue<string>("Picture");
				chore.Status = document.GetValue<string>("Status");
				chore.AssigneeName = document.GetValue<string>("AssigneeName");
				choreList.Add(chore);
			}
			return choreList;
		}
		public async Task ChorePayoutAsync(string Name, string assigneeName, int coinsToAdd)
		{
			List<ChoreModel> choreList = new List<ChoreModel>();
			CollectionReference collection = AuthService.Database.Collection("Chores");

			QuerySnapshot querySnapshot = await collection
				.WhereEqualTo("Email", AuthService._currentUser.Email)
				.WhereEqualTo("AssigneeName", assigneeName)
				.WhereEqualTo("Name", Name)
				.Limit(1)
				.GetSnapshotAsync();
			Dictionary<string, object> updates = new Dictionary<string, object>
	{
		{ "Status", "Closed" }
	};
			await querySnapshot.Documents[0].Reference.UpdateAsync(updates);

			collection = AuthService.Database.Collection("Users");

			querySnapshot = await collection
				.WhereEqualTo("Email", AuthService._currentUser.Email)
				.WhereEqualTo("Name", assigneeName)
				.Limit(1)
				.GetSnapshotAsync();
			if (querySnapshot.Documents.Count > 0)
			{
				DocumentSnapshot documentSnapshot = querySnapshot.Documents[0];

				if (documentSnapshot.TryGetValue<int>("Coins", out int currentCoinsValue))
				{
						int newCoins = currentCoinsValue + coinsToAdd;

						await documentSnapshot.Reference.UpdateAsync("Coins", newCoins);
				}
			}
		}
		public async Task<List<RewardModel>> GetRewardListAsync()
		{
			List<RewardModel> rewardList = new List<RewardModel>();
			CollectionReference collection = AuthService.Database.Collection("Rewards");

			QuerySnapshot querySnapshot = await collection
				.WhereEqualTo("Email", AuthService._currentUser.Email)
				.GetSnapshotAsync();

			var filteredDocuments = querySnapshot.Documents.Where(document =>
																  document.GetValue<string>("Status") == "Pending").ToList();

			foreach (var document in filteredDocuments)
			{
				RewardModel reward = new RewardModel();
				reward.Name = document.GetValue<string>("Name");
				reward.Coins = document.GetValue<int>("Coins");
				reward.ImageString = document.GetValue<string>("Picture");
				reward.Status = document.GetValue<string>("Status");
				reward.AssigneeName = document.GetValue<string>("AssigneeName");
				rewardList.Add(reward);
			}
			return rewardList;
		}
		public async Task RewardPayoutAsync(string Name, string assigneeName, int coinsToAdd)
		{
			List<RewardModel> rewardList = new List<RewardModel>();
			CollectionReference collection = AuthService.Database.Collection("Rewards");

			QuerySnapshot querySnapshot = await collection
				.WhereEqualTo("Email", AuthService._currentUser.Email)
				.WhereEqualTo("AssigneeName", assigneeName)
				.WhereEqualTo("Name", Name)
				.Limit(1)
				.GetSnapshotAsync();
			Dictionary<string, object> updates = new Dictionary<string, object>
	{
		{ "Status", "Closed" }
	};
			await querySnapshot.Documents[0].Reference.UpdateAsync(updates);

		}
	}
}


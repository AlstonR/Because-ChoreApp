using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace because.Services
{
	public class RewardService
	{
		public async Task<MethodResult> AddRewardAsync(RewardModel reward)
		{
			try
			{
				var collection = AuthService.Database.Collection("Rewards");

				byte[] fileBytes;
				using (var memoryStream = new MemoryStream())
				{
					await reward.Image.OpenReadStream().CopyToAsync(memoryStream);
					fileBytes = memoryStream.ToArray();
				}
				string base64ProfileImage = Convert.ToBase64String(fileBytes);
				base64ProfileImage = $"data:{reward.Image.ContentType};base64,{base64ProfileImage}";

				var rewardData = new Dictionary<string, object>()
		{
			{ "Email", AuthService._currentUser.Email },
			{ "HouseHoldName", AuthService._currentUser.HouseHoldName },
			{ "Name", reward.Name },
			{"Picture", base64ProfileImage},
			{ "Coins", reward.Coins },
			{ "AssigneeName", reward.AssigneeName },
		 	{ "Status", "Active" },
		};

				await collection.AddAsync(rewardData);

				return MethodResult.Success();
			}
			catch (Exception)
			{
				return MethodResult.Fail("Failed to sign up. Please try again.");
			}
		}
		public async Task<List<RewardModel>> GetRewardListAsync()
		{
			List<RewardModel> rewardList = new List<RewardModel>();
			CollectionReference collection = AuthService.Database.Collection("Rewards");

			QuerySnapshot querySnapshot = await collection
				.WhereEqualTo("Email", AuthService._currentUser.Email)
				.WhereEqualTo("AssigneeName", AuthService._currentUser.Name)
				.GetSnapshotAsync();

			var filteredDocuments = querySnapshot.Documents.Where(document =>
																  document.GetValue<string>("Status") != "Closed").ToList();

			foreach (var document in filteredDocuments)
			{
				RewardModel reward = new RewardModel();
				reward.Name = document.GetValue<string>("Name");
				reward.Coins = document.GetValue<int>("Coins");
				reward.ImageString = document.GetValue<string>("Picture");
				reward.Status = document.GetValue<string>("Status");
				rewardList.Add(reward);
			}
			return rewardList;
		}
		public async Task UpdateRewardStatusAsync(string Name, string UpdateStatus, int coins)
		{
			List<RewardModel> rewardList = new List<RewardModel>();

			CollectionReference collection = AuthService.Database.Collection("Users");

			QuerySnapshot querySnapshot = await collection
	.WhereEqualTo("Email", AuthService._currentUser.Email)
	.WhereEqualTo("Name", AuthService._currentUser.Name)
	.Limit(1)
	.GetSnapshotAsync();



			if (querySnapshot.Documents.Count > 0)
			{
				DocumentSnapshot documentSnapshot = querySnapshot.Documents[0];

				if (documentSnapshot.TryGetValue<int>("Coins", out int currentCoinsValue))
				{
					await documentSnapshot.Reference.UpdateAsync("Coins", coins);
				}
			}

			collection = AuthService.Database.Collection("Rewards");

			querySnapshot = await collection
			   .WhereEqualTo("Email", AuthService._currentUser.Email)
			   .WhereEqualTo("AssigneeName", AuthService._currentUser.Name)
			   .WhereEqualTo("Name", Name)
			   .Limit(1)
			   .GetSnapshotAsync();

			Dictionary<string, object> updates = new Dictionary<string, object>
	{
		{ "Status", UpdateStatus }
	};
			await querySnapshot.Documents[0].Reference.UpdateAsync(updates);
		}
	}
}


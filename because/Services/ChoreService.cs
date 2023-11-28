using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace because.Services
{
	public class ChoreService
	{

		public async Task<MethodResult> AddChoreAsync(ChoreModel chore)
		{
			try
			{
				var collection = AuthService.Database.Collection("Chores");

				byte[] fileBytes;
				using (var memoryStream = new MemoryStream())
				{
					await chore.Image.OpenReadStream().CopyToAsync(memoryStream);
					fileBytes = memoryStream.ToArray();
				}
				string base64ProfileImage = Convert.ToBase64String(fileBytes);
				base64ProfileImage = $"data:{chore.Image.ContentType};base64,{base64ProfileImage}";

				var choreData = new Dictionary<string, object>()
		{
			{ "Email", AuthService._currentUser.Email },
			{ "HouseHoldName", AuthService._currentUser.HouseHoldName },
			{ "Name", chore.Name },
			{"Picture", base64ProfileImage},
			{ "Coins", chore.Coins },
			{ "AssigneeName", chore.AssigneeName },
		 	{ "Status", "Active" },
		};

				await collection.AddAsync(choreData);

				return MethodResult.Success();
			}
			catch (Exception)
			{
				return MethodResult.Fail("Failed to sign up. Please try again.");
			}
		}
		public async Task<List<ChoreModel>> GetChoreListAsync()
		{
			List<ChoreModel> choreList = new List<ChoreModel>();
			CollectionReference collection = AuthService.Database.Collection("Chores");

			QuerySnapshot querySnapshot = await collection
				.WhereEqualTo("Email", AuthService._currentUser.Email)
				.WhereEqualTo("AssigneeName", AuthService._currentUser.Name)
				.GetSnapshotAsync();

			var filteredDocuments = querySnapshot.Documents.Where(document => 
																  document.GetValue<string>("Status") != "Closed").ToList();

			foreach (var document in filteredDocuments)
			{
				ChoreModel chore = new ChoreModel();
				chore.Name = document.GetValue<string>("Name");
				chore.Coins = document.GetValue<int>("Coins");
				chore.ImageString = document.GetValue<string>("Picture");
				chore.Status = document.GetValue<string>("Status");
				choreList.Add(chore);
			}
			return choreList;
		}
		public async Task UpdateChoreStatusAsync(string Name, string UpdateStatus)
		{
			List<ChoreModel> choreList = new List<ChoreModel>();
			CollectionReference collection = AuthService.Database.Collection("Chores");

			QuerySnapshot querySnapshot = await collection
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
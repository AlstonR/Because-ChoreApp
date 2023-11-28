using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace because.Services
{
	public class ChildService
	{

		public async Task<MethodResult> AddChildAsync(ChildModel child)
		{
			try
			{
				var collection = AuthService.Database.Collection("Users");

				byte[] fileBytes;
				using (var memoryStream = new MemoryStream())
				{
					await child.ProfileImage.OpenReadStream().CopyToAsync(memoryStream);
					fileBytes = memoryStream.ToArray();
				}
				string base64ProfileImage = Convert.ToBase64String(fileBytes);
				base64ProfileImage = $"data:{child.ProfileImage.ContentType};base64,{base64ProfileImage}";

				var childData = new Dictionary<string, object>()
		{
			{ "Email", AuthService._currentUser.Email },
			{ "HouseHoldName", AuthService._currentUser.HouseHoldName },
			{ "Name", child.Name },
			{"ProfilePicture", base64ProfileImage},
			{ "Role", "C" },
			{ "Coins", 0 }
		};

				await collection.AddAsync(childData);

				return MethodResult.Success();
			}
			catch (Exception)
			{
				return MethodResult.Fail("Failed to sign up. Please try again.");
			}
		}
	}
}

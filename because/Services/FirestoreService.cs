using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace because.Services
{
	public class FirestoreService
	{
		public static async Task InitializeFirestoreAsync()
		{
			try
			{
				var localPath = Path.Combine(FileSystem.CacheDirectory, "credentials.json");

				using var json = await FileSystem.OpenAppPackageFileAsync("credentials.json");
				using var dest = File.Create(localPath);
				await json.CopyToAsync(dest);

				Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", localPath);

				dest.Close();
			}
			catch (Exception ex) {
				Console.WriteLine($"An error occurred: {ex}");
					}
		}
	}
}

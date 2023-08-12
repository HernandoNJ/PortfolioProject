using Amazon;
using Amazon.CognitoIdentity;
using Amazon.S3;
using Amazon.S3.Model;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.DemoProjects.GameDevHQ.Projects.Service_App.Scripts
{
	public class AWSManager : MonoBehaviour
	{
		private static AWSManager instance;
		public static AWSManager Instance => instance;

		private AmazonS3Client s3Client;
		public AmazonS3Client S3Client => s3Client;

		public string S3Region = RegionEndpoint.USEast2.SystemName;
		private RegionEndpoint _S3Region => RegionEndpoint.GetBySystemName(S3Region);

		private readonly string identityPoolId = "us-east-2:540a9e84-b4bb-49f9-a1df-60a8b67a4d02";
		private readonly string awsBucketName = "aws-case-bucket";

		private void Awake()
		{
			instance = this;
			Init();
		}

		private void Start()
		{
			if (instance == null)
				Debug.LogError("AWS manager is null");
		}

		private void Init()
		{
			UnityInitializer.AttachToGameObject(gameObject);
			AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;

			var credentials = new CognitoAWSCredentials(identityPoolId, RegionEndpoint.USEast2);

			s3Client = new AmazonS3Client(credentials, _S3Region);

			s3Client.ListBucketsAsync(new ListBucketsRequest(), responseObject =>
			{
				if (responseObject.Exception == null)
					responseObject.Response.Buckets.ForEach(
							s3b => { Debug.Log("Bucket names in list: " + s3b.BucketName); });

				else
					Debug.Log("AWS error ..." + responseObject.Exception);
			});
		}

		public void UploadToS3(string filePath, string caseId)
		{
			// FIX stream is not closed
			var stream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);

			var request = new PostObjectRequest
			{
				Bucket = awsBucketName,
				Key = "case #" + caseId,
				InputStream = stream,
				CannedACL = S3CannedACL.Private,
				Region = _S3Region
			};

			// FIX the stream (request) is being uploaded, how to close it?
			// BUG AndroidPlayer(ADB@127.0.0.1:34999) ArgumentException: Object of type 'System.Object[]' cannot be converted to type 'UnityEngine.AndroidJavaObject[]'.
			s3Client.PostObjectAsync(request, responseObj =>
			{
				if (responseObj.Exception == null)
				{
				// TODO here the scene can be reloaded
				Debug.Log("Successfully posted to bucket");
				}
				else
					Debug.Log("Exception occurred during uploading" + responseObj.Exception);
			});
		}

		public void GetS3List(string userNumberInput)
		{
			var targetCase = "case #" + userNumberInput;
			var request = new ListObjectsRequest { BucketName = awsBucketName };

			s3Client.ListObjectsAsync(request, responseObject =>
			{
				if (responseObject.Exception == null)
				{
					var caseFound = responseObject.Response.S3Objects.Any(s => s.Key == targetCase);

					if (caseFound)
					{
						s3Client.GetObjectAsync(awsBucketName, targetCase, awsObjResult =>
						{
						// Check if response stream is null
						if (awsObjResult.Response.ResponseStream != null)
							{
								Debug.Log("awsObjResult is not null");
							// Byte array to store data from file
							byte[] data = null;

							// Use stream reader to read ResponseStream data
							using (var reader = new StreamReader(awsObjResult.Response.ResponseStream))
								{
								// Access memory stream
								using (var memory = new MemoryStream())
									{
									// Populate data byte array wit memory stream data
									var buffer = new byte[512];
										int bytesRead;

										while ((bytesRead = reader.BaseStream.Read(buffer, 0, buffer.Length)) > 0)
										{
											memory.Write(buffer, 0, bytesRead);
											data = memory.ToArray();
										}
									}

								// Convert the bytes into an object (case)
								using (var memory = new MemoryStream(data))
									{
									// Use binary formatter to re construct file
									var bf = new BinaryFormatter();
										var downloadedCase = (Case)bf.Deserialize(memory);
										Debug.Log("Downloaded case with client's name: " + downloadedCase.clientName);
									}
								}
							}

							else
								Debug.Log("Case not found");
						});
					//else Debug.Log("Error getting list of items from S3..." + responseObject.Exception);
				}
				}
			});
		}

	}
}

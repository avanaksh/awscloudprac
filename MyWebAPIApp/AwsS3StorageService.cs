

public class AwsS3StorageService : IStorageService
{
const string BucketName = "csharp-examples-bucket";
public async Task<string> Upload(Stream stream)
{
    var fileName = Guid.NewGuid().ToString() + ".pdf";
    using var s3Client = new AmazonS3Client(RegionEndpoint.EUWest2);
    await s3Client.PutObjectAsync(new PutObjectRequest()
    {
    InputStream = stream,
    BucketName = BucketName,
    Key = fileName,
    });
    var url = s3Client.GetPreSignedURL(new GetPreSignedUrlRequest()
    {
    BucketName = BucketName,
    Key = fileName,
    Expires = DateTime.UtcNow.AddMinutes(10)
    });
    return url;
}
}
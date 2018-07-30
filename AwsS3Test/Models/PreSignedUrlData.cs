using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AwsS3Test.Models
{
    public class PreSignedUrlData
    {
        public string ObjectKey { get; set; }
        public string BucketName { get; set; }
        public string BucketRegion { get; set; }

        public string GeneratePreSignedURL()
        {
            var bucketRegion = typeof(RegionEndpoint).GetField(BucketRegion).GetValue(null);
            var s3Client = new AmazonS3Client((RegionEndpoint)bucketRegion);

            var request = new GetPreSignedUrlRequest
            {
                BucketName = BucketName,
                Key = ObjectKey,
                Verb = HttpVerb.PUT,
                Expires = DateTime.Now.AddHours(1)
            };

            string url = s3Client.GetPreSignedURL(request);
            return url;
        }
    }
}
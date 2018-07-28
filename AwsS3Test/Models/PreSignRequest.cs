using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace TraniningMVC.Models
{
    public enum BucketRegion
    {
        USEast2,
        USEast1,
        CACentral1,
        CNNorthWest,
        CNNorth1,
        USGovCloudWest1,
        SAEast1,
        APSoutheast1,
        APSouth1,
        APNortheast2,
        APSoutheast2,
        EUCentral1,
        EUWest3,
        EUWest2,
        EUWest1,
        USWest2,
        USWest1,
        APNortheast1
    }

    public class PreSignedRequest
    {
        [HiddenInput(DisplayValue = false)]
        public RegionEndpoint BucketRegion { get; set; }

        [DisplayName("Region")]
        public BucketRegion BucketRegionEnum { get; set; } = Models.BucketRegion.USEast2;

        [DisplayName("Bucket name")]
        public string BucketName { get; set; } = "test-bucket-pre-sign-url";

        [DisplayName("Object Key")]
        public string ObjectKey { get; set; } = "default";

        public HttpPostedFileBase File { get; set; }

        [Range(0, 3, ErrorMessage = "Select a correct license")]
        public HttpVerb HttpRequestMethod { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Expiration time")]
        public DateTime ExpirationTime { get; set; }

        private static IAmazonS3 s3Client;

        public string SendRequest()
        {
            var bucketRegion = typeof(RegionEndpoint).GetField(BucketRegionEnum.ToString()).GetValue(null);
            s3Client = new AmazonS3Client((RegionEndpoint)bucketRegion);
            var url = GeneratePreSignedURL();

            HttpWebRequest httpRequest = WebRequest.Create(url) as HttpWebRequest;
            httpRequest.Method = HttpRequestMethod.ToString();

            if (HttpRequestMethod == HttpVerb.PUT)
            {
                AddFile(httpRequest, url);
            }

            HttpWebResponse response = httpRequest.GetResponse() as HttpWebResponse;

            return url;
        }

        private void AddFile(HttpWebRequest httpRequest, string url)
        {
            using (Stream dataStream = httpRequest.GetRequestStream())
            {
                File.InputStream.CopyTo(dataStream);
            }
        }

        private string GeneratePreSignedURL()
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = BucketName,
                Key = ObjectKey,
                Verb = HttpRequestMethod,
                Expires = ExpirationTime
            };

            string url = s3Client.GetPreSignedURL(request);
            return url;
        }
    }
}
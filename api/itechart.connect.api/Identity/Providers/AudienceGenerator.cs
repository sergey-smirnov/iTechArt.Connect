using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Security.Cryptography;
using Microsoft.Owin.Security.DataHandler.Encoder;

namespace itechart.connect.api.Identity.Providers
{
    internal class AudienceGenerator
    {
        public const string AudienceClientIdKey = "as:AudienceId";
        public const string AudienceSecretKey = "as:AudienceSecret";
        public const string AudienceName = "PerformanceReviewAudience";

        public static Audience GenerateAudience(string name)
        {
            var clientId = Guid.NewGuid().ToString("N");

            var key = new byte[32];
            RandomNumberGenerator.Create().GetBytes(key);
            var base64Secret = TextEncodings.Base64Url.Encode(key);

            var newAudience = new Audience { ClientId = clientId, Base64Secret = base64Secret, Name = name };
            return newAudience;
        }

        public static void InitializeAudienceSettings()
        {
            var audienceClientId = ConfigurationManager.AppSettings[AudienceClientIdKey];
            var audienceSecret = ConfigurationManager.AppSettings[AudienceSecretKey];

            if (String.IsNullOrEmpty(audienceClientId) || String.IsNullOrEmpty(audienceSecret))
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var audience = GenerateAudience(AudienceName);
                config.AppSettings.Settings.Add(AudienceClientIdKey, audience.ClientId);
                config.AppSettings.Settings.Add(AudienceSecretKey, audience.Base64Secret);

                config.Save(ConfigurationSaveMode.Modified);
            }
        }
    }

    internal class Audience
    {
        [Key]
        [MaxLength(32)]
        public string ClientId { get; set; }

        [MaxLength(80)]
        [Required]
        public string Base64Secret { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
    }
}
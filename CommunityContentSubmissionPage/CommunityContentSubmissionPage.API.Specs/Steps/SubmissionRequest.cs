namespace CommunityContentSubmissionPage.API.Specs.Steps
{
    public class SubmissionRequest
    {
        public string Url { get; set; }

        public string Type { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }

        public bool AcceptPrivacyPolicy { get; set; }
    }
}
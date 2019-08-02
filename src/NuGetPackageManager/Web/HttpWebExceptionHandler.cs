﻿namespace NuGetPackageManager.Web
{
    using Catel.Logging;
    using System;
    using System.Net;

    public class HttpWebExceptionHandler : IHttpExceptionHandler<WebException>
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public FeedVerificationResult HandleException(WebException exception, string source)
        {
            try
            {
                var httpWebResponse = (HttpWebResponse)exception.Response;
                if (ReferenceEquals(httpWebResponse, null))
                {
                    return FeedVerificationResult.Invalid;
                }

                //403 error
                if (httpWebResponse.StatusCode == HttpStatusCode.Forbidden)
                {
                    return FeedVerificationResult.AuthorizationRequired;
                }

                //401 error
                if (httpWebResponse.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return FeedVerificationResult.AuthenticationRequired;
                }
            }
            catch (Exception ex)
            {
                Log.Debug(ex, "Failed to verify feed '{0}'", source);
            }

            return FeedVerificationResult.Invalid;
        }
    }
}

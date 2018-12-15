using System;
using System.ServiceModel;
using System.Web;
using LayrCake.StaticModel.DataVisualiserServiceReference;
using RequestBase = LayrCake.StaticModel.DataVisualiserServiceReference.RequestBase;
using ResponseBase = LayrCake.StaticModel.DataVisualiserServiceReference.ResponseBase;

namespace LayrCake.StaticModel.StaticModelReserved
{
    /// <summary>
    /// Base class for all Repositories.
    /// Provides common repository functionality, including:
    ///     - Management of Service client.
    ///     - Offers common request-response correlation check.
    /// </summary>
    public abstract class RepositoryBase
    {
        /// <summary>
        /// Lazy loads ActionServiceClient and stores it in Session object.
        /// </summary>
        protected ActionServiceClient Client
        {
            get
            {
                //Check if not initialized yet
                if (HttpContext.Current.Session == null)
                    return new ActionServiceClient();

                if (HttpContext.Current.Session["ActionServiceClient"] == null)
                    HttpContext.Current.Session["ActionServiceClient"] = new ActionServiceClient();

                // If current client is 'faulted' (due to some error), create a new instance.
                var client = HttpContext.Current.Session["ActionServiceClient"] as ActionServiceClient;
                if (client.State == CommunicationState.Faulted)
                {
                    try { client.Abort(); }
                    catch { /* no action */ }

                    client = new ActionServiceClient();
                    HttpContext.Current.Session["ActionServiceClient"] = client;
                }

                return client;
            }
        }

        /// <summary>
        /// Correlates requestId with returned response correlationId
        /// These must always match. If not, request and responses are not related.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        protected void Correlate(RequestBase request, ResponseBase response)
        {
            if (request.RequestId != response.CorrelationId)
                throw new ApplicationException("RequestId and CorrelationId do not match.");
        }
    }
}
﻿using PnP.Core.Services.Core.CSOM.Requests;
using PnP.Core.Services.Core.CSOM.Utils;
using System.Collections.Generic;

namespace PnP.Core.Services.Core.CSOM
{
    internal sealed class CSOMApiCallBuilder
    {
        internal IIdProvider IdProvider { get; set; } = new IteratorIdProvider();

        internal IBodySerializer RequestBodySerializer { get; set; } = new CSOMBodySerializer();

        internal ICollection<IRequest<object>> Requests { get; set; } = new List<IRequest<object>>();

        internal void AddRequest<T>(IRequest<T> request)
        {
            Requests.Add((IRequest<object>)request);
        }

        internal string SerializeCSOMRequests()
        {
            var requests = new List<ActionObjectPath>();
            foreach (IRequest<object> request in Requests)
            {
                var actionRequests = request.GetRequest(IdProvider);

                requests.AddRange(actionRequests);
            }

            string requestBody = RequestBodySerializer.SerializeRequestBody(requests);

            // Clear requests now that we've built the body
            Requests.Clear();
            return requestBody;
        }
    }
}

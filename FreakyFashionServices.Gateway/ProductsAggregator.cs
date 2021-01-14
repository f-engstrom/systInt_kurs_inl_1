using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using Ocelot.Responses;

namespace FreakyFashionServices.Gateway
{
    public class ProductsAggregator : IDefinedAggregator
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProductsAggregator(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            await Console.Out.WriteAsync("In aggregator");

            
            foreach (var response in responses)
            {
                Console.WriteLine(response.Request);
                Console.WriteLine(response.Response.Body);
            }

            return new DownstreamResponse(new HttpResponseMessage(HttpStatusCode.OK));
        }
    }
}
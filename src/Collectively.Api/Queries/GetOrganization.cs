using System;
using Collectively.Common.Queries;

namespace Collectively.Api.Queries
{
    public class GetOrganization : IQuery
    {
        public Guid Id { get; set; }
    }
}
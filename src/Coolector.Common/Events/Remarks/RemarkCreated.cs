﻿using System;

namespace Coolector.Common.Events.Remarks
{
    public class RemarkCreated : IAuthenticatedEvent
    {
        public Guid RemarkId { get; }
        public string UserId { get; set; }
        public RemarkCategory Category { get; }
        public RemarkFile Photo { get; }
        public RemarkLocation Location { get; }
        public string Description { get; }

        protected RemarkCreated()
        {
        }

        public RemarkCreated(Guid remarkId, string userId, 
            RemarkCategory category, RemarkLocation location,
            RemarkFile photo, string description)
        {
            RemarkId = remarkId;
            UserId = userId;
            Category = category;
            Location = location;
            Photo = photo;
            Description = description;
        }

        public class RemarkFile
        {
            public string InternalId { get; }
            public byte[] Bytes { get; }
            public string Name { get; }
            public string ContentType { get; }

            protected RemarkFile()
            {
            }

            public RemarkFile(string internalId, byte[] bytes,
                string name, string contentType)
            {
                InternalId = internalId;
                Bytes = bytes;
                Name = name;
                ContentType = contentType;
            }
        }

        public class RemarkCategory
        {
            public Guid CategoryId { get; }
            public string Name { get; }

            protected RemarkCategory()
            {
            }

            public RemarkCategory(Guid categoryId, string name)
            {
                CategoryId = categoryId;
                Name = name;
            }
        }

        public class RemarkLocation
        {
            public string Address { get; }
            public double Latitude { get; }
            public double Longitude { get; }

            protected RemarkLocation()
            {
            }

            public RemarkLocation(string address, double latitude, double longitude)
            {
                Address = address;
                Latitude = latitude;
                Longitude = longitude;
            }
        }
    }
}
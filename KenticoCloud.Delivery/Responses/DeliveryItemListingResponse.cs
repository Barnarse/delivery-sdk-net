﻿using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace KenticoCloud.Delivery
{
    /// <summary>
    /// Represents a response from Kentico Cloud Delivery API that contains a list of content items.
    /// </summary>
    public sealed class DeliveryItemListingResponse : AbstractResponse
    {
        private readonly JToken _response;
        private readonly IModelProvider _modelProvider;
        private readonly IContentLinkUrlResolver _contentLinkUrlResolver;
        private Pagination _pagination;
        private IReadOnlyList<ContentItem> _items;
        private dynamic _linkedItems;

        /// <summary>
        /// Gets paging information.
        /// </summary>
        public Pagination Pagination
        {
            get { return _pagination ?? (_pagination = _response["pagination"].ToObject<Pagination>()); }
        }

        /// <summary>
        /// Gets a list of content items.
        /// </summary>
        public IReadOnlyList<ContentItem> Items
        {
            get { return _items ?? (_items = ((JArray)_response["items"]).Select(source => new ContentItem(source, _response["modular_content"], _contentLinkUrlResolver, _modelProvider)).ToList().AsReadOnly()); }
        }

        /// <summary>
        /// Gets the dynamic view of the JSON response where linked items and their properties can be retrieved by name, for example <c>LinkedItems.about_us.elements.description.value</c>.
        /// </summary>
        public dynamic LinkedItems
        {
            get { return _linkedItems ?? (_linkedItems = JObject.Parse(_response["modular_content"].ToString())); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeliveryItemListingResponse"/> class with information from a response.
        /// </summary>
        /// <param name="response">A response from Kentico Cloud Delivery API that contains a list of content items.</param>
        /// /// <param name="modelProvider">An instance of an object that can JSON responses into strongly typed CLR objects</param>
        /// <param name="contentLinkUrlResolver">An instance of an object that can resolve links in rich text elements</param>
        /// <param name="apiUrl">API URL used to communicate with the underlying Kentico Cloud endpoint.</param>
        internal DeliveryItemListingResponse(JToken response, IModelProvider modelProvider, IContentLinkUrlResolver contentLinkUrlResolver, string apiUrl) : base(apiUrl)
        {
            _response = response;
            _modelProvider = modelProvider;
            _contentLinkUrlResolver = contentLinkUrlResolver;
        }

        /// <summary>
        /// Casts DeliveryItemListingResponse to its generic version. Use this method only when the listed items are of the same type.
        /// </summary>
        /// <typeparam name="T">Target type.</typeparam>
        public DeliveryItemListingResponse<T> CastTo<T>()
        {
            return new DeliveryItemListingResponse<T>(_response, _modelProvider, ApiUrl);
        }
    }
}

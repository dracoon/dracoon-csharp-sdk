using System.Collections.Generic;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;

namespace Dracoon.Sdk.SdkInternal.Mapper {
    internal static class AttributeMapper {

        internal static AttributeList FromApiAttributeList(ApiAttributeList apiAttributeList) {
            if (apiAttributeList == null) {
                return null;
            }

            AttributeList attributeList = new AttributeList() {
                Offset = apiAttributeList.Range.Offset,
                Limit = apiAttributeList.Range.Limit,
                Total = apiAttributeList.Range.Total,
                Items = new List<Attribute>()
            };
            foreach (ApiAttribute currentAttribute in apiAttributeList.Items) {
                attributeList.Items.Add(FromApiAttribute(currentAttribute));
            }

            return attributeList;
        }

        private static Attribute FromApiAttribute(ApiAttribute apiAttribute) {
            if (apiAttribute == null) {
                return null;
            }

            Attribute attribute = new Attribute() {
                Key = apiAttribute.Key,
                Value = apiAttribute.Value
            };
            return attribute;
        }

        internal static ApiAddOrUpdateAttributeRequest ToApiAddOrUpdateAttributeRequest(List<Attribute> attributes) {
            if (attributes == null) {
                return null;
            }

            ApiAddOrUpdateAttributeRequest apiRequest = new ApiAddOrUpdateAttributeRequest() {
                Items = new List<ApiAttribute>()
            };

            foreach (Attribute currentAttribute in attributes) {
                apiRequest.Items.Add(ToApiAttribute(currentAttribute));
            }

            return apiRequest;
        }

        private static ApiAttribute ToApiAttribute(Attribute attribute) {
            if (attribute == null) {
                return null;
            }

            ApiAttribute apiAttribute = new ApiAttribute() {
                Key = attribute.Key,
                Value = attribute.Value
            };
            return apiAttribute;
        }
    }
}
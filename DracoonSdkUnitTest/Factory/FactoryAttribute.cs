using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using System.Collections.Generic;

namespace Dracoon.Sdk.UnitTest.Factory {
    internal static class FactoryAttribute {
        internal static AttributeList AttributeList => new AttributeList {
            Total = 2,
            Limit = 2,
            Offset = 0,
            Items = new List<Attribute>() {
                        new Attribute() {
                            Key = "Key1",
                            Value = "Value1"
                        },
                        new Attribute() {
                            Key = "Key2",
                            Value = "Value2"
                        }
                    }
        };

        internal static ApiAttributeList ApiAttributeList => new ApiAttributeList() {
            Range = new ApiRange() {
                Total = 2,
                Limit = 2,
                Offset = 0
            },
            Items = new List<ApiAttribute>() {
                        new ApiAttribute() {
                            Key = "Key1",
                            Value = "Value1"
                        },
                        new ApiAttribute() {
                            Key = "Key2",
                            Value = "Value2"
                        }
                    }
        };

        internal static ApiAddOrUpdateAttributeRequest ApiAddOrUpdateAttributeRequest => new ApiAddOrUpdateAttributeRequest() {
            Items = new List<ApiAttribute>() {
                        new ApiAttribute() {
                            Key = "Key1",
                            Value = "Value1"
                        }
                    }
        };
    }
}
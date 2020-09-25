using System.Collections.Generic;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using Dracoon.Sdk.SdkInternal.Mapper;
using Dracoon.Sdk.UnitTest.Factory;
using Dracoon.Sdk.UnitTest.XUnitComparer;
using Xunit;

namespace Dracoon.Sdk.UnitTest.Test.Mapper {
    public class AttributeMapperTest {
        #region FromApiAttributeList

        [Fact]
        public void FromApiAttributeList() {
            // ARRANGE
            AttributeList expected = FactoryAttribute.AttributeList;

            ApiAttributeList param = new ApiAttributeList() {
                Range = new ApiRange() {
                    Total = expected.Total,
                    Limit = expected.Limit,
                    Offset = expected.Offset
                },
                Items = new List<ApiAttribute>()
            };
            foreach (Attribute current in expected.Items) {
                param.Items.Add(new ApiAttribute() {
                    Key = current.Key,
                    Value = current.Value
                });
            }

            // ACT
            AttributeList actual = AttributeMapper.FromApiAttributeList(param);

            // ASSERT
            Assert.Equal(expected, actual, new AttributeListComparer());
        }

        #endregion

        #region ToApiAddOrUpdateAttributeRequest

        [Fact]
        public void ToApiAddOrUpdateAttributeRequest() {
            // ARRANGE
            ApiAddOrUpdateAttributeRequest expected = FactoryAttribute.ApiAddOrUpdateAttributeRequest;

            List<Attribute> param = new List<Attribute>();
            foreach (ApiAttribute current in expected.Items) {
                param.Add(new Attribute() {
                    Key = current.Key,
                    Value = current.Value
                });
            }

            // ACT
            ApiAddOrUpdateAttributeRequest actual = AttributeMapper.ToApiAddOrUpdateAttributeRequest(param);

            // ASSERT
            Assert.Equal(expected, actual, new ApiAddOrUpdateAttributeRequestComparer());
        }

        #endregion
    }
}
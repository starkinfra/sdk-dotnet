using Xunit;
using StarkInfra;
using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using StarkInfraTests;

namespace StarkInfraTests
{
    public class RequestTest
    {

        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Get()
        {

            string path = "/pix-request";
            Dictionary<string, object> query = new Dictionary<string, object>() { { "limit", 10 } };

            JObject request = Request.Get(
                    path: path,
                    query: query,
                    user: user
                ).Json();

            Assert.NotNull(request["requests"][0]["id"]);

        }

        [Fact]
        public void Post()
        {

            string path = "/issuing-holder";
            Dictionary<string, object> data = new Dictionary<string, object>() {
                {
                    "holders", new List<Dictionary<string, object>>() { new Dictionary<string, object>()
                        {
                            { "name", "Jaime Lannister " + Guid.NewGuid().ToString() },
                            { "externalId", Guid.NewGuid().ToString() },
                            { "taxId", "20.018.183/0001-80" }
                        },

                    }
                }
            };

            JObject request = Request.Post(
                    path: path,
                    payload: data,
                    user: user
                ).Json();

            Assert.NotNull(request["holders"][0]["id"]);

        }

        [Fact]
        public void Patch()
        {

            string path = "/issuing-holder";

            JObject initialState = Request.Get(
                    path: path,
                    query: new Dictionary<string, object>() { { "limit", 1 } },
                    user: user
                ).Json();

            path += "/" + initialState["holders"][0]["id"].ToString();

            Dictionary<string, object> data = new Dictionary<string, object>() { { "tags", new List<string> { Guid.NewGuid().ToString() } } };

            Request.Patch(
                    path: path,
                    payload: data,
                    user: user
                ).Json();

            JObject finalState = Request.Get(
                    path: path,
                    query: null,
                    user: user
                ).Json();

            Assert.NotNull(finalState["holder"]["id"]);

        }

        [Fact]
        public void Delete()
        {

            Dictionary<string, object> data = new Dictionary<string, object>() {
                {
                    "holders", new List<Dictionary<string, object>>() { new Dictionary<string, object>()
                        {
                            { "name", "Jaime Lannister " + Guid.NewGuid().ToString() },
                            { "externalId", Guid.NewGuid().ToString() },
                            { "taxId", "20.018.183/0001-80" }
                        },

                    }
                }
            };

            JObject create = Request.Post(
                    path: "/issuing-holder/",
                    payload: data,
                    user: user
                ).Json();

            JObject deleted = Request.Delete(
                path: "/issuing-holder/" + create["holders"][0]["id"].ToString(),
                user: user
               ).Json();

            Assert.NotNull(deleted["holder"]["id"]);

        }

    }
}

